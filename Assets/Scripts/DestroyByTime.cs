using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Skript pouzivam, pro zniceni objektu po urcitem case.
    /// Napriklad predmetu, ktere se nepohybuji a nemohou dorazit k hranici (animace vybuchu, atd.)
    /// </summary>
    public class DestroyByTime : MonoBehaviour {

        public float lifeTime;

        void Start()
        {
            Destroy(gameObject, lifeTime);
        }
    }
}
