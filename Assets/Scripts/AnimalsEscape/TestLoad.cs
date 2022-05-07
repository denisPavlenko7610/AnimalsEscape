using AnimalsEscape.Core.SceneManagement;
using UnityEngine;
using Zenject;

namespace AnimalsEscape
{
    public class TestLoad : MonoBehaviour
    {
        private LevelLoader LevelLoader;
        private float time = 5f;

        [Inject]
        public void Construct(LevelLoader LevelLoader)
        {
            this.LevelLoader = LevelLoader;
        }

        void Update()
        {
            time -= Time.deltaTime;
            
            if (time < 0)
            {
                time = 5f;
                LevelLoader.LoadNextLevel();
            }
        }
    }
}