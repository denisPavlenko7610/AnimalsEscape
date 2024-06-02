using AnimalsEscape.UI;
using UnityEngine;
using Zenject;

namespace AnimalsEscape.Core.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] LevelText _levelText;
        public override void InstallBindings()
        {
            Container.Bind<LevelText>().FromInstance(_levelText).AsSingle().NonLazy();
        }
    }
}
