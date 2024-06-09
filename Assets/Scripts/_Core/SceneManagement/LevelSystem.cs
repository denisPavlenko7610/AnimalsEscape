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
        
        int _currentLevelIndex = -1;
        LevelText _levelText;
        const string key = "Level";
        bool _shouldSave = true;

        [Inject]
        void Construct(LevelText levelText)
        {
            _levelText = levelText;
        }
        
        public void init(bool shouldSave)
        {
            _shouldSave = shouldSave;
        }

        public void SaveLevel(int level)
        {
            PlayerPrefs.SetInt(key, level);
        }

        public void LoadSavedOrNextLevel()
        {
            if (PlayerPrefs.HasKey(key))
            {
                int levelIndex = PlayerPrefs.GetInt(key);
                Load(levelIndex);
            }
            else
            {
                LoadNextLevel();
                print("Key isn`t found");
            }
        }
        
        public void LoadNextLevel()
        {
            int levelIndex = _currentLevelIndex + 1;
            if (_shouldSave)
            {
                SaveLevel(levelIndex);
            }
            Load(levelIndex);
        }

        public int GetCurrentLevelNumber() => SceneManager.GetActiveScene().buildIndex;

        public void ReloadLevel() => SceneManager.LoadScene(GetCurrentLevelNumber());

        void Load(int levelIndex)
        {
            if (levelIndex < _levels.Count)
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