using AnimalsEscape.Interactive;
using UnityEngine;
using Zenject;

namespace AnimalsEscape
{
    public class Animal : MonoBehaviour
    {
        [SerializeField] private AnimalInput _animalInput;
        [SerializeField] private AnimalMove _animalMove;
        [SerializeField] private AnimalAnimations animalAnimations;

        private Key _key;
        public bool HasKey { get; private set; }

        private void OnEnable()
        {
            if (_animalInput) _animalInput.input += Move;
        }

        private void OnDisable()
        {
            if (_animalInput) _animalInput.input -= Move;
            if (_key) _key.CollectKeyHandler -= SetHasKey;
        }

        public void SetKey(Key key)
        {
            _key = key;
            _key.CollectKeyHandler += SetHasKey;
        }

        private void SetHasKey()
        {
            HasKey = true;
        }

        private void Move(Vector2 moveInput)
        {
            _animalMove.MoveInput = moveInput;
            animalAnimations.MoveAnimation(moveInput);
        }
    }
}