using UnityEngine;

[CreateAssetMenu(fileName = "ParticleEffects", menuName = "Gameplay/Particles")]
public class ParticleEffects : ScriptableObject
{
    public ParticleSystem CannonFireParticle;

    public ParticleSystem BulletCollisionParticle;

    public ParticleSystem BombWickParticle;

    public ParticleSystem BombExplosionParticle;

}
