using AnimalsEscape.Interactive;
using Zenject;

namespace AnimalsEscape._Core.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public Door Door;
        public Key Key;

        private void OnValidate()
        {
            if (!Door)
            {
                Door = FindObjectOfType<Door>();
            }

            if (!Key)
            {
                Key = FindObjectOfType<Key>();
            }
        }

        public override void InstallBindings()
        {
            Container.Bind<Door>().FromInstance(Door).AsSingle();
            Container.Bind<Key>().FromInstance(Key).AsSingle();
        }
    }
}