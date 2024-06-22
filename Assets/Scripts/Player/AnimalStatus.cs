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

        void Start()
        {
            _animalState = AnimalState.alive;
        }

    }
}
