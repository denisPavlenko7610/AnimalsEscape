using UnityEngine;
using UnityEngine.Pool;

namespace Gun
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Bullet.Bullet _bulletPrefab;
        [SerializeField] private float _fireSpeed;
        [SerializeField] private float _delay;

        private ObjectPool<Bullet.Bullet> _pool;
        private float _startDelay;

        private void Awake()
        {
            _pool = new ObjectPool<Bullet.Bullet>
                (SpawnBullet, OnTakeBulletFromPool, OnReturnBulletFromPool);
        }

        private void Start()
        {
            _startDelay = _delay;
        }

        private void Update()
        {
            Shoot();
        }

        private Bullet.Bullet SpawnBullet()
        {
            var newBullet = Instantiate(_bulletPrefab, _spawnPoint.position, Quaternion.identity, transform);
            newBullet.SetPool(_pool);
            return newBullet;
        }

        private void OnTakeBulletFromPool(Bullet.Bullet bullet)
        {
            bullet.gameObject.SetActive(true);
            bullet.Rigidbody.isKinematic = false;
            bullet.Rigidbody.velocity = Vector3.right * _fireSpeed;
        }

        private void OnReturnBulletFromPool(Bullet.Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            bullet.Rigidbody.isKinematic = true;
            bullet.transform.position = _spawnPoint.position;
        }

        private void GetBullet()
        {
            _pool.Get();
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
    }
}