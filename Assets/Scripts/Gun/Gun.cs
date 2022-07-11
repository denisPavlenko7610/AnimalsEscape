using UnityEngine;
using UnityEngine.Pool;

namespace Gun
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Bullet.Bullet _bulletPrefab;
        [SerializeField] private float _fireSpeed = 1.5f;

        private ObjectPool<Bullet.Bullet> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<Bullet.Bullet>(SpawnBullet, OnTakeBulletFromPool, OnReturnBulletFromPool);
        }

        private void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            _fireSpeed -= Time.deltaTime;

            if (_fireSpeed < 0f)
            {
                _fireSpeed = 1.5f;
                GetBullet();
            }
        }

        private Vector3 RayToPoint()
        {
            var startRay = transform.position;
            var direction = transform.right * 10f;

            var ray = new Ray(startRay, direction);

            return Physics.Raycast(ray, out var hit, 10f) ? hit.collider.transform.position : Vector3.zero;
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
            bullet.Rigidbody.MovePosition(RayToPoint());
        }
        
        private void OnReturnBulletFromPool(Bullet.Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            bullet.transform.position = _spawnPoint.position;
        }

        private void GetBullet()
        {
            _pool.Get();
        }
    }
}