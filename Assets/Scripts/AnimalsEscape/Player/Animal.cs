using UnityEngine;

namespace AnimalsEscape
{
    public class Animal : MonoBehaviour
    {
        [SerializeField] private AnimalInput _animalInput;
        [SerializeField] private AnimalMove _animalMove;
        [SerializeField] private AnimalAnimations animalAnimations;

        private void OnEnable()
        {
            if (_animalInput != null) _animalInput.input += Move;
        }

        private void OnDisable()
        {
            if (_animalInput != null) _animalInput.input -= Move;
        }

        private void Move(Vector2 moveInput)
        {
            _animalMove.MoveInput = moveInput;
            animalAnimations.MoveAnimation(moveInput);
        }
    }
}