using System.Collections.Generic;
using AnimalsEscape._Core.SceneManagement;
using AnimalsEscape.Interactive;
using System.Linq;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;

namespace AnimalsEscape._Core
{
    public class Game : MonoBehaviour
    {
        int _deathCounter;
        int _adsTreshold = 5;
        bool _deathSoundIsPlaying;

        List<Portal> _portals = new();

        LevelSystem _levelSystem;
        Door _door;
        Key _key;
        Animal _animal;
        Ads _ads;
        AnimalSound _animalSound;

        [Inject]
        public void Construct(Ads ads, LevelSystem levelSystem, Door door, Animal animal, Key key, AnimalSound animalSound)
        {
            _ads = ads;
            _levelSystem = levelSystem;
            _door = door;
            _animal = animal;
            _key = key;
            _animalSound = animalSound;
        }

        void OnEnable()
        {
            _portals = FindObjectsByType<Portal>(FindObjectsSortMode.None).ToList();

            _animal.SetKey(_key);
            _door.CompleteLevelHandler += LoadNextLevel;
            _animal.OnBulletCollision += GameOver;

            foreach (var portal in _portals)
            {
                portal.OnPortalTriggerEnterHandler += _animal.MoveThroughPortal;
            }

            _ads.onAdClosed += ReloadLevel;
        }

        void OnDisable()
        {
            _door.CompleteLevelHandler -= LoadNextLevel;
            _animal.OnBulletCollision -= GameOver;
            foreach (var portal in _portals)
            {
                portal.OnPortalTriggerEnterHandler -= _animal.MoveThroughPortal;
            }

            _ads.onAdClosed -= ReloadLevel;
        }

        void Start()
        {
            LogSettings();

            Debug.Log("Death " + _deathCounter);
            Debug.Log("Ads Threshold " + _adsTreshold);
            Debug.Log("Start Sound");

            _animalSound.PlaySoundAtStart();
            _deathSoundIsPlaying = false;
        }

        public async void GameOver()
        {
            if (_deathCounter == _adsTreshold)
            {
                await DeathSound();
                _ads.ShowInterstitialAd();
                _deathCounter = 0;
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

        async void ReloadLevel()
        {
            await DeathSound();

            _levelSystem.ReloadLevel();
            Debug.Log("End Death Sound");
            _deathCounter++;
        }

        async UniTask DeathSound()
        {
            if (!_deathSoundIsPlaying)
            {
                await _animalSound.PlayDeathSound();
                _deathSoundIsPlaying = true;
            }
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
