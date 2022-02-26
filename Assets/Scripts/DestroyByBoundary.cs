using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Skript slouzi ke zniceni vsech objektu, ktere vyjedou za hranice urceneho objektu
    /// </summary>
    public class DestroyByBoundary : MonoBehaviour
    {
        public void OnTriggerExit2D(Collider2D collision)
        {
            Destroy(collision.gameObject);
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            Destroy(collision.gameObject);
        }
    }
}
