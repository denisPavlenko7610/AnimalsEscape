using System;
using AnimalsEscape.Utils;
using UnityEngine;

namespace AnimalsEscape
{
    public class Scanner : MonoBehaviour
    {
        public event Action OnScannerReactHandler;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.AnimalTag))
            {
                OnScannerReactHandler?.Invoke();
            }
        }
    }
}
