using AnimalsEscape.Interactive;
using Cannon;
using RDTools.AutoAttach;
using System;
using UnityEngine;

namespace AnimalsEscape
{
    public class Animal : MonoBehaviour
    {
        [SerializeField, Attach] AnimalInput _animalInput;
        [SerializeField, Attach] AnimalMove _animalMove;
        [SerializeField, Attach] AnimalAnimations _animalAnimations;

        public event Action OnBulletCollision;

        Key _key;
        public bool HasKey { get; private set; }

        void OnEnable()
        {
            if (_animalInput)
                _animalInput.input += Move;
        }

        void OnDisable()
        {
            if (_animalInput)
                _animalInput.input -= Move;
            
            if (_key)
                _key.CollectKeyHandler -= SetHasKey;
        }

        public void SetKey(Key key)
        {
            _key = key;
            _key.CollectKeyHandler += SetHasKey;
        }
        public void MoveThroughPortal(Portal anotherPortal)
        {
            transform.position = anotherPortal.transform.position;
        }

        void SetHasKey() => HasKey = true;

        void Move(Vector2 moveInput)
        {
            _animalMove.MoveInput = moveInput;
            _animalAnimations.MoveAnimation(moveInput);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Bullet bullet))
                OnBulletCollision?.Invoke();
        }
    }
}