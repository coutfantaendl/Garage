using PlayerSettings.Abstraction;
using UnityEngine;
using Zenject;

namespace PlayerSettings
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float lookSensitivity;
        [SerializeField] private Transform cameraTransform;

        private CharacterController _characterController;
        private IPlayerInput _playerInput;
        private float _pitch;

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
            Vector2 input = _playerInput.GetMoveInput();
            Vector3 moveDirection = new Vector3(input.x, 0, input.y);

            if (moveDirection.sqrMagnitude > 0.01f)
            {
                moveDirection = moveDirection.normalized;

                Vector3 worldMoveDirection = transform.TransformDirection(moveDirection);

                _characterController.Move(worldMoveDirection * moveSpeed * Time.deltaTime);

                Quaternion targetRotation = Quaternion.LookRotation(worldMoveDirection);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotation, lookSensitivity * Time.deltaTime);

                cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetRotation,
                    lookSensitivity * Time.deltaTime);
            }
        }
    }
}