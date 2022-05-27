using UnityEngine;

namespace AnimalsEscape
{
    public class AnimalAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int Run = Animator.StringToHash("Run");
        private float _minValue = 0.01f;

        public void MoveAnimation(Vector2 moveInput)
        {
            if (moveInput.x > _minValue || moveInput.y > _minValue || moveInput.x < -_minValue ||
                moveInput.y < -_minValue)
            {
                _animator.SetBool(Run, true);
            }
            else
            {
                _animator.SetBool(Run, false);
            }
        }
    }
}