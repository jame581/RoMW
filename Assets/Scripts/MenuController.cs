using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /// <summary>
    /// Skript pro obsluhu hlavniho menu
    /// </summary>
    public class MenuController : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject aboutMenu;
        public GameObject settingsMenu;
        public GameObject confirmReset;
        public Text highScoreText;
        public Text loadingText;
        public Button btnSound;
        public Toggle accel;
        public Toggle tap;

        public Sprite soundOn;
        public Sprite soundOff;

        private AudioSource audioSource;
        private Config config;

        public void Awake()
        {
            mainMenu.SetActive(true);
            aboutMenu.SetActive(false);
            settingsMenu.SetActive(false);
            confirmReset.SetActive(false);
            Time.timeScale = 1;
            loadingText.enabled = false;
        }

        public void Start()
        {
            config = GetComponent<Config>();
            config.GetSettings();
            audioSource = GetComponent<AudioSource>();
            SetSound();
            //SetControl();
            UpdateScore();
            loadingText.text = "Loading...";
        }

        public void StartGame()
        {
            OffMenu();
            loadingText.enabled = true;
            SceneManager.LoadScene(1);
        }

        public void About()
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(false);
            aboutMenu.SetActive(true);
        }

        public void BackToMenu()
        {
            mainMenu.SetActive(true);
            aboutMenu.SetActive(false);
            settingsMenu.SetActive(false);
        }

        public void Settings()
        {
            mainMenu.SetActive(false);
            aboutMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }

        public void Update()
        {
            if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        public void MusicOnOff()
        {
            if (config.Music == 0)
            {
                audioSource.Play();
                config.SaveMusic(1);
                btnSound.image.sprite = soundOn;
            }
            else
            {
                audioSource.Stop();
                config.SaveMusic(0);
                btnSound.image.sprite = soundOff;
            }

        }

        public void AccelOn(bool value)
        {
            if (value)
            {
                config.SaveController(0);
            }
        }

        public void TapOn(bool value)
        {
            if (value)
            {
                config.SaveController(1);
            }
        }

        public void ShowConfirm()
        {
            confirmReset.SetActive(!confirmReset.activeInHierarchy);
        }

        public void ResetAllSettings()
        {
            config.ResetAll();
            UpdateScore();
            confirmReset.SetActive(false);
        }

        private void OffMenu()
        {
            mainMenu.SetActive(false);
            aboutMenu.SetActive(false);
            settingsMenu.SetActive(false);
        }

        private void SetSound()
        {
            if (config.Music == 1)
            {
                audioSource.Play();
                btnSound.image.sprite = soundOn;
            }
            else
            {
                audioSource.Stop();
                btnSound.image.sprite = soundOff;
            }
        }

        private void UpdateScore()
        {
            highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString("0");
        }
    }
}