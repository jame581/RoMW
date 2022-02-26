using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Jednoduchy skript pro vytvoreni pocatecní rychlosti objektu
    /// </summary>
    public class Mover : MonoBehaviour {

        public float speed;
        public float diff;

        private Rigidbody2D rb;

        // Use this for initialization
        void Start () {
            rb = GetComponent<Rigidbody2D>();
            float random = Random.Range(-diff, diff);
            rb.velocity = Vector2.up * (speed - random);
        }
    }
}