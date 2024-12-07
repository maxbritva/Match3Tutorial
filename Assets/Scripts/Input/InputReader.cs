using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputReader : IDisposable
    {
        public event Action Click;
        private Inputs _inputs;
        private InputAction _positionAction;
        private InputAction _fireAction;

        private bool _isFire;
        
        public InputReader()
        {
            _inputs = new Inputs();
            _inputs.Player.Fire.performed += OnClick;
        }
        public void Dispose() => _inputs.Player.Fire.performed -= OnClick;

        public void EnableInputs(bool value)
        {
            if(value)
                _inputs.Enable();
            else
                _inputs.Disable();
        }

        public Vector2 Position() => _inputs.Player.Select.ReadValue<Vector2>();

        private void OnClick(InputAction.CallbackContext context)
        {
            Click?.Invoke();
        }
    }
}