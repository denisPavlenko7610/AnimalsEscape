using DG.Tweening;
using UnityEngine;

namespace AnimalsEscape
{
    public class ButtonAnimation : MonoBehaviour
    {
        [SerializeField] float _height = 0.1f;
        [SerializeField] float _timeToAnimate = 1f;
        [SerializeField] Transform _buttonCenter;
        [SerializeField] MeshRenderer _meshRenderer;
        [SerializeField] Material _material;

        public void Play()
        {
            var position = _buttonCenter.position;
            var movementVector = Vector3.up * _height;
            var divider = 3f;
            
            _buttonCenter
                .DOMove(position - movementVector, _timeToAnimate)
                .OnComplete(() =>
                {
                    _buttonCenter.DOMove(position + movementVector / divider, _timeToAnimate);
                    _meshRenderer.material = _material;
                });
        }
    }
}