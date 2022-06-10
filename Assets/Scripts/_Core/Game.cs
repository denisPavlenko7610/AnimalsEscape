using AnimalsEscape._Core.SceneManagement;
using AnimalsEscape.Interactive;
using UnityEngine;
using Zenject;

namespace AnimalsEscape._Core
{
    public class Game : MonoBehaviour
    {
        private LevelSystem levelSystem;
        private Door _door;
        private Key _key;
        private Animal _animal;

        [Inject]
        public void Construct(LevelSystem levelSystem, Door door, Animal animal, Key key)
        {
            this.levelSystem = levelSystem;
            _door = door;
            _animal = animal;
            _key = key;
        }

        private void OnEnable()
        {
            _door.CompleteLevelHandler += LoadNextLevel;
            _animal.SetKey(_key);
        }

        private void OnDisable()
        {
            _door.CompleteLevelHandler -= LoadNextLevel;
        }

        public void GameOver()
        {
            ReloadLevel();
        }

        private void LoadNextLevel()
        {
            if (!_animal.HasKey)
                return;
                
            levelSystem.LoadNextLevel();
        }

        private void ReloadLevel()
        {
            levelSystem.ReloadLevel();
        }
    }
}