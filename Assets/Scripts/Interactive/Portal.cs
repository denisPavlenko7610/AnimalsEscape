using System;
using System.Collections;
using AnimalsEscape.Utils;
using UnityEngine;
using UnityEngine.VFX;

namespace AnimalsEscape
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] Transform anotherPortal;
        [SerializeField] PortalVFX _portalVFX;

        public event Action<Transform> OnPortalTriggerEnterHandler;

        void OnTriggerEnter(Collider other)
        {
            if (!_portalVFX.IsActive)
                return;

            if (other.CompareTag(Constants.AnimalTag))
                OnPortalTriggerEnterHandler?.Invoke(anotherPortal);
               
        }
    }
}
