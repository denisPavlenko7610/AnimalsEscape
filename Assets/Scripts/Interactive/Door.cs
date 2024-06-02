using System;
using AnimalsEscape.Utils;
using UnityEngine;

namespace AnimalsEscape
{
    public class Door : MonoBehaviour
    {
        public event Action CompleteLevelHandler;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.AnimalTag))
            {
                CompleteLevelHandler?.Invoke();
            }
        }
    }
}
