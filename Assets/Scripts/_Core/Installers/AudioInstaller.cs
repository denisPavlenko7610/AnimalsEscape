using AnimalsEscape._Core;
using UnityEngine;
using Zenject;

public class AudioInstaller : MonoInstaller
{
    [SerializeField] AnimalSound _animalSound;

    public override void InstallBindings()
    {
        Container.Bind<AnimalSound>().FromInstance(_animalSound).AsSingle().NonLazy();
    }
}