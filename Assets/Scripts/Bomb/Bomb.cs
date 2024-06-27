using AnimalsEscape._Core;
using AnimalsEscape;
using RDTools.AutoAttach;
using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class Bomb : MonoBehaviour
{
    [SerializeField] float _radius;
    [SerializeField] float _timeToExplode = 2f;
    [SerializeField] Transform _spawnPosition;

    [SerializeField, Attach(Attach.Scene)] Game _game;

    bool _explosionDone;

    ParticleFactory _particleFactory;

    [Inject]
    public void Construct(ParticleFactory particleFactory)
    {
        _particleFactory = particleFactory;
    }

    public void ExplodeWithDelay()
    {
        if (_explosionDone) return;
            _explosionDone = true;

        Invoke("Explode", _timeToExplode);
        GetComponent<Renderer>().material.color = Color.red;
        _particleFactory.SpawnEffect(Effect.BombWickParticle, _spawnPosition.position, _spawnPosition.rotation, gameObject.transform);
    }

    void Explode()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
        HashSet<GameObject> processedObjects = new HashSet<GameObject>();
        foreach (var overlappedCollider in overlappedColliders)
        {
            GameObject obj = overlappedCollider.gameObject;

            if (!processedObjects.Add(obj))
                continue;

            if (overlappedCollider.TryGetComponent(out AnimalHealth animal))
            {
                animal.DecreaseHealth();
                _game.GameOver();
            }

            if (overlappedCollider.TryGetComponent(out Bomb secondBomb))
            {
                if (Vector3.Distance(transform.position, secondBomb.transform.position) < _radius / 2f)
                {
                    secondBomb.ExplodeWithDelay();
                }
            }
        }
        
        //TODO: change to pool effect
        gameObject.SetActive(false);
        Destroy(gameObject, Random.Range(1f, 5f));
        _particleFactory.SpawnEffect(Effect.BombExplosionParticle, _spawnPosition.position, _spawnPosition.rotation, null);
        //
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Animal animal))
            ExplodeWithDelay();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius / 2f);
    }
#endif

}
