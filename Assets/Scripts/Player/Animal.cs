using AnimalsEscape.Interactive;
using RDTools.AutoAttach;
using UnityEngine;

namespace AnimalsEscape
{
    public class Animal : MonoBehaviour
    {
        [SerializeField, Attach] AnimalInput _animalInput;
        [SerializeField, Attach] AnimalMove _animalMove;
        [SerializeField, Attach] AnimalAnimations _animalAnimations;

        bool canTeleportation = true;
        Key _key;
        public bool HasKey { get; private set; }

        void OnEnable()
        {
            if (_animalInput) _animalInput.input += Move;
        }

        void OnDisable()
        {
            if (_animalInput) _animalInput.input -= Move;
            if (_key) _key.CollectKeyHandler -= SetHasKey;
        }

        public void SetKey(Key key)
        {
            _key = key;
            _key.CollectKeyHandler += SetHasKey;
        }

        public void MoveThroughPortal(Transform anotherPortal)
        {
            if (canTeleportation)
            {
                transform.position = anotherPortal.position;
                canTeleportation = false;
            }
        }

        void SetHasKey() => HasKey = true;

        void Move(Vector2 moveInput)
        {
            _animalMove.MoveInput = moveInput;
            _animalAnimations.MoveAnimation(moveInput);
            SetCanTeleportation(moveInput);
        }

        void SetCanTeleportation(Vector2 moveInput)
        {
            if (canTeleportation)
                return;

            canTeleportation = true;
        }
    }
}