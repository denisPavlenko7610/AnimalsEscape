using UnityEngine;
using UnityEngine.Pool;

namespace Gun.Bullet
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        private IObjectPool<Bullet> _pool;
        private Rigidbody _rigidbody;

        public Rigidbody Rigidbody => _rigidbody;
        public void SetPool(IObjectPool<Bullet> pool) => _pool = pool;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 7)
                _pool.Release(this);
        }
    }
}
