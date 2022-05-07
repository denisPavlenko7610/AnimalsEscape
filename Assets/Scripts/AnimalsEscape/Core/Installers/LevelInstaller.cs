using AnimalsEscape.Core.SceneManagement;
using Zenject;

namespace AnimalsEscape.Core.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public LevelLoader LevelLoader;

        public override void InstallBindings()
        {
            Container.Bind<LevelLoader>().FromInstance(LevelLoader).AsSingle().NonLazy();
        }
    }
}