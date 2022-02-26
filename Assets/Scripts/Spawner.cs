using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Skript ridi vyskyt asteroidu, nepratel...
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        public GameObject[] meteorits; // prefaby pro meteority
        public GameObject[] predefinedWave;
        public GameObject fuel; // prefab pro fuel
        public GameObject enemy;
        public float spawnWait; // doba v sekundach, cekani mezi spawnem kamene
        public float spawnStart; // cekani na zacatku hry
        public float waitWave;  // cekani mezi vlnama
        public int count;   // pocet kamenu v prvni vlne
        public Vector2 spawnValues; // hodnoty ve kterych se budou spawnovat prekazky

        private bool gameOver;  // udrzuje hodnotu jestli se stale hraje nebo ne     
        private float chanceToSpawn;
        private int waveCount = 1;

        private Vector2 GetRandomPos()
        {
            return new Vector2(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y);
        }

        /// <summary>
        /// Property game over, rozhoduje o stavu hry
        /// </summary>
        public bool GameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
        }

        public bool UpgradeWeapon { get; set; }

        // Use this for initialization
        void Start()
        {
            UpgradeWeapon = true;
            gameOver = false;
            chanceToSpawn = 0.0f;
        }

        public void StartSpawn()
        {
            StartCoroutine(SpawnWaves());
            StartCoroutine(SpawnFuel());
        }

        IEnumerator SpawnWaves()
        {
            yield return new WaitForSeconds(spawnStart);    // cekani pred prvni vlnou, aby se hrac "rozkoukal"
            while (!gameOver)
            {
                Debug.Log("Count random waves: " + waveCount + ", time: " + Time.realtimeSinceStartup);
                for (int i = 0; i < count; i++) // cyklus pro spawnovani
                {
                    if (gameOver)
                    {
                        break;
                    }
                                        
                    GameObject meteor = SelectMeteorit(); // vyber nahodneho rozsahu meteoritu dle poctu vln
                    Vector2 spawnPos = GetRandomPos(); // nastaveni nahodne pozice
                    Instantiate(meteor, spawnPos, Quaternion.identity); // Spawn meteoritu

                    if (Random.Range(chanceToSpawn, 15.0f) >= 11.5f && (count > count / 2))
                    {
                        SpawnEnemy();
                    }
                    yield return new WaitForSeconds(spawnWait); // cekani
                }
                yield return new WaitForSeconds(waitWave);  // cekani mezi vlnama
                SetDifficulty();
                waveCount++;
            }
        }

        IEnumerator SpawnFuel()
        {
            yield return new WaitForSeconds(Random.Range(spawnStart * 2, 12.0f));    // cekani pred prvni vlnou, aby se hrac "rozkoukal"
            while (!gameOver)
            {
                if (gameOver)
                {
                    break;
                }
                Vector2 spawnPos = new Vector2(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y); // nastaveni nahodne pozice
                Instantiate(fuel, spawnPos, Quaternion.identity); // Spawn meteoritu
                yield return new WaitForSeconds(Random.Range(5.0f, 15.0f)); // cekani
            }
        }

        void SetDifficulty()
        {
            if (waveCount > 4)
            {
                spawnWait = Random.Range(0.5f, 1);
            }
            if (waitWave > 2.5f)
            {
                waitWave -= 0.5f;
            }            
            count += count;
        }

        void SpawnMeteorit(int prefab)
        {
            GameObject tempMeteorit = Instantiate(enemy, GetRandomPos(), Quaternion.identity);
            DestroyByContact sc = tempMeteorit.GetComponent<DestroyByContact>();
            sc.health = Random.Range(1, 4);
        }

        GameObject SelectMeteorit()
        {
            if (waveCount < meteorits.Length / 2)
            {
                return meteorits[Random.Range(0, 5)];
            }
            else if (waveCount > (meteorits.Length / 2) && waveCount < meteorits.Length)
            {
                return meteorits[Random.Range(0, 8)];
            }
            else
            {
                return meteorits[Random.Range(0, meteorits.Length)];
            }
        }

        void SpawnEnemy()
        {
            GameObject tempEnemy = Instantiate(enemy, GetRandomPos(), Quaternion.identity);
            DestroyByContact sc = tempEnemy.GetComponent<DestroyByContact>();
            sc.health = Random.Range(1, 4);
        }

        bool CheckWaveCleared()
        {
            foreach (GameObject o in FindObjectsOfType<GameObject>())
            {
                if (o.CompareTag("Meteorit"))
                {
                    return false;
                }
            }
            return true;
        }

    }

}