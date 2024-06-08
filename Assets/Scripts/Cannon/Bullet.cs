using System;
using RDTools.AutoAttach;
using UnityEngine;
using AnimalsEscape;

namespace Cannon
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField, Attach] Rigidbody _rigidbody;
        [SerializeField] LayerMask _obstacleLayerMask = 0;

        public event Action<Bullet> OnTriggeredBullet;
        public Rigidbody Rigidbody => _rigidbody;

        void OnTriggerEnter(Collider other)
        {
            if ((_obstacleLayerMask & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
                OnTriggeredBullet?.Invoke(this);
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
