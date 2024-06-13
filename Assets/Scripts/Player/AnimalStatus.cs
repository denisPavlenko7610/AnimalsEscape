using AnimalsEscape;
using UnityEngine;

namespace Player
{
    public enum AnimalState
    {
        dead,
        alive
    }

    public class AnimalStatus : MonoBehaviour
    {
        public AnimalState _animalState;
        [SerializeField] AnimalInput _input;
        [SerializeField] Animal _animal;
        [SerializeField] Collider[] _colliders;

        void OnEnable()
        {
            _animal.OnBulletCollision += SetStateDead;
        }

        void OnDisable()
        {
            _animal.OnBulletCollision -= SetStateDead;
        }

        void Start()
        {
            _animalState = AnimalState.alive;
            _input.enabled = true;
        }

        public void SetStateDead()
        {
            _animalState = AnimalState.dead;
            _input.enabled = false;
            foreach (Collider collider in _colliders)
            {
                collider.enabled = false;
            }
        }
    }
}
