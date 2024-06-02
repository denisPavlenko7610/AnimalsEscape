using System.Collections.Generic;
using AnimalsEscape._Core.SceneManagement;
using AnimalsEscape.Interactive;
using RDTools.AutoAttach;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace AnimalsEscape._Core
{
    public class Game : MonoBehaviour
    {
        [FormerlySerializedAs("portals")] 
        [SerializeField, Attach(Attach.Scene)] List<Portal> _portals;
        
        LevelSystem levelSystem;
        Door _door;
        Key _key;
        Animal _animal;

        [Inject]
        public void Construct(LevelSystem levelSystem, Door door, Animal animal, Key key)
        {
            this.levelSystem = levelSystem;
            _door = door;
            _animal = animal;
            _key = key;
        }

        void OnEnable()
        {
            _animal.SetKey(_key);
            _door.CompleteLevelHandler += LoadNextLevel;
            foreach (var portal in _portals)
            {
                portal.OnPortalTriggerEnterHandler += _animal.MoveThroughPortal;
            }
        }

        void OnDisable()
        {
            _door.CompleteLevelHandler -= LoadNextLevel;
            foreach (var portal in _portals)
            {
                portal.OnPortalTriggerEnterHandler -= _animal.MoveThroughPortal;
            }
        }

        public void GameOver()
        {
            ReloadLevel();
        }

        void LoadNextLevel()
        {
            if (!_animal.HasKey)
                return;
                
            levelSystem.LoadNextLevel();
        }

        void ReloadLevel()
        {
            levelSystem.ReloadLevel();
        }
    }
}