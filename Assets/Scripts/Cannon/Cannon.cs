using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Cannon
{
    public class Cannon : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private float _fireSpeed;
        [SerializeField] private float _delay;

        public ObjectPool<Bullet> Pool { get; set; }
        private List<Bullet> _instantiatedBullets = new();
        private float _startDelay;

        private void Awake()
        {
            Pool = new ObjectPool<Bullet>
                (SpawnBullet, OnTakeBulletFromPool, OnReturnBulletFromPool);
            _startDelay = _delay;
        }

        private void OnDisable()
        {
            foreach (var instantiatedBullet in _instantiatedBullets)
            {
                instantiatedBullet.onTriggeredBullet -= BulletRelease;
            }
        }

        private void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            _delay -= Time.deltaTime;

            if (_delay <= 0f)
            {
                _delay = _startDelay;
                GetBullet();
            }
        }

        private void GetBullet() => Pool.Get();

        private Bullet SpawnBullet()
        {
            var newBullet = Instantiate(_bulletPrefab, _spawnPoint.position, Quaternion.identity, transform);
            _instantiatedBullets.Add(newBullet);
            newBullet.onTriggeredBullet += BulletRelease;
            return newBullet;
        }

        private void OnTakeBulletFromPool(Bullet bullet) => bullet.InitBullet(bullet, _fireSpeed);

        private void OnReturnBulletFromPool(Bullet bullet) => bullet.DisableBullet(bullet, _spawnPoint);

        private void BulletRelease(Bullet bullet) => Pool.Release(bullet);
    }
}