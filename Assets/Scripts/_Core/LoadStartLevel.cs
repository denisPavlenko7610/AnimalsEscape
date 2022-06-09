using AnimalsEscape._Core.SceneManagement;
using UnityEngine;
using Zenject;

namespace AnimalsEscape._Core
{
    public class LoadStartLevel : MonoBehaviour
    {
        private LevelSystem _levelSystem;

        [Inject]
        public void Construct(LevelSystem levelSystem)
        {
            _levelSystem = levelSystem;
        }

        private void Start()
        {
            _levelSystem.LoadSavedOrNextLevel();
        }
    }
}
