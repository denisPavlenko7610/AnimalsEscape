using Cysharp.Threading.Tasks;
using Player;
using System;
using System.Threading;
using UnityEngine;

namespace AnimalsEscape
{
    public enum PortalType
    {
        Blue,
        Pink
    }

    public class Portal : MonoBehaviour
    {
        [SerializeField] Portal _anotherPortal;
        [SerializeField] PortalVFX _portalVFX;
        
        private CancellationTokenSource _cancellationTokenSource;
        public event Action<Portal> OnPortalTriggerEnterHandler;
        public event Action<bool> OnStateChanged;

        static bool IsActive;

        int _timeToReloadPortalsInMs = 10_000;

        [SerializeField] PortalType _portalType;

        void Awake()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            IsActive = true;
        }

        void OnDisable()
        {
            if (!IsActive)
                _cancellationTokenSource?.Cancel();
        }

        void OnTriggerEnter(Collider other)
        {
            PortalTrigger(other);
        }

        void PortalTrigger(Collider other)
        {
            if (IsActive && other.TryGetComponent(out AnimalTag animalTag))
            {
                OnPortalTriggerEnterHandler?.Invoke(_anotherPortal);
                TogglePortalsAsync();
            }
        }

        async void TogglePortalsAsync()
        {
            ChangePortalView(false);
            try
            {
                await UniTask.Delay(_timeToReloadPortalsInMs, cancellationToken: _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("cancellation token called");
                return;
            }
            ChangePortalView(true);
        }

        void ChangePortalView(bool state)
        {
            IsActive = state;
            OnStateChanged?.Invoke(state);
        }
    }
}
