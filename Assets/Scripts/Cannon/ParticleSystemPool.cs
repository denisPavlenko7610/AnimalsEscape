using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class ParticleSystemPool : MonoBehaviour
{
    ParticleFactory _particleFactory;

    [FormerlySerializedAs("typeEffect")]
    [SerializeField] Effect _typeEffect;
    
    [FormerlySerializedAs("poolSize")]
    [SerializeField] int _poolSize = 5;
    
    List<ParticleSystem> _pool = new(10);

    [Inject]
    public void Construct(ParticleFactory particleFactory)
    {
        _particleFactory = particleFactory;
    }

    void Awake()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            CreateNewParticle();
        }
    }

    void CreateNewParticle()
    {
        ParticleSystem newParticle = _particleFactory.SpawnEffect(_typeEffect);
        newParticle.gameObject.SetActive(false);
        _pool.Add(newParticle);
    }

    public ParticleSystem GetParticle()
    {
        foreach (ParticleSystem particle in _pool)
        {
            if (!particle.gameObject.activeInHierarchy)
            {
                particle.gameObject.SetActive(true);
                return particle;
            }
        }
        
        CreateNewParticle();
        return _pool[_pool.Count - 1];
    }
    
    public void SetParticle(Transform spawnPoint)
    {
        ParticleSystem particle = GetParticle();
        particle.transform.position = spawnPoint.position;
        particle.transform.rotation = spawnPoint.rotation;
        particle.GetComponent<ParticleSystemStoppedHandler>().onParticleStopped += ReleaseParticleSystem;
        particle.Play();
    }

    void ReleaseParticleSystem(ParticleSystem particle)
    {
        particle.GetComponent<ParticleSystemStoppedHandler>().onParticleStopped -= ReleaseParticleSystem;
        particle.Clear();
        particle.Stop();
        particle.gameObject.SetActive(false);
    }
}