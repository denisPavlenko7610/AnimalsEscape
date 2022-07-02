using System;
using AnimalsEscape.Utils;
using UnityEngine;

namespace AnimalsEscape
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Transform anotherPortal;

        public event Action<Transform> OnPortalTriggerEnterHandler;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.AnimalTag))
            {
                OnPortalTriggerEnterHandler?.Invoke(anotherPortal);
            }
        }
    }
}
