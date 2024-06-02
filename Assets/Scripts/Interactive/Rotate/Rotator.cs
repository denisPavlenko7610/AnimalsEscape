using AnimalsEscape.Interactive.Rotate;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace AnimalsEscape.Interactive
{
    public class Rotator : MonoBehaviour, IRotator, IKillSequence
    {
        [SerializeField, Range(1,10)] float _rotateTime = 2f;

        [SerializeField]
        bool _aroundY;

        float _aroundAxisDegree = 360f;
        Sequence _rotateSequence;

        private void OnDisable()
        {
            StopRotation();
        }

        void Start()
        {
            Rotate();
        }
        
        public void Rotate()
        {
            Vector3 rotationVector;
            rotationVector = _aroundY 
                ? new Vector3(0, _aroundAxisDegree, 0) 
                : new Vector3(0, 0, _aroundAxisDegree);
            
            _rotateSequence = DOTween.Sequence();
            _rotateSequence
                .Append(transform
                    .DORotate(rotationVector, _rotateTime, RotateMode.FastBeyond360)
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