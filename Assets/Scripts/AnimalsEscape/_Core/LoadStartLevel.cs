using AnimalsEscape.Core.SceneManagement;
using UnityEngine;
using Zenject;

namespace AnimalsEscape._Core
{
    public class LoadStartLevel : MonoBehaviour
    {
        private LevelLoader _levelLoader;

        [Inject]
        public void Construct(LevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
        }

        private void Start()
        {
            _levelLoader.LoadNextLevel();
        }
    }
}
