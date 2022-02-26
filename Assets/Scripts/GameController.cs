using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /// <summary>
    /// Hlavni skript, ktery se stara o celou hru
    /// vykresluje skore, aktualizuje herni cas, vyvola zmenu paliva atd.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        

        [HideInInspector]
        public GameObject player;   // prefab hrace
        public GameObject explosionPlayer; // prefab vybuchu hrace

  
        public Text auText; // text pro zobrazeni AU

        public Text gameOverScore;   // Game over text

        public Text pauseText;  // pause text

        public Text loadingText; // loading text
  
        public Button btnPause; // tlacitko pro pause
     
        public GameObject fuelObject;   // prefab paliva
     
        public GameObject gameOverMenu; // Panel s tlacitky menu a restart
     
        public GameObject pauseMenu; // Panel s Pause menu
    
        public GameObject imgHeart;
     
        public GameObject imgFuel;
     
        public GameObject tutorial;

        public Animator hearthAnim; // animator srdicek
        public Animator playerAnim; // animator hrace

        public SoundEffects soundsEffects;



        // Vlastnosti
        public float PlayerFuel { get; set; }

        public bool Pause { get; private set; }

        public bool GameOver { get; set; }

        public int UpgradePlayer { get; set; }

        private int playerHealth;   // zivoty hrace
        private int score;  // score hrace
        private int addScoreV; // hodnota pricteni score
        private Spawner spawner;    // skript pro rizeni spawnovani
        private Config config; // nastaveni hry
        private PlayerController playerController; // skript pro obsluhu hrace
        private TutorialController tutorialController;

        //private bool hitted;
        private AudioSource audioSource;
        private bool playedGameOver;
        private bool playerHitted;

        private FuelBarController fuelController; // skript pro ovladani fuel baru

        public void Awake()
        {           
            InitScripts();
        }

        public void Start()
        {           
            // zakladni nastaveni, na zacatku hry
            playedGameOver = false;
            playerHealth = 3;
            GameOver = false;
            Pause = false;
            gameOverScore.text = "";
            score = 0;
            UpdateScore();
            GetSettings();
            btnPause.enabled = true;
            loadingText.text = "Loading...";
            imgFuel.SetActive(false);
            imgHeart.SetActive(false);
            MusicOnOff();
            addScoreV = 1;
            playerHitted = false;

            int showHint = config.GetShowHint();
            if (showHint == 0)
            {
                StartCoroutine(ShowHint());
            }
            else
            {
                StartGame();
            }


        }

        void Update()
        {
            hearthAnim.SetInteger("playerHealth", playerHealth); // aktualizace health baru
        }

        void StartGame()
        {
            fuelController.StartFuelBar();
            spawner.StartSpawn();
            // pocitani skore kazdou vterinu se vyvola metoda AddAU
            InvokeRepeating("AddAU", 0.5f, 0.5f);
        }

        void AddAU()
        {
            if (GameOver || Pause)
            {
                return;
            }
            score += addScoreV;
            UpdateScore();
        }

        void UpdateScore()
        {
            auText.text = "Score: " + score.ToString("0");
        }

        public void SetGameOver(bool fuel)
        {
            if (!playedGameOver)
            {
                soundsEffects.PlayGameOver();
            }
            playedGameOver = true;

            if (fuel)
            {
                imgFuel.SetActive(true);
            }
            else
            {
                imgHeart.SetActive(true);
            }
            StopAllCoroutines();
            SaveScore();
            gameOverScore.text = "Your score " + score.ToString("0");
            GameOver = true;
            btnPause.enabled = false;
            fuelObject.SetActive(false);
            spawner.GameOver = true;
            gameOverMenu.SetActive(true);
            DeleteAll();
        }

        public void PlusScore(int value)
        {
            score += value;
        }

        public void RestartGame()
        {
            SaveScore();
            pauseMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            loadingText.enabled = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void GoToMenu()
        {
            SaveScore();
            pauseMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            loadingText.enabled = true;
            SceneManager.LoadScene(0);
        }

        void PlayerDead()
        {
            Instantiate(explosionPlayer, player.transform.position, player.transform.rotation);
            Destroy(player.gameObject);
            SetGameOver(false);
        }

        public void PlayerHealthDown()
        {
            if (playerHitted == false)
            {
                playerHitted = true;
                soundsEffects.PlayHitted();
                playerHealth--;        
                BlinkPlayer();                
            }
        }

        public void PlayerHealthUp()
        {
            if (playerHealth < 3)
            {
                playerHealth++;
                soundsEffects.PlayPickUp();
            }
        }

        IEnumerator ShowHint()
        {
            yield return new WaitForSeconds(2.0f);
            tutorialController.NextState();


        }

        public void HideHint()
        {
            config.SetShowHint(1);
            tutorialController.HideHint();
            StartGame();
        }

        public void NextHint()
        {
            bool temp = tutorialController.NextState();
            if (temp)
            {
                HideHint();
            }
        }


        IEnumerator StopImortality()
        {
            yield return new WaitForSeconds(3.0f);
            playerController.canShoot = true;
            playerHitted = false;
            if (playerHealth <= 0)
            {
                PlayerDead();
            }
        }

        public void PlayerUpgrades(int up)
        {
            switch (up)
            {
                case 0:
                    addScoreV = 1;
                    break;
                case 1:
                    addScoreV = 3;
                    break;
                case 2:
                    addScoreV = 4;
                    break;
                case 3:
                    addScoreV = 5;
                    break;
            }
            UpgradePlayer = up;
            playerController.SetWeapon(up);
        }

        public void PauseGame()
        {
            Pause = !Pause;

            if (Pause)
            {
                pauseText.text = "Pause";
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.gameObject.SetActive(false);
            }
        }

        void OnApplicationPause(bool _bool)
        {
            if (_bool)
            {
                PauseGame();
            }
        }

        public void MusicOnOff()
        {
            if (config.Music == 1)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();

            }
        }

        public void GetSettings()
        {
            config.GetSettings();
        }


        public void AddFuel(float plusFuel)
        {
            fuelController.PlusFuel(plusFuel);
        }

        public int GetPlayerHealth()
        {
            return playerHealth;
        }

        public void BlinkPlayer()
        {
            playerHitted = true;
            playerController.canShoot = false;
            if (playerAnim != null)
            {
                playerAnim.SetTrigger("TrigHit");
#if UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WINRT
                Handheld.Vibrate();
#endif
                               
            }
            StartCoroutine(StopImortality());
        }

        private void DeleteAll()
        {
            foreach (GameObject o in FindObjectsOfType<GameObject>())
            {
                if (o.CompareTag("Meteorit") || o.CompareTag("Fuel"))
                {
                    Destroy(o);
                }
                else if (o.CompareTag("Player"))
                {
                    Destroy(o);
                }
                else if (o.CompareTag("Shoot"))
                {
                    Destroy(o);
                }
                else if (o.CompareTag("DeleteAfterGameOver"))
                {
                    Destroy(o);
                }
                else if (o.CompareTag("Enemy"))
                {
                    Destroy(o);
                }
            }
        }

        private void SaveScore()
        {
            if (config.HighScore < score)
            {
                config.SaveScore(score);
            }
        }

        /// <summary>
        /// Metoda pro inicializaci skriptu, promenych, atd.
        /// </summary>
        private void InitScripts()
        {
            gameOverMenu.SetActive(false);  // vypnout game over menu
            pauseMenu.SetActive(false);
            tutorial.SetActive(false);
            Time.timeScale = 1;
            loadingText.enabled = false;
            audioSource = GetComponent<AudioSource>();
            config = GetComponent<Config>();
            fuelController = GetComponent<FuelBarController>();
            tutorialController = GetComponent<TutorialController>();
            player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerController = player.GetComponent<PlayerController>();
            }

            // vyhledani skriptu spawner
            GameObject spawnerObject = GameObject.FindWithTag("Spawner");
            if (spawnerObject != null)
            {
                spawner = spawnerObject.GetComponent<Spawner>();
            }
        }
    }
}