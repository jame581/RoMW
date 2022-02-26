using UnityEngine;
using System.Collections;
using Assets.Scripts;

/// <summary>
/// Skript slouzi pro obsluhu kolizi s nepritelem 
/// </summary>
public class DestroyByEnemy : MonoBehaviour {

    private GameController gameController;

    void Start()
    {
        // vyhledani Game controlleru
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // pokud narazi na hranici
        if (other.CompareTag("Boundary"))
        {
            return;
        }

        // pokud narazi na strelu
        if (other.CompareTag("EnemyShoot"))
        {
            if (gameController.UpgradePlayer == 0)
            {
                gameController.PlayerHealthDown();
            }
            else
            {
                gameController.PlayerUpgrades(0);
            }
            Destroy(other.gameObject);  // zniceni objektu, do ktereho to narazilo
        }
    }
}