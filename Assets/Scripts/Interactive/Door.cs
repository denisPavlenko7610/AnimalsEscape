using System;
using AnimalsEscape.Utils;
using UnityEngine;

namespace AnimalsEscape
{
    public class Door : MonoBehaviour
    {
        public event Action CompleteLevelHandler;
        bool isTriggered;

        void OnEnable()
        {
            isTriggered = false;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.AnimalTag))
            {
                if (isTriggered)
                    return;

                isTriggered = true;
                CompleteLevelHandler?.Invoke();
            }
        }
    }
}
