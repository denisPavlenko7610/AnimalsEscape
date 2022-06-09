using System.Collections.Generic;
using AnimalsEscape.UI;
using Trisibo;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace AnimalsEscape._Core.SceneManagement
{
    public class LevelSystem : MonoBehaviour
    {
        [SerializeField] List<SceneField> _levels = new();
        private int _currentLevelIndex = -1;
        private LevelText _levelText;
        string key = "Level";

        [Inject]
        private void Construct(LevelText levelText)
        {
            _levelText = levelText;
        }

        public void SaveLevel(int level)
        {
            PlayerPrefs.SetInt(key, level);
        }

        public void LoadSavedOrNextLevel()
        {
            if (PlayerPrefs.HasKey(key))
            {
                var levelIndex = PlayerPrefs.GetInt(key);
                LoadLevel(levelIndex);
            }
            else
            {
                LoadNextLevel();
                print("Key isn`t found");
            }
        }
        
        public void LoadNextLevel()
        {
            var levelIndex = _currentLevelIndex + 1;
            SaveLevel(levelIndex);
            Load(levelIndex);
        }

        private void LoadLevel(int level)
        {
            Load(level);
        }
        
        private void Load(int levelIndex)
        {
            if (levelIndex != _levels.Count)
            {
                _currentLevelIndex = levelIndex;
            }
            else
            {
                _currentLevelIndex = 0;
                levelIndex = _currentLevelIndex;
            }

            _levelText.ShowLevel(levelIndex + 1);
            SceneManager.LoadScene(_levels[levelIndex].BuildIndex);
        }
    }
}