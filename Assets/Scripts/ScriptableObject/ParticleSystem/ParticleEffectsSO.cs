using System;
using System.Collections.Generic;
using UnityEngine;

public enum Effect
{
    CannonFireParticle,
    BulletCollisionParticle,
    BombWickParticle,
    BombExplosionParticle
}

[Serializable]
public struct Particles
{
    public ParticleSystem _particleEffect;
    public Effect _effectState;
}

[CreateAssetMenu(fileName = "ParticleEffects", menuName = "Gameplay/ScriptableObjects/Particles")]
public class ParticleEffectsSO : ScriptableObject
{
    public List<Particles> Particles;
}
    