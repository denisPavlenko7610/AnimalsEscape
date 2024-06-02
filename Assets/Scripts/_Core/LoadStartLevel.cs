using AnimalsEscape._Core.SceneManagement;
using UnityEngine;
using Zenject;

namespace AnimalsEscape._Core
{
    public class LoadStartLevel : MonoBehaviour
    {
        LevelSystem _levelSystem;

        [Inject]
        public void Construct(LevelSystem levelSystem)
        {
            _levelSystem = levelSystem;
        }

        void Start()
        {
            _levelSystem.LoadSavedOrNextLevel();
        }
    }
}
