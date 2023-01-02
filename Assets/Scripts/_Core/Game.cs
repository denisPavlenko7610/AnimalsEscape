using System.Collections.Generic;
using AnimalsEscape._Core.SceneManagement;
using AnimalsEscape.Interactive;
using RDTools.AutoAttach;
using UnityEngine;
using Zenject;

namespace AnimalsEscape._Core
{
    public class Game : MonoBehaviour
    {
        [SerializeField, Attach(Attach.Scene)] private List<Portal> portals;
        private LevelSystem levelSystem;
        private Door _door;
        private Key _key;
        private Animal _animal;

        [Inject]
        public void Construct(LevelSystem levelSystem, Door door, Animal animal, Key key)
        {
            this.levelSystem = levelSystem;
            _door = door;
            _animal = animal;
            _key = key;
        }

        private void OnEnable()
        {
            _animal.SetKey(_key);
            _door.CompleteLevelHandler += LoadNextLevel;
            foreach (var portal in portals)
            {
                portal.OnPortalTriggerEnterHandler += _animal.MoveThroughPortal;
            }
        }

        private void OnDisable()
        {
            _door.CompleteLevelHandler -= LoadNextLevel;
            foreach (var portal in portals)
            {
                portal.OnPortalTriggerEnterHandler -= _animal.MoveThroughPortal;
            }
        }

        public void GameOver()
        {
            ReloadLevel();
        }

        private void LoadNextLevel()
        {
            if (!_animal.HasKey)
                return;
                
            levelSystem.LoadNextLevel();
        }

        private void ReloadLevel()
        {
            levelSystem.ReloadLevel();
        }
    }
}