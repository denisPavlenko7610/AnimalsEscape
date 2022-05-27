using AnimalsEscape.Core.SceneManagement;
using AnimalsEscape.Utils;
using UnityEngine;
using Zenject;

namespace AnimalsEscape
{
    public class Door : MonoBehaviour
    {
        private LevelLoader LevelLoader;

        [Inject]
        public void Construct(LevelLoader LevelLoader)
        {
            this.LevelLoader = LevelLoader;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.AnimalTag))
            {
                LevelLoader.LoadNextLevel();
            }
        }
    }
}
