using System.Collections.Generic;
using AnimalsEscape._Core;
using AnimalsEscape.Factories;
using UnityEngine;
using Zenject;

namespace AnimalsEscape.Core.Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private Game _game;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private List<Transform> _waypoints = new();

        private void OnValidate()
        {
            if (_enemyFactory == null)
                _enemyFactory = FindObjectOfType<EnemyFactory>();

            if (_waypoints.Count == 0)
                FindWaypoints();

            if (!_game)
            {
                _game = FindObjectOfType<Game>();
            }
        }

        private void FindWaypoints()
        {
            _waypoints = FindObjectOfType<Waypoints>().Points;
        }

        public override void InstallBindings()
        {
            var newEnemy = _enemyFactory.Create();
            newEnemy.FieldOfView.OnScannerReactHandler += _game.GameOver;
            newEnemy.Waypoints = _waypoints;
            Container.Bind<Enemy>().FromInstance(newEnemy).AsSingle().NonLazy();
        }
        
        [ContextMenu("ReInit")]
        private void ReInit()
        {
            FindWaypoints();
        }
    }
}