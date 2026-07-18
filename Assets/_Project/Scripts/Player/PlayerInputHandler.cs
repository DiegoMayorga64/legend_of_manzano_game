using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Player
{

    [RequireComponent(typeof(PlayerInput))]
        public class PlayerInputHandler : MonoBehaviour
        {
            public Vector2 MoveInput { get; private set; }
            public bool JumpPressed { get; private set; }
            public bool JumpReleased { get; private set; }

            private InputAction _moveAction;
            private InputAction _jumpAction;

            private void Awake()
            {
                var playerInput = GetComponent<PlayerInput>();
                _moveAction = playerInput.actions["Move"];
                _jumpAction = playerInput.actions["Jump"];
            }

            private void OnEnable()
            {
                _jumpAction.performed += OnJumpPerformed;
                _jumpAction.canceled += OnJumpCanceled;
            }

            private void OnDisable()
            {
                _jumpAction.performed -= OnJumpPerformed;
                _jumpAction.canceled -= OnJumpCanceled;
            }

            private void Update()
            {
                MoveInput = _moveAction.ReadValue<Vector2>();
            }

            private void OnJumpPerformed(InputAction.CallbackContext ctx) => JumpPressed = true;
            private void OnJumpCanceled(InputAction.CallbackContext ctx) => JumpReleased = true;

            // Llamado por PlayerMovement al final de cada frame para resetear flags de un solo frame
            public void ConsumeJumpFlags()
            {
                JumpPressed = false;
                JumpReleased = false;
            }
    }    
    
}
