using AnimalsEscape.UI;
using UnityEngine;
using Zenject;

namespace AnimalsEscape.Core.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] LevelText _levelText;
        [SerializeField] HealthUI _healthUI;

        public override void InstallBindings()
        {
            Debug.Log($"Binding HealthUI: {_healthUI != null}");

            Container.Bind<LevelText>().FromInstance(_levelText).AsSingle().NonLazy();
            Container.Bind<HealthUI>().FromInstance(_healthUI).AsSingle().NonLazy();
            
        }
    }
}
