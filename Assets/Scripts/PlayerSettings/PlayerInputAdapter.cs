using PlayerSettings.Abstraction;
using UnityEngine.InputSystem;
using UnityEngine;

namespace PlayerSettings
{
    public class PlayerInputAdapter: MonoBehaviour, IPlayerInput
    {
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        public Vector2 GetMoveInput()
        {
            return _playerInput.actions["PlayerMove"].ReadValue<Vector2>();
        }
        
        public Vector2 GetLookInput()
        {
            return _playerInput.actions["Look"].ReadValue<Vector2>();
        }
    }
}