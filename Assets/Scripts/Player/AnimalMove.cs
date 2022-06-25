using UnityEngine;

namespace AnimalsEscape
{
    public class AnimalMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed = 5.5f;
        [SerializeField] private float _rotationMultiplier = 2f;
        private float _rotationDegree = 360f;

        public Vector2 MoveInput { get; set; }

        private void FixedUpdate()
        {
            var input = new Vector3(MoveInput.x, 0, MoveInput.y);
            if (input == Vector3.zero)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }

            Move(input);
            Rotate(input);
        }

        private void Move(Vector3 input) => _rigidbody.velocity = input * _speed;

        private void Rotate(Vector3 input)
        {
            Quaternion targetRotation = Quaternion.LookRotation(input);
            targetRotation =
                Quaternion.RotateTowards(_rigidbody.rotation, targetRotation,
                    _rotationDegree * _rotationMultiplier * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(targetRotation);
        }
    }
}