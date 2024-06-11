using UnityEngine;

namespace Player
{
    public class AnimalDeathBool : MonoBehaviour
    {
        public bool IsDead { get; set; }

        void Start()
        {
            IsDead = false;
        }

    }
}
