using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace AnimalsEscape
{
    public enum PortalType
    {
        Blue
    }

    public class Portal : MonoBehaviour
    {
        [SerializeField] Portal _anotherPortal;
        [SerializeField] PortalVFX _portalVFX;

        public event Action<Portal> OnPortalTriggerEnterHandler;
        public event Action<bool> OnStateChanged;

        public bool IsActive { get; private set; } = true;

        int _timeToReloadPortalsInMs = 10_000;

        [SerializeField] PortalType _portalType;

        void OnTriggerEnter(Collider other)
        {
            PortalTrigger(other);
        }

        void PortalTrigger(Collider other)
        {
            if (IsActive && other.GetComponentInParent<Animal>())
            {
                OnPortalTriggerEnterHandler?.Invoke(_anotherPortal);
                TogglePortalsAsync();
            }
        }

        async void TogglePortalsAsync()
        {
            ChangePortalView(false);
            await UniTask.Delay(_timeToReloadPortalsInMs);
            ChangePortalView(true);
        }

        void ChangePortalView(bool state)
        {
            IsActive = state;
            OnStateChanged?.Invoke(state);
        }
    }
}
