using AnimalsEscape.Core.SceneManagement;
using AnimalsEscape.Interactive;
using UnityEngine;
using Zenject;

namespace AnimalsEscape._Core
{
    public class Game : MonoBehaviour
    {
        private LevelLoader _levelLoader;
        private Door _door;
        private Key _key;
        private Animal _animal;

        [Inject]
        public void Construct(LevelLoader levelLoader, Door door, Animal animal, Key key)
        {
            _levelLoader = levelLoader;
            _door = door;
            _animal = animal;
            _key = key;
        }

        private void OnEnable()
        {
            _door.CompleteLevelHandler += LoadLevel;
            _animal.SetKey(_key);
        }

        private void OnDisable()
        {
            _door.CompleteLevelHandler -= LoadLevel;
        }

        private void LoadLevel()
        {
            if (!_animal.HasKey)
                return;
                
            _levelLoader.LoadNextLevel();
        }
    }
}