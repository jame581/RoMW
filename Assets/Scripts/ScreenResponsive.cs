using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Skript ktery resi prizpusobeni obrazovce, pro jednotlive platformy
    /// </summary>
    public class ScreenResponsive : MonoBehaviour
    {
        public PlayerController playerController;   // skript pro ovladani hrace
        public Spawner spawner;   // skript pro spawn meteoritu
        public Canvas gui;
        public Canvas mainMenu;
        public Canvas pauseMenu;
        public Canvas tutorial;
        public int lastScreenWidth = 0;
        public int lastScreenHeight = 0;

        public void Awake()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep; // nastaveni aby se nevypinala obrazovka    
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            ScreenSize();
        }

        public void Update()
        {
            if (lastScreenWidth.CompareTo(Screen.width) != 0 || lastScreenHeight.CompareTo(Screen.height) != 0)
            {
                lastScreenWidth = Screen.width;
                lastScreenHeight = Screen.height;
                ScreenSize();
            }

        }
        void PlayerSet(float value, float speed)
        {
            if (playerController != null)
            {
                playerController.xMinMax = value;
                playerController.Speed = speed;
            }
        }

        void SpawnerSet(Vector2 value)
        {
            if (spawner != null)
            {
                spawner.spawnValues = value;
            }
        }
        public void ScreenSize()
        {

#if UNITY_STANDALONE || UNITY_WEBGL
        if (Camera.main.aspect >= 2)
        {
            PlayerSet(11.6f, 6.5f);            
            SpawnerSet(new Vector2(11.6f, 6.0f));
        }
        else if (Camera.main.aspect >= 1.7)
        {
            PlayerSet(8.5f, 5.5f);            
            SpawnerSet(new Vector2(8.5f, 6.0f));
        }
        else if (Camera.main.aspect >= 1.6)
        {
            PlayerSet(7.6f, 5.5f);            
            SpawnerSet(new Vector2(7.6f, 6.0f));
        }
        else if (Camera.main.aspect >= 1.5)
        {
            PlayerSet(7.1f, 5.5f);            
            SpawnerSet(new Vector2(7.1f, 6.0f));
        }
        else if (Camera.main.aspect >= 1.3)
        {
            PlayerSet(6.25f, 5.0f);            
            SpawnerSet(new Vector2(6.25f, 6.0f));
        }
        else if (Camera.main.aspect >= 1.2)
        {
            PlayerSet(6.3f, 5.0f);           
            SpawnerSet(new Vector2(6.3f, 6.0f));
        }
        else if (Camera.main.aspect >= 0.7)
        {
            PlayerSet(2.7f, 4.5f);            
            SpawnerSet(new Vector2(2.7f, 6.0f));
        }
        else if (Camera.main.aspect >= 0.6)
        {
            PlayerSet(2.7f, 4.5f);            
            SpawnerSet(new Vector2(2.7f, 6.0f));
        }
        else if (Camera.main.aspect >= 0.5)
        {
            PlayerSet(2.45f, 4.5f);
            SpawnerSet(new Vector2(2.65f, 6.0f));
        }
#elif UNITY_IOS || UNITY_IPHONE
            if (Camera.main.aspect >= 2)
            {
                PlayerSet(11.6f, 3.0f);                
                SpawnerSet(new Vector2(11.6f, 6.0f));
                Debug.Log("Main aspect >= 2");
            }
            else if (Camera.main.aspect >= 1.7)
            {
                PlayerSet(8.5f, 3.0f);               
                SpawnerSet(new Vector2(8.0f, 6.0f));
                Debug.Log("16:9");
            }
            else if (Camera.main.aspect >= 1.6)
            {
                PlayerSet(7.6f, 3.0f);           
                SpawnerSet(new Vector2(7.6f, 6.0f));
                Debug.Log("16:10");
            }
            else if (Camera.main.aspect >= 1.5)
            {
                PlayerSet(7.1f, 3.0f);               
                SpawnerSet(new Vector2(7.1f, 6.0f));
                Debug.Log("3:2");
            }
            else if (Camera.main.aspect >= 1.3)
            {
                PlayerSet(6.25f, 3.0f);               
                SpawnerSet(new Vector2(6.3f, 6.0f));
                Debug.Log("4:3");
            }
            else if (Camera.main.aspect >= 1.2)
            {
                PlayerSet(6.3f, 3.0f);         
                SpawnerSet(new Vector2(6.3f, 6.0f));
                Debug.Log("4:3");
            }
            else if (Camera.main.aspect >= 0.7)
            {
                PlayerSet(3.3f, 3.0f);          
                SpawnerSet(new Vector2(3.4f, 6.0f));
                Debug.Log("10:16");
            }
            else if (Camera.main.aspect >= 0.6)
            {
                PlayerSet(2.9f, 3.0f);           
                SpawnerSet(new Vector2(3.0f, 6.0f));
                Debug.Log("10:16");
            }
            else if (Camera.main.aspect >= 0.563)
            {
                PlayerSet(2.45f, 3.0f);              
                SpawnerSet(new Vector2(2.65f, 6.0f));
                Debug.Log("9:16");
            }
            else if (Camera.main.aspect >= 0.5625)
            {
                PlayerSet(2.45f, 3.0f);                
                SpawnerSet(new Vector2(2.65f, 6.0f));
                Debug.Log("9:16");
            }
            else if (Camera.main.aspect >= 0.56)
            {
                PlayerSet(2.45f, 3.0f);             
                SpawnerSet(new Vector2(2.65f, 6.0f));
                Debug.Log("9:16");
            }
#elif UNITY_ANDROID
            if (Camera.main.aspect >= 2)
            {
                PlayerSet(11.6f, 7.0f);
                SpawnerSet(new Vector2(11.6f, 6.0f));
                Debug.Log("Main aspect >= 2");
            }
            else if (Camera.main.aspect >= 1.8)
            {
                PlayerSet(8.75f, 6.5f);
                SpawnerSet(new Vector2(8.2f, 6.0f));
                Debug.Log("16:9");
            }
            else if (Camera.main.aspect >= 1.7)
            {
                PlayerSet(8.15f, 6.5f);
                SpawnerSet(new Vector2(8.2f, 6.0f));
                Debug.Log("16:9");
            }
            else if (Camera.main.aspect >= 1.6)
            {
                PlayerSet(7.6f, 5.5f);
                SpawnerSet(new Vector2(7.65f, 6.0f));
                Debug.Log("16:10");
            }
            else if (Camera.main.aspect >= 1.5)
            {
                PlayerSet(7.1f, 5.5f);
                SpawnerSet(new Vector2(7.1f, 6.0f));
                Debug.Log("3:2");
            }
            else if (Camera.main.aspect >= 1.3)
            {
                PlayerSet(6.25f, 5.0f);
                SpawnerSet(new Vector2(3.35f, 6.0f));
                Debug.Log("4:3");
            }
            else if (Camera.main.aspect >= 1.2)
            {
                PlayerSet(6.3f, 5.0f);
                SpawnerSet(new Vector2(6.3f, 6.0f));
                Debug.Log("4:3");
            }
            else if (Camera.main.aspect >= 0.7)
            {
                PlayerSet(2.7f, 4.5f);
                SpawnerSet(new Vector2(2.7f, 6.0f));
                Debug.Log("10:16");
            }
            else if (Camera.main.aspect >= 0.65)
            {
                PlayerSet(2.9f, 4.5f);
                SpawnerSet(new Vector2(2.9f, 6.0f));
                Debug.Log("10:16");
            }
            else if (Camera.main.aspect >= 0.62)
            {
                PlayerSet(2.7f, 4.5f);
                SpawnerSet(new Vector2(2.8f, 6.0f));
                Debug.Log("10:16");
            }
            else if (Camera.main.aspect >= 0.6)
            {
                PlayerSet(2.65f, 4.5f);
                SpawnerSet(new Vector2(2.7f, 6.0f));
                Debug.Log("10:16");
            }
            else if (Camera.main.aspect >= 0.57)
            {
                PlayerSet(2.55f, 4.0f);
                SpawnerSet(new Vector2(2.55f, 6.0f));
                Debug.Log("9:16");
            }
            else if (Camera.main.aspect >= 0.5625)
            {
                PlayerSet(2.45f, 4.5f);
                SpawnerSet(new Vector2(2.5f, 6.0f));
                Debug.Log("Full HD portrait");
            }
            else if (Camera.main.aspect >= 0.5)
            {
                PlayerSet(2.45f, 4.0f);
                SpawnerSet(new Vector2(2.5f, 6.0f));
                Debug.Log("9:16");
            }
#elif UNITY_WINRT

            if (Camera.main.aspect >= 2.2)
            {
                PlayerSet(12.0f, 6.0f);
                SpawnerSet(new Vector2(11.6f, 6.0f));
            }
            else if (Camera.main.aspect >= 2)
            {
                PlayerSet(9.9f, 6.0f);
                SpawnerSet(new Vector2(11.6f, 6.0f));
                Debug.Log("Main aspect >= 2");
            }
            else if (Camera.main.aspect >= 1.9)
            {
                PlayerSet(9.4f, 7.0f);
                SpawnerSet(new Vector2(8.5f, 6.0f));
                Debug.Log("16:9");
            }
            else if (Camera.main.aspect >= 1.8)
            {
                PlayerSet(8.85f, 7.0f);
                SpawnerSet(new Vector2(8.5f, 6.0f));
                Debug.Log("16:9");
            }
            else if (Camera.main.aspect >= 1.7)
            {
                PlayerSet(8.4f, 6.0f);
                SpawnerSet(new Vector2(8.5f, 6.0f));
                Debug.Log("16:9");
            }
            else if (Camera.main.aspect >= 1.66)
            {
                PlayerSet(7.75f, 5.5f);
                SpawnerSet(new Vector2(7.6f, 6.0f));
                Debug.Log("16:10");
            }
            else if (Camera.main.aspect >= 1.63)
            {
                PlayerSet(7.65f, 5.5f);
                SpawnerSet(new Vector2(7.6f, 6.0f));
                Debug.Log("16:10");
            }
            else if (Camera.main.aspect >= 1.6)
            {
                PlayerSet(7.55f, 5.5f);
                SpawnerSet(new Vector2(7.6f, 6.0f));
                Debug.Log("16:10");
            }
            else if (Camera.main.aspect >= 1.55)
            {
                PlayerSet(7.35f, 5.0f);
                SpawnerSet(new Vector2(7.1f, 6.0f));
                Debug.Log("3:2");
            }
            else if (Camera.main.aspect >= 1.5)
            {
                PlayerSet(7.35f, 4.5f);
                SpawnerSet(new Vector2(7.1f, 6.0f));
                Debug.Log("3:2");
            }
            else if (Camera.main.aspect >= 1.3)
            {
                PlayerSet(6.55f, 4.0f);
                SpawnerSet(new Vector2(3.35f, 6.0f));
                Debug.Log("4:3");
            }
            else if (Camera.main.aspect >= 1.2)
            {
                PlayerSet(6.3f, 4.0f);
                SpawnerSet(new Vector2(6.3f, 6.0f));
                Debug.Log("4:3");
            }
            else if (Camera.main.aspect >= 0.7)
            {
                PlayerSet(3.35f, 3.5f);
                SpawnerSet(new Vector2(2.7f, 6.0f));
                Debug.Log("10:16");
            }
            else if (Camera.main.aspect >= 0.6)
            {
                PlayerSet(2.7f, 3.5f);
                SpawnerSet(new Vector2(2.7f, 6.0f));
                Debug.Log("10:16");
            }
            else if (Camera.main.aspect >= 0.5)
            {
                PlayerSet(2.55f, 3.5f);
                SpawnerSet(new Vector2(2.65f, 6.0f));
                Debug.Log("9:16");
            }
#endif //End of mobile platform dependendent compilation section started above with #elif

            Debug.Log(Camera.main.aspect);
        }
    }
}