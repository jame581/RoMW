using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Tento skript spravuje zvuky, hudbu a efekty
    /// </summary>
    public class SoundEffects : MonoBehaviour
    {

        public AudioClip pickAudioClip;
        public AudioClip hittedClip;
        public AudioClip gameOverClip;

        private AudioSource source;

        // Use this for initialization
        void Start ()
        {
            source = GetComponent<AudioSource>();
        }

        public void PlayGameOver()
        {
            if (PlayerPrefs.GetInt("Music") == 1)
            {
                if (!source.isPlaying)
                {
                    source.clip = gameOverClip;
                    source.Play();
                }
            }
        }

        public void PlayPickUp()
        {
            if (PlayerPrefs.GetInt("Music") == 1)
            {
                source.clip = pickAudioClip;
                source.Play();
            }
        
        }

        public void PlayHitted()
        {
            if (PlayerPrefs.GetInt("Music") == 1)
            {
                source.clip = hittedClip;
                source.Play();
            }
        }
    }
}
