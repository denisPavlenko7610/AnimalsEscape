using AnimalsEscape.Player;
using UnityEngine;
using Zenject;

namespace AnimalsEscape.Core.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private AnimalFactory _animalFactory;

        private void OnValidate()
        {
            if (_animalFactory == null)
            {
                _animalFactory = FindObjectOfType<AnimalFactory>();
            }
        }

        public override void InstallBindings()
        {
            var newAnimal = _animalFactory.Create();
            Container.Bind<Animal>().FromInstance(newAnimal).AsSingle().NonLazy();
        }
    }
}