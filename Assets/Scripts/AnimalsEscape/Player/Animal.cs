using System;
using AnimalsEscape.Player;
using UnityEngine;

namespace AnimalsEscape.Animal
{
    public class Animal : MonoBehaviour
    {
        [SerializeField] private AnimalInput _animalInput;
        [SerializeField] private AnimalMove _animalMove;
        [SerializeField] private AnimalAnimation _animalAnimation;

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
            _animalAnimation.MoveAnimation(moveInput);
        }
    }
}