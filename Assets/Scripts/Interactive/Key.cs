using System;
using AnimalsEscape.Utils;
using UnityEngine;

namespace AnimalsEscape.Interactive
{
    [RequireComponent(typeof(Collider), typeof(Rotator))]
    public class Key : MonoBehaviour
    {
        [SerializeField] MeshRenderer _meshRenderer;
        [SerializeField] Collider _collider;
        [SerializeField, HideInInspector] Rotator _rotator;
        
        public event Action CollectKeyHandler;

        void OnValidate()
        {
            if (!_meshRenderer)
                _meshRenderer = GetComponent<MeshRenderer>();

            if (!_collider)
                _collider = GetComponent<Collider>();

            if (!_rotator)
                _rotator = GetComponent<Rotator>();
        }
        
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.AnimalTag))
                return;
            
            CollectKeyHandler?.Invoke();
            _meshRenderer.enabled = false;
            _collider.enabled = false;
            _rotator.StopRotation();
        }
    }
}
