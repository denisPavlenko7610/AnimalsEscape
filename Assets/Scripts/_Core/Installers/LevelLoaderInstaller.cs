using AnimalsEscape._Core.SceneManagement;
using Zenject;

namespace AnimalsEscape._Core.Installers
{
    public class LevelLoaderInstaller : MonoInstaller
    {
        public LevelSystem levelSystem;
        
        public override void InstallBindings()
        {
            Container.Bind<LevelSystem>().FromInstance(levelSystem).AsSingle().NonLazy();
        }
    }
}