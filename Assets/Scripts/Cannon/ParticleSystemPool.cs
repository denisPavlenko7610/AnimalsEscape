using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ParticleSystemPool : MonoBehaviour
{
    ParticleFactory _particleFactory;

    [Inject]
    public void Construct(ParticleFactory particleFactory)
    {
        _particleFactory = particleFactory;
    }

    [SerializeField] private Effect typeEffect;
    [SerializeField] private int poolSize = 5;
    private List<ParticleSystem> pool; 

    void Awake()
    {
        // Initialize the pool
        pool = new List<ParticleSystem>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewParticleSystem();
        }
    }

    private void CreateNewParticleSystem()
    {
        ParticleSystem newParticle = _particleFactory.SpawnEffect(typeEffect);
        newParticle.gameObject.SetActive(false);
        newParticle.GetComponent<ParticleSystemStoppedHandler>().Init(this);
        pool.Add(newParticle);
    }


    public ParticleSystem GetParticleSystem()
    {
        foreach (ParticleSystem ps in pool)
        {
            if (!ps.gameObject.activeInHierarchy)
            {
                ps.gameObject.SetActive(true);
                return ps;
            }
        }
        CreateNewParticleSystem();
        return pool[pool.Count - 1];
    }
    
    public void ReleaseParticleSystem(ParticleSystem ps)
    {
        ps.Clear();
        ps.Stop();
        ps.gameObject.SetActive(false);
    }
    
    public void SetParticle(Transform spawnPoint)
    {
        ParticleSystem ps = GetParticleSystem();
        ps.transform.position = spawnPoint.position;
        ps.transform.rotation = spawnPoint.rotation;
        ps.Play();
    }
}
