using AnimalsEscape._Core.SceneManagement;
using UnityEngine;
using Zenject;

namespace AnimalsEscape._Core.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] LevelSystem _levelSystem;
        [SerializeField] Ads _ads;
        [SerializeField] RewardAd _rewardedAd;
        [SerializeField] ParticleFactory _particleFactory;
        
        public override void InstallBindings()
        {
            Container.Bind<LevelSystem>().FromInstance(_levelSystem).AsSingle().NonLazy();
            Container.Bind<Ads>().FromInstance(_ads).AsSingle().NonLazy();
            Container.Bind<RewardAd>().FromInstance(_rewardedAd).AsSingle().NonLazy();
            Container.Bind<ParticleFactory>().FromInstance(_particleFactory).AsSingle().NonLazy();
        }
    }
}