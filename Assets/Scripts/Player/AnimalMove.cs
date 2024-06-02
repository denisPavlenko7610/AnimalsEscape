using UnityEngine;

namespace AnimalsEscape
{
    public class AnimalMove : MonoBehaviour
    {
        [SerializeField] Rigidbody _rigidbody;
        [SerializeField] float _speed = 5.5f;
        [SerializeField] float _rotationMultiplier = 2f;
        float _rotationDegree = 360f;

        public Vector2 MoveInput { get; set; }

        void FixedUpdate()
        {
            var input = new Vector3(MoveInput.x, 0, MoveInput.y);
            if (input == Vector3.zero)
            {
                _rigidbody.linearVelocity = Vector3.zero;
                return;
            }

            Move(input);
            Rotate(input);
        }

        void Move(Vector3 input) => _rigidbody.linearVelocity = input * _speed;

        void Rotate(Vector3 input)
        {
            Quaternion targetRotation = Quaternion.LookRotation(input);
            targetRotation =
                Quaternion.RotateTowards(_rigidbody.rotation, targetRotation,
                    _rotationDegree * _rotationMultiplier * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(targetRotation);
        }
    }
}