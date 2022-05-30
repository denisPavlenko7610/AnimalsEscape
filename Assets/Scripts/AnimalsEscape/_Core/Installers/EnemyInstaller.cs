using AnimalsEscape.Factories;
using AnimalsEscape.Player;
using UnityEngine;
using Zenject;

namespace AnimalsEscape.Core.Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemyFactory _enemyFactory;

        private void OnValidate()
        {
            if (_enemyFactory == null)
            {
                _enemyFactory = FindObjectOfType<EnemyFactory>();
            }
        }

        public override void InstallBindings()
        {
            var newEnemy = _enemyFactory.Create();
            Container.Bind<Enemy>().FromInstance(newEnemy).AsSingle().NonLazy();
        }
    }
}