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
        [SerializeField, Attach(Attach.Scene)] private Game _game;
        [SerializeField, Attach(Attach.Scene)] private EnemyFactory _enemyFactory;
        [SerializeField, Attach(Attach.Scene)] private List<Transform> _waypoints = new();

        public override void InstallBindings()
        {
            var newEnemy = _enemyFactory.Create();
            newEnemy.FieldOfView.OnScannerReactHandler += _game.GameOver;
            newEnemy.Waypoints = _waypoints;
            Container.Bind<Enemy>().FromInstance(newEnemy).AsSingle().NonLazy();
        }
    }
}