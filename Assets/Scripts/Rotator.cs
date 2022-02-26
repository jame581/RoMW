using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Skript pro rotaci predmetu
    /// pri vytvoreni objektu urci nahodnou rotaci doleva ci doprava
    /// </summary>
    public class Rotator : MonoBehaviour {

        public float tumble;

        private Rigidbody2D rb;

        public void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            float random = Random.value;
            if (random <= 0.5f)
            {
                rb.angularVelocity = -Random.value * tumble;
            }
            else
            {
                rb.angularVelocity = Random.value * tumble;
            }

        }
    }
}
