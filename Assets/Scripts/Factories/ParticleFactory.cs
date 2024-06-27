using System.Collections.Generic;
using UnityEngine;

public class ParticleFactory : MonoBehaviour
{
    [SerializeField] ParticleEffectsSO _particleEffects;
    Dictionary<Effect, ParticleSystem> _effectDictionary;

    private void Awake()
    {
        _effectDictionary = new Dictionary<Effect, ParticleSystem>();

        foreach (var particle in _particleEffects.Particles)
            _effectDictionary[particle._effectState] = particle._particleEffect;
    }

    public void SpawnEffect(Effect effect, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (_effectDictionary.TryGetValue(effect, out ParticleSystem particleEffect))
            Instantiate(particleEffect, position, rotation, parent);



    }
}
