using System;
using AnimalsEscape.Utils;
using UnityEngine;

namespace AnimalsEscape.Interactive
{
    [RequireComponent(typeof(Collider), typeof(Rotator))]
    public class Coin : MonoBehaviour
    {
        [SerializeField] MeshRenderer _meshRenderer;
        [SerializeField] private Collider _collider;
        [SerializeField, HideInInspector] private Rotator _rotator;
        
        public event Action CollectCoinHandler;

        private void OnValidate()
        {
            if (!_meshRenderer)
                _meshRenderer = GetComponent<MeshRenderer>();

            if (!_collider)
                _collider = GetComponent<Collider>();

            if (!_rotator)
                _rotator = GetComponent<Rotator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.AnimalTag))
                return;

            CollectCoinHandler?.Invoke();
            _meshRenderer.enabled = false;
            _collider.enabled = false;
            _rotator.StopRotation();
        }
    }
}