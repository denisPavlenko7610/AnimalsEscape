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


        public void LoadSavedOrNextLevel()
        {
            if (PlayerPrefs.HasKey(key))
            {
                _currentLevelIndex = PlayerPrefs.GetInt(key);
                Load();
            }
            else
            {
                LoadNextLevel();
                print("Key isn`t found");
            }
        }
        
        public void LoadNextLevel()
        {
            _currentLevelIndex++;
            if (_currentLevelIndex != 0 && _shouldSave)
            {
                SaveLevel();
            }
            Load();
        }
        
        public void ReloadLevel() => SceneManager.LoadScene(GetCurrentLevelNumber());
        
        int GetCurrentLevelNumber() => SceneManager.GetActiveScene().buildIndex;
        
        void SaveLevel() => PlayerPrefs.SetInt(key, _currentLevelIndex);

        void Load()
        {
            if (_currentLevelIndex >= _levels.Count)
            {
                _currentLevelIndex = 0;
            }

            _levelText.ShowLevel(_currentLevelIndex + 1);
            SceneManager.LoadScene(_levels[_currentLevelIndex].BuildIndex);
        }
    }
}