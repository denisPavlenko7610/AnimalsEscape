using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AnimalsEscape.Player
{
    public class AnimalInput : MonoBehaviour
    {
        public Action<Vector2> input;
        private Vector2 _moveVector;
        private InputActions _inputActions;

        private void OnEnable()
        {
            _inputActions = new InputActions();
            _inputActions.Enable();
            _inputActions.Player.Move.performed += UpdateInput;
            _inputActions.Player.Move.canceled += CanceledInput;
        }

        private void OnDisable()
        {
            _inputActions?.Disable();
            if (_inputActions != null) _inputActions.Player.Move.performed -= UpdateInput;
            if (_inputActions != null) _inputActions.Player.Move.canceled -= CanceledInput;
        }

        private void UpdateInput(InputAction.CallbackContext ctx)
        {
            _moveVector = _inputActions.Player.Move.ReadValue<Vector2>();
            input?.Invoke(_moveVector);
        }

        private void CanceledInput(InputAction.CallbackContext ctx)
        {
            _moveVector = Vector2.zero;
            input?.Invoke(_moveVector);
        }
    }
}
