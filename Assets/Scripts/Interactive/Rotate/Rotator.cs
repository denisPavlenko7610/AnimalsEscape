using AnimalsEscape.Interactive.Rotate;
using DG.Tweening;
using UnityEngine;

namespace AnimalsEscape.Interactive
{
    public class Rotator : MonoBehaviour, IRotator, IKillSequence
    {
        [SerializeField, Range(1,10)] private float rotateTime = 2f;

        private float _aroundAxisDegree = 360f;
        private Sequence _rotateSequence;

        void Start()
        {
            Rotate();
        }
        
        public void Rotate()
        {
            _rotateSequence = DOTween.Sequence();
            _rotateSequence
                .Append(transform
                    .DORotate(new Vector3(0, 0, _aroundAxisDegree), rotateTime, RotateMode.FastBeyond360)
                    .SetRelative(true)
                    .SetEase(Ease.Linear))
                .SetLoops(-1);
        }
        
        public void StopRotation()
        {
            _rotateSequence.Kill();
        }
    }
}