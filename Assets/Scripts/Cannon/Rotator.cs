using System;
using DG.Tweening;
using UnityEngine;

namespace Cannon
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float durationAnimation = 3f;
        [SerializeField] private Vector3 rotateAxe = new Vector3(360f,360f,0);
        private Tween _rotateTween;


        private void Awake()
        {
            _rotateTween = transform
                .DOLocalRotate(rotateAxe, durationAnimation, RotateMode.FastBeyond360)
                .SetRelative(true)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }

        private void OnEnable() => _rotateTween.Play();
        
        private void OnDisable() => _rotateTween.Pause();

        private void OnDestroy() => _rotateTween.Kill();
    }
}
