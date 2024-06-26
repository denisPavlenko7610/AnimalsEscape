using AnimalsEscape._Core;
using AnimalsEscape;
using RDTools.AutoAttach;
using UnityEngine;
using System.Collections.Generic;

public class Bomb : MonoBehaviour
{
    [SerializeField] float _radius;
    [SerializeField] GameObject _explosionEffect;
    [SerializeField] ParticleSystem _activatedBombEffect;

    [SerializeField, Attach(Attach.Scene)] Game _game;
    [SerializeField] float _timeToExplode = 2f;

    bool _explosionDone;

    public void ExplodeWithDelay()
    {
        if (_explosionDone) return;
        _explosionDone = true;

        Invoke("Explode", _timeToExplode);
        GetComponent<Renderer>().material.color = Color.red;
        _activatedBombEffect.Play();
    }

    void Explode()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
        HashSet<GameObject> processedObjects = new HashSet<GameObject>();
        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            GameObject obj = overlappedColliders[i].gameObject;

            if (processedObjects.Contains(obj))
                continue;

            processedObjects.Add(obj);

            if (overlappedColliders[i].TryGetComponent(out AnimalHealth animal))
            {
                animal.DecreaseHealth();
                _game.GameOver();
            }

            if (overlappedColliders[i].TryGetComponent(out Bomb secondBomb))
            {
                if (Vector3.Distance(transform.position, secondBomb.transform.position) < _radius / 2f)
                {
                    secondBomb.ExplodeWithDelay();
                }
            }
        }
        
        Destroy(gameObject);
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);//////////////
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
