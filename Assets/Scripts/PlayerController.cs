using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    /// <summary>
    /// Skript se stara o spravu hrace,
    /// ma na starosti pohyb hrace, strelbu, prepinani vylepseni... 
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public GameObject[] playerShips;
        public float speed; // rychlost pohybu hrace
        public float xMinMax; // min a max hodnota osy X, aby hrac nevyjel z obrazovky
        public GameObject shot; // prefab strely
        public Transform shotSpawn; // misto odkud se vystreli
        public Transform shotSpawn1; // misto odkud se vystreli
        public Transform shotSpawn2; // misto odkud se vystreli
        public float fireRate = 0.5f;   // cas po kterym se muze vystrelit znova    
        public bool canShoot;

        public float Speed
        {
            get { return speed; }
            set
            {
                if (value > 2)
                {
                    speed = value;
                }
                else
                {
                    speed = 2.5f;
                }
            }
        }

        private int playerWeapon;
        private GameController gameController;
        private Rigidbody2D rb;
        private float nextFire = 0.0f;  // casovy interval do dalsi strely
        private AudioSource audioSource;

        private int lastFingerIndex = -1;
        private int lastIndex = -1;

        // Use this for initialization
        public void Start()
        {
            playerShips[0].SetActive(true);
            for (int i = 1; i < playerShips.Length; i++)
            {
                playerShips[i].SetActive(false);
            }
            InitScripts();
            audioSource = GetComponent<AudioSource>();
            rb = GetComponent<Rigidbody2D>();   // nactu RB
            playerWeapon = 0;
            canShoot = true;
        }

        public void Update()
        {
            // ovladani strileni
            if (Time.time > nextFire && !gameController.GameOver && canShoot)
            {
                switch (playerWeapon)
                {
                    case 0:
                        Shoot();
                        playerShips[0].SetActive(true);
                        playerShips[1].SetActive(false);
                        playerShips[2].SetActive(false);
                        playerShips[3].SetActive(false);
                        break;
                    case 1:
                        ShootTwice();
                        playerShips[0].SetActive(false);
                        playerShips[1].SetActive(true);
                        playerShips[2].SetActive(false);
                        playerShips[3].SetActive(false);
                        break;
                    case 2:
                        ShootArrow();
                        playerShips[0].SetActive(false);
                        playerShips[1].SetActive(false);
                        playerShips[2].SetActive(true);
                        playerShips[3].SetActive(false);
                        break;
                    case 3:
                        StartCoroutine(ShootLaser());
                        playerShips[0].SetActive(false);
                        playerShips[1].SetActive(false);
                        playerShips[2].SetActive(false);
                        playerShips[3].SetActive(true);
                        break;
                    default:
                        Shoot();
                        break;
                }
            }
        }

        // Update is called once per frame
        public void FixedUpdate()
        {
            //Check if we are running either in the Unity editor or in a standalone build.
#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
            // ovladani klavesnice
            //if (SystemInfo.deviceType == DeviceType.Desktop)
            //{

            float horizontal = Input.GetAxis("Horizontal");
            Vector2 movement = new Vector2(horizontal, transform.position.y);
            rb.velocity = movement * speed;

            if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.Escape))
            {
                gameController.PauseGame();
            }

            Vector2 direction = Vector2.zero;

            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touches.Length; i++)
                {
                    if (Input.touches[i].phase == TouchPhase.Began)
                    {
                        lastFingerIndex = Input.touches[i].fingerId;
                    }
                    if (Input.touches[i].fingerId == lastFingerIndex)
                    {
                        lastIndex = i;
                    }
                }

                if (lastIndex != -1)
                {
                    Touch lastTouch = Input.GetTouch(lastIndex);

                    if (lastTouch.phase == TouchPhase.Began || lastTouch.phase == TouchPhase.Moved || lastTouch.phase == TouchPhase.Stationary)
                    {
                        if (lastTouch.position.x < (Screen.width / 2))
                        {
                            direction = Vector2.left;
                        }
                        else if (lastTouch.position.x > (Screen.width / 2))
                        {
                            direction = Vector2.right;
                        }
                    }
                }
                rb.velocity = direction * speed;
            }

            //}
            //Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
#elif UNITY_IOS || UNITY_ANDROID || UNITY_IPHONE || UNITY_WP_8_1 || UNITY_WINRT

            Vector2 direction = Vector2.zero;

            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touches.Length; i++)
                {
                    if (Input.touches[i].phase == TouchPhase.Began)
                    {
                        lastFingerIndex = Input.touches[i].fingerId;
                    }
                    if (Input.touches[i].fingerId == lastFingerIndex)
                    {
                        lastIndex = i;
                    }
                }

                if (lastIndex != -1)
                {
                    Touch lastTouch = Input.GetTouch(lastIndex);

                    if (lastTouch.phase == TouchPhase.Began || lastTouch.phase == TouchPhase.Moved || lastTouch.phase == TouchPhase.Stationary)
                    {
                        if (lastTouch.position.x < (Screen.width / 2))
                        {
                            direction = Vector2.left;
                        }
                        else if (lastTouch.position.x > (Screen.width / 2))
                        {
                            direction = Vector2.right;
                        }
                    }
                }
            }

            rb.velocity = direction * speed;

            if (Input.GetKey(KeyCode.Escape))
            {
                OnBackButtonPressed();
            }

#elif UNITY_WINRT_10_0 || UNITY_WINRT_8_1

            float horizontal = Input.GetAxis("Horizontal");
            Vector2 movement = new Vector2(horizontal, transform.position.y);
            rb.velocity = movement * speed;

            if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.Escape))
            {
                gameController.PauseGame();
            }

                    Vector2 direction = Vector2.zero;

            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touches.Length; i++)
                {
                    if (Input.touches[i].phase == TouchPhase.Began)
                    {
                        lastFingerIndex = Input.touches[i].fingerId;
                    }
                    if (Input.touches[i].fingerId == lastFingerIndex)
                    {
                        lastIndex = i;
                    }
                }

                if (lastIndex != -1)
                {
                    Touch lastTouch = Input.GetTouch(lastIndex);

                    if (lastTouch.phase == TouchPhase.Began || lastTouch.phase == TouchPhase.Moved || lastTouch.phase == TouchPhase.Stationary)
                    {
                        if (lastTouch.position.x < (Screen.width / 2))
                        {
                            direction = Vector2.left;
                        }
                        else if (lastTouch.position.x > (Screen.width / 2))
                        {
                            direction = Vector2.right;
                        }
                    }
                }
                rb.velocity = direction * speed;
            }
            

#endif //End of mobile platform dependendent compilation section started above with #elif

            // kontrola jestli hrac nevyjel mimo obrazovku
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -xMinMax, xMinMax), transform.position.y);
        }

        void Shoot()
        {
            fireRate = 0.45f;
            //shot.transform.localScale = new Vector3(1.3f, 1.3f, 1.0f);
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            if (PlayerPrefs.GetInt("Music") == 1)
            {
                audioSource.Play();
            }
        }

        void ShootTwice()
        {
            fireRate = 0.40f;
            //shot.transform.localScale = new Vector3(1.6f, 1.6f, 1.0f);
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn1.position, shotSpawn1.rotation);
            Instantiate(shot, shotSpawn2.position, shotSpawn2.rotation);

            if (PlayerPrefs.GetInt("Music") == 1)
            {
                audioSource.Play();
            }
        }

        IEnumerator ShootLaser()
        {
            fireRate = 1.25f;
            nextFire = Time.time + fireRate;
            for (int i = 0; i < 5; i++)
            {
                Instantiate(shot, shotSpawn.position, shotSpawn1.rotation);
                yield return new WaitForSeconds(0.1f);
            }

            if (PlayerPrefs.GetInt("Music") == 1)
            {
                audioSource.Play();
            }
        }

        void ShootArrow()
        {
            fireRate = 0.40f;
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn1.position, shotSpawn1.rotation);
            Instantiate(shot, shotSpawn2.position, shotSpawn2.rotation);
            Instantiate(shot, shotSpawn.position, shotSpawn2.rotation);

            if (PlayerPrefs.GetInt("Music") == 1)
            {
                audioSource.Play();
            }
        }

        void OnBackButtonPressed()
        {
            // add code here to handle the back button press
            if (gameController.Pause)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                gameController.PauseGame();
            }
        }

        public void SetWeapon(int up)
        {
            playerWeapon = up;
            for (int i = 0; i < playerShips.Length; i++)
            {
                playerShips[i].SetActive(false);
            }

            playerShips[playerWeapon].SetActive(true);

            if (up == 0)
            {
                gameController.BlinkPlayer();
            }
        }

        private void InitScripts()
        {
            GameObject controllerGameObject = GameObject.FindWithTag("GameController");
            if (controllerGameObject != null)
            {
                gameController = controllerGameObject.GetComponent<GameController>();
            }
        }

        private Vector2 GetDirection(Touch t)
        {
            if (t.position.x < (Screen.width / 2))
            {
                return Vector2.left;
            }
            return Vector2.right;
        }
    }
}