using UnityEngine;
using UnityEngine.Pool;

namespace Gun.Bullet
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _timeToDestroy = 5f;

        private Rigidbody _rigidbody;
        private IObjectPool<Bullet> _pool;

        public Rigidbody Rigidbody => _rigidbody;
        public void SetPool(IObjectPool<Bullet> pool) => _pool = pool;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _timeToDestroy -= Time.deltaTime;

            if (_timeToDestroy <= 0f)
            {
                if (_pool != null)
                {
                    _timeToDestroy = 5f;
                    _pool.Release(this);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
