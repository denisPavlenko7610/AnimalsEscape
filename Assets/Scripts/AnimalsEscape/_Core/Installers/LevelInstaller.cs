using System;
using Zenject;

namespace AnimalsEscape._Core.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public Door Door;

        private void OnValidate()
        {
            if (!Door)
            {
                Door = FindObjectOfType<Door>();
            }
        }

        public override void InstallBindings()
        {
            Container.Bind<Door>().FromInstance(Door).AsSingle();
        }
    }
}