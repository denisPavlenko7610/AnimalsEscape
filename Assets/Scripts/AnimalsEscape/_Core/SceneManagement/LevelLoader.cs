using AnimalsEscape.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace AnimalsEscape.Core.SceneManagement
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private LevelsConfig LevelsConfig;
        private int _currentLevelIndex = -1;
        private LevelText _levelText;

        [Inject]
        private void Construct(LevelText levelText)
        {
            _levelText = levelText;
        }

        public void LoadNextLevel()
        {
            var nextLevelIndex = _currentLevelIndex + 1;
            if (nextLevelIndex != LevelsConfig.Levels.Count)
            {
                _currentLevelIndex = nextLevelIndex;
            }
            else
            {
                _currentLevelIndex = 0;
                nextLevelIndex = _currentLevelIndex;
            }

            _levelText.ShowLevel(nextLevelIndex + 1);
            SceneManager.LoadScene(LevelsConfig.Levels[nextLevelIndex].name);
        }
    }
}