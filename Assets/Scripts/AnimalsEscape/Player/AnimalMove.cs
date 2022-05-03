using UnityEngine;

namespace AnimalsEscape.Animal
{
    public class AnimalMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed = 7f;
        [SerializeField] private float _rotationMultiplier = 1.2f;
        private float _rotationDegree = 360f;

        public Vector2 MoveInput { get; set; }

        private void FixedUpdate()
        {
            if (MoveInput.magnitude > 0)
            {
                _rigidbody.isKinematic = false;
                var input = new Vector3(MoveInput.x, 0, MoveInput.y);
                Move(input);
                Rotate(input);
            }
            else
            {
                if (_rigidbody.isKinematic)
                    return;

                _rigidbody.isKinematic = true;
            }
        }
        private void Move(Vector3 input) =>
            _rigidbody.MovePosition(_rigidbody.position + input * (Time.fixedDeltaTime * _speed));

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