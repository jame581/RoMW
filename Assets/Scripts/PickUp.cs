using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Skript se stara o "PickUP" predmety,
    /// jako napriklad palivo, vylepseni hrace atd.
    /// </summary>
    public class PickUp : MonoBehaviour
    {
        public float plusFuel;
        private GameController gameController;

        void Start()
        {
            gameController = FindObjectOfType<GameController>();
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && gameObject.CompareTag("Fuel"))
            {
                gameController.AddFuel(plusFuel);
                Destroy(gameObject);
            }
            if (collision.CompareTag("Player") && gameObject.CompareTag("HeartPickUp"))
            {
                gameController.PlayerHealthUp();
                Destroy(gameObject);
            }

            if (collision.CompareTag("Player") && gameObject.CompareTag("Weapon1"))
            {
                gameController.PlayerUpgrades(1);
                Destroy(gameObject);
            }

            if (collision.CompareTag("Player") && gameObject.CompareTag("Weapon2"))
            {
                gameController.PlayerUpgrades(2);
                Destroy(gameObject);
            }

            if (collision.CompareTag("Player") && gameObject.CompareTag("Weapon3"))
            {
                gameController.PlayerUpgrades(3);
                Destroy(gameObject);
            }
        }
    }
}
