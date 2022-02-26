using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Trida slouzi k uchovani nastaveni hry, uklada, nacita a stara se o data.
    /// </summary>
    public class Config : MonoBehaviour {

        private int Controller { get; set; } // tip ovladani
        public int Music { get; set; }  // stav hudby zap/vyp
        public int HighScore { get; private set; }  // nejvetsi high score
        private float PlayerSpeed { get; set; }  // rychlost pohybu hrace
        private int FirstTime { get; set; }  // jestli se hra zapla poprve
        private int ShowHint { get; set; }

        // pojmenovani ulozeni v pameti
        private const string controller = "Controller";
        private const string music = "Music";
        private const string highScore = "HighScore";
        private const string firstTime = "FirstTime";
        private const string playerSpeed = "PlayerSpeed";
        private const string showHint = "showHint";

        // Use this for initialization
        void Start () {

            FirstTime = PlayerPrefs.GetInt(firstTime);
            if (FirstTime == 0)
            {
                SetDefault();
            }
            else
            {
                GetSettings();
            }
        }

        /// <summary>
        /// Defaultni nastevni hry
        /// </summary>
        void SetDefault()
        {
            Controller = 1;
            Music = 1;
            HighScore = 0;
            FirstTime = 1;
            ShowHint = 1;
            PlayerSpeed = 7.0f;
            PlayerPrefs.SetInt(controller, Controller);
            PlayerPrefs.SetInt(music, Music);
            PlayerPrefs.SetInt(highScore, HighScore);
            PlayerPrefs.SetInt(firstTime, FirstTime);
            PlayerPrefs.SetFloat(playerSpeed, PlayerSpeed);      
        }


        /// <summary>
        /// Nacteni nastaveni
        /// </summary>
        public void GetSettings()
        {
            Controller = PlayerPrefs.GetInt(controller);
            Music = PlayerPrefs.GetInt(music);
            HighScore = PlayerPrefs.GetInt(highScore);
            FirstTime = PlayerPrefs.GetInt(firstTime);
            PlayerSpeed = PlayerPrefs.GetFloat(playerSpeed);
            ShowHint = PlayerPrefs.GetInt(showHint);
        }

        /// <summary>
        /// Ulozeni nejvyssiho skore
        /// </summary>
        /// <param name="value"></param>
        public void SaveScore(int value)
        {
            HighScore = value;
            PlayerPrefs.SetInt(highScore, HighScore);
        }

        /// <summary>
        /// Ulozeni stavu hudby
        /// </summary>
        /// <param name="value">0 - vypnuto, 1 - zapnuto</param>
        public void SaveMusic(int value)
        {
            Music = value;
            PlayerPrefs.SetInt(music, Music);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Ulozeni typu ovladani
        /// </summary>
        /// <param name="value">0 - tap, 1 - akcelerometr</param>
        public void SaveController(int value)
        {
            Controller = value;
            PlayerPrefs.SetInt(controller, Controller);
        }

        public void SavePlayerSpeed(float value)
        {
            PlayerSpeed = value;
            PlayerPrefs.SetFloat(playerSpeed, PlayerSpeed);
        }

        public float GetPlayerSpeed()
        {
            return PlayerPrefs.GetFloat(playerSpeed);
        }

        public int GetFirstTime()
        {
            return PlayerPrefs.GetInt(firstTime);
        }

        public int GetShowHint()
        {
            return PlayerPrefs.GetInt(showHint);
        }

        public void SetShowHint(int value)
        {
            PlayerPrefs.SetInt(showHint, value);
        }

        public void ResetAll()
        {
            PlayerPrefs.DeleteAll();
            SetDefault();
        }

    }
}