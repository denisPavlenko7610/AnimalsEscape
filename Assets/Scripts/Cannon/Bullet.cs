using System;
using RDTools.AutoAttach;
using UnityEngine;
using Zenject;

namespace Cannon
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField, Attach] Rigidbody _rigidbody;
        [SerializeField] LayerMask _obstacleLayerMask = 0;

        public event Action<Bullet> OnTriggeredBullet;
        public Rigidbody Rigidbody => _rigidbody;

        ParticleFactory _particleFactory;

        [Inject]
        public void Construct(ParticleFactory particleFactory)
        {
            _particleFactory = particleFactory;
        }

        void OnTriggerEnter(Collider other)
        {
            if ((_obstacleLayerMask & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
                OnTriggeredBullet?.Invoke(this);

            if (other.gameObject.TryGetComponent(out AnimalHealth animal))
                _particleFactory.SpawnEffect(Effect.BulletCollisionParticle, transform.position, transform.rotation, null);
        }

        public void InitBullet(Bullet bullet, float fireSpeed)
        {
            bullet.gameObject.SetActive(true);
            bullet.Rigidbody.isKinematic = false;
            bullet.Rigidbody.velocity = Vector3.right * fireSpeed;
        }

        public void DisableBullet(Bullet bullet, Transform spawnPoint)
        {
            bullet.gameObject.SetActive(false);
            bullet.Rigidbody.isKinematic = true;
            bullet.transform.position = spawnPoint.position;
        }
    }
}
