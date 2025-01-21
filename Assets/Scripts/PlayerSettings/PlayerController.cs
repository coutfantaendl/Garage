using PlayerSettings.Abstraction;
using UnityEngine;
using Zenject;

namespace PlayerSettings
{
    public class PlayerController : MonoBehaviour
    {
        [Header("PlayerSettings")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _lookSensitivity;
        [SerializeField] private Transform _cameraTransform;

        private CharacterController _characterController;
        private IPlayerInput _playerInput;

        [Inject]
        public void Construct(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            MoveAndRotate();
        }

        private void MoveAndRotate()
        {
            var input = _playerInput.GetMoveInput();
            var moveDirection = new Vector3(input.x, 0, input.y);

            if (moveDirection.sqrMagnitude > 0.01f)
            {
                moveDirection = moveDirection.normalized;

                var worldMoveDirection = transform.TransformDirection(moveDirection);

                _characterController.Move(worldMoveDirection * _moveSpeed * Time.deltaTime);

                Quaternion targetRotation = Quaternion.LookRotation(worldMoveDirection);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotation, _lookSensitivity * Time.deltaTime);

                _cameraTransform.rotation = Quaternion.Slerp(_cameraTransform.rotation, targetRotation,
                    _lookSensitivity * Time.deltaTime);
            }
        }
    }
}