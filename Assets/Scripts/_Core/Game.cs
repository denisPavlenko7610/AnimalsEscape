using System.Collections.Generic;
using AnimalsEscape._Core.SceneManagement;
using AnimalsEscape.Interactive;
using System.Linq;
using UnityEngine;
using Zenject;

namespace AnimalsEscape._Core
{
    public class Game : MonoBehaviour
    {
        List<Portal> _portals = new();
        
        LevelSystem _levelSystem;
        Door _door;
        Key _key;
        Animal _animal;
        Ads _ads;

        [Inject]
        public void Construct(Ads ads, LevelSystem levelSystem, Door door, Animal animal, Key key)
        {
            _ads = ads;
            _levelSystem = levelSystem;
            _door = door;
            _animal = animal;
            _key = key;
        }

        void OnEnable()
        {
            _portals = FindObjectsByType<Portal>(FindObjectsSortMode.None).ToList();
            
            _animal.SetKey(_key);
            _door.CompleteLevelHandler += LoadNextLevel;
            foreach (var portal in _portals)
            {
                portal.OnPortalTriggerEnterHandler += _animal.MoveThroughPortal;
            }

            _ads.onAdClosed += ReloadLevel;
        }

        void OnDisable()
        {
            _door.CompleteLevelHandler -= LoadNextLevel;
            foreach (var portal in _portals)
            {
                portal.OnPortalTriggerEnterHandler -= _animal.MoveThroughPortal;
            }
            
            _ads.onAdClosed -= ReloadLevel;
        }

        void Start()
        {
            LogSettings();
        }

        public void GameOver()
        {
            if (_levelSystem.GetCurrentLevelNumber() % 2 == 0)
            {
                _ads.ShowInterstitialAd();
            }
            else
            {
                ReloadLevel();
            }
        }

        void LoadNextLevel()
        {
            if (!_animal.HasKey)
                return;
                
            _levelSystem.LoadNextLevel();
        }

        void ReloadLevel()
        {
            _levelSystem.ReloadLevel();
        }
        
        void LogSettings()
        {

#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
#else
            Debug.logger.logEnabled = false;
#endif
        }
    }
}