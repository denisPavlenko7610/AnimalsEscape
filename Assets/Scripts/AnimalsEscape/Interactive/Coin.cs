using System;
using AnimalsEscape.Utils;
using DG.Tweening;
using UnityEngine;

namespace AnimalsEscape.Interactive
{
    [RequireComponent(typeof(Collider))]
    public class Coin : MonoBehaviour
    {
        [SerializeField] private float rotateTime = 2f;
        [SerializeField] MeshRenderer _meshRenderer;
        [SerializeField] private Collider _collider;

        public event Action CollectCoinHandler;
        private float _aroundAxisDegree = 360f;
        private Sequence _rotateSequence;
        private void OnValidate()
        {
            if (!_meshRenderer)
            {
                _meshRenderer = GetComponent<MeshRenderer>();
            }

            if (!_collider)
            {
                _collider = GetComponent<Collider>();
            }
        }

        void Start()
        {
            RotateCoin();
        }

        private void RotateCoin()
        {
            _rotateSequence = DOTween.Sequence();
            _rotateSequence.Append(transform
                .DORotate(new Vector3(0, 0, _aroundAxisDegree), rotateTime, RotateMode.FastBeyond360)
                .SetRelative(true)
                .SetEase(Ease.Linear))
                .SetLoops(-1);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.AnimalTag))
            {
                CollectCoinHandler?.Invoke();
                _meshRenderer.enabled = false;
                _collider.enabled = false;
                _rotateSequence.Kill();
            }
        }
    }
}