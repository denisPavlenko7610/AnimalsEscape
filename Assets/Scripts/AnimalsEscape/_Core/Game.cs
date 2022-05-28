using AnimalsEscape.Core.SceneManagement;
using UnityEngine;
using Zenject;

namespace AnimalsEscape._Core
{
    public class Game : MonoBehaviour
    {
        private LevelLoader _levelLoader;
        private Door _door;

        [Inject]
        public void Construct(LevelLoader levelLoader, Door door)
        {
            _levelLoader = levelLoader;
            _door = door;
        }

        private void OnEnable()
        {
            _door.CompleteLevelHandler += LoadLevel;
        }

        private void OnDisable()
        {
            _door.CompleteLevelHandler -= LoadLevel;
        }

        private void LoadLevel()
        {
            _levelLoader.LoadNextLevel();
        }
    }
}