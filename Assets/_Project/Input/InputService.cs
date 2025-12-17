using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Input
{
    public interface IInputService
    {
        void SwitchMap(InputMapType type);
        Vector3 MoveDirection { get; }
    }
    
    public class InputService : IInputService
    {
        private readonly InputActions _inputActions;
        public Vector3 MoveDirection { get; private set; }

        public InputService()
        {
            _inputActions = new InputActions();
            _inputActions.Enable();
        }

        public void SwitchMap(InputMapType type)
        {
            switch (type)
            {
                case InputMapType.Player:
                    PlayerMapSubscribe();
                    _inputActions.UI.Disable();
                    _inputActions.Player.Enable();
                    break;
                case InputMapType.UI:
                    PlayerMapUnSubscribe();
                    _inputActions.Player.Disable();
                    _inputActions.UI.Enable();
                    break;
            }
        }

        private void PlayerMapSubscribe()
        {
            _inputActions.Player.Move.performed += OnMovePerformed;
            _inputActions.Player.Move.canceled += OnMovePerformed;
        }
        
        private void PlayerMapUnSubscribe()
        {
            _inputActions.Player.Move.performed -= OnMovePerformed;
            _inputActions.Player.Move.canceled -= OnMovePerformed;
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            MoveDirection = new Vector3(direction.x, 0, direction.y);
        }
    }
}