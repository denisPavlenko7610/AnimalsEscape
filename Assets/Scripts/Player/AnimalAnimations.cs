using UnityEngine;

namespace AnimalsEscape
{
    public class AnimalAnimations : MonoBehaviour
    {
        [SerializeField] Animator _animalAnimator;
        static readonly int Run = Animator.StringToHash("Run");
        float _minValue = 0.01f;

        public void MoveAnimation(Vector2 moveInput)
        {
            if (moveInput.x > _minValue || moveInput.y > _minValue || moveInput.x < -_minValue ||
                moveInput.y < -_minValue)
            {
                _animalAnimator.SetBool(Run, true);
            }
            else
            {
                _animalAnimator.SetBool(Run, false);
            }
        }
    }
}