using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AnimalsEscape
{
    public class AnimalInput : MonoBehaviour
    {
        public Action<Vector2> input;
        Vector2 _moveVector;
        Input_Actions _inputActions;

        public bool IsMoving { get; set; } = true;

        void Awake()
        {
            _inputActions = new Input_Actions();
        }

        void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Player.Move.performed += UpdateInput;
            _inputActions.Player.Move.canceled += CanceledInput;
        }

        void OnDisable()
        {
            _inputActions?.Disable();
            
            if (_inputActions != null)
                _inputActions.Player.Move.performed -= UpdateInput;
            
            if (_inputActions != null)
                _inputActions.Player.Move.canceled -= CanceledInput;
        }

        void UpdateInput(InputAction.CallbackContext ctx)
        {
            _moveVector = _inputActions.Player.Move.ReadValue<Vector2>();
            input?.Invoke(_moveVector);
        }

        void CanceledInput(InputAction.CallbackContext ctx)
        {
            _moveVector = Vector2.zero;
            input?.Invoke(_moveVector);
        }
    }
}
