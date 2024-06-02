using AnimalsEscape.Player;
using UnityEngine;
using Zenject;

namespace AnimalsEscape.Core.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] AnimalFactory _animalFactory;

        private void OnValidate()
        {
            if (!_animalFactory)
            {
                _animalFactory = FindAnyObjectByType<AnimalFactory>();
            }
        }

        public override void InstallBindings()
        {
            var newAnimal = _animalFactory.Create();
            Container.Bind<Animal>().FromInstance(newAnimal).AsSingle().NonLazy();
        }
    }
}