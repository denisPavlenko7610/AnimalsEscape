using System.Collections.Generic;
using AnimalsEscape._Core;
using AnimalsEscape.Factories;
using RDTools.AutoAttach;
using UnityEngine;
using Zenject;

namespace AnimalsEscape.Core.Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField, Attach(Attach.Scene)] Game _game;
        [SerializeField, Attach(Attach.Scene)] EnemyFactory _enemyFactory;
        [SerializeField, Attach(Attach.Scene)] Waypoints _waypoints;

        List<Enemy> _enemies = new();

        public override void InstallBindings()
        {
            Enemy enemy = _enemyFactory.Create();
            enemy.FieldOfView.OnScannerAndTriggerReactHandler += _game.GameOver;
            enemy.Init(_waypoints?.Points);
            _enemies.Add(enemy);
        }

        void OnDestroy()
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.FieldOfView.OnScannerAndTriggerReactHandler -= _game.GameOver;
            }
        }
    }
}