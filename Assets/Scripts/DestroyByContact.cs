using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /// <summary>
    /// Skript slouzi k detekci kolizi
    /// </summary>
    public class DestroyByContact : MonoBehaviour {

        public GameObject explosion;    // prefab vybuchu
        public GameObject fuelObject;   // prefab paliva
        public GameObject heartObject;  // prefab srdce
        public GameObject[] weaponObjects; // pole prefabu zbrani
        public int health;  // zivot prekazky
        public int scoreValue;  // hodnota, ktera se pricte hraci po sestreleni

        private GameController gameController;
        private Animator anim;

        void Start()
        {
            // vyhledani Game controlleru
            GameObject gameControllerObject = GameObject.FindWithTag("GameController");
            if (gameControllerObject != null)
            {
                gameController = gameControllerObject.GetComponent<GameController>();
            }
            anim = GetComponent<Animator>();
        }

        /// <summary>
        /// Tato metoda se volá pokažde, kdy dojde ke kolizi s jinym objektem
        /// porovna kolizi a podle typu objektu se kterym nastala kolize se rozhodne
        /// </summary>
        /// <param name="other">Collider druheho objektu</param>
        public void OnTriggerEnter2D(Collider2D other)
        {
            // pokud narazi na hranici
            if (other.CompareTag("Boundary"))
            {
                return;
            }

            // pokud narazi na strelu
            if (other.CompareTag("Shoot"))
            {
                health -= 1;    // snizim zivot
            
                if (anim != null)
                {
                    if (!CompareTag("Enemy"))
                    {
                        anim.SetInteger("hp", health);    // spustim animaci 
                    }
                }

                if (health == 0) // pokud je zivot nula
                {
                    gameController.PlusScore(scoreValue); // prictu score hraci
                    int rnd = Random.Range(0, 130); // generuji pseudonahodne cislo

                    // spawn paliva
                    if (rnd <= 45)
                    {
                        if (fuelObject != null)
                        {
                            Instantiate(fuelObject, transform.position, transform.rotation);
                        }
                    }
                    //spawn zivota
                    if (rnd > 55 && rnd <= 65)
                    {
                        if (gameController.GetPlayerHealth() < 3)
                        {
                            if (heartObject != null)
                            {
                                Instantiate(heartObject, transform.position, transform.rotation);
                            }
                        }
                    }
                    //spawn zbrani
                    if (rnd > 80 && rnd <= 100)
                    {
                        if (weaponObjects != null)
                        {                               
                            //int rando = Random.Range(0, 2); // nahodna zbran
                            switch (gameController.UpgradePlayer)
                            {
                                case 0:
                                    Instantiate(weaponObjects[0], transform.position, transform.rotation);
                                    break;
                                case 1:
                                    Instantiate(weaponObjects[1], transform.position, transform.rotation);
                                    break;
                                case 2:
                                    Instantiate(weaponObjects[2], transform.position, transform.rotation);
                                    break;
                                default:
                                    break;
                            }
                            //if (gameController.UpgradePlayer != rando) // pokud hrac nema jiz stejnou zbran
                            //{
                            //    // spawn zbrane
                                
                            //}
                        }
                    }
                   
                    Destroy(gameObject);    // znicim objekt
                    Instantiate(explosion, transform.position, transform.rotation); // exploze
                }
                Destroy(other.gameObject);  // zniceni objektu, do ktereho to narazilo
            }    
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            // pokud objekt koliduje s hracem
            if (collision.gameObject.CompareTag("Player"))
            {
                if (gameController.UpgradePlayer == 0)
                {
                    gameController.PlayerHealthDown();
                }
                else
                {
                    gameController.PlayerUpgrades(0);
                }
                
                // zobraz explozi
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
