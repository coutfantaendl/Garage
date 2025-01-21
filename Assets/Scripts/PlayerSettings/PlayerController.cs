using InteractableItemSettings;
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
        [SerializeField] private Transform _holdPoint;
        [SerializeField] private float _throwForce;

        private CharacterController _characterController;
        private IPlayerInput _playerInput;
        private float _pitch;
        private PlayerInventory _playerInventory;

        [Inject]
        public void Construct(IPlayerInput playerInput, PlayerInventory playerInventory)
        {
            _playerInput = playerInput;
            _playerInventory = playerInventory;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            MoveAndRotate();
            
            if (Input.GetMouseButtonDown(0))
            {
                TryInteract();
            }
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
        
        private void TryInteract()
        {
            if (_playerInventory.HeldItem is not null) return; 

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            if (Physics.Raycast(ray, out RaycastHit hit, 2f))
            {
                InteractableItem interactable = hit.collider.GetComponent<InteractableItem>();
                
                if (interactable is not null)
                {
                    interactable.PickUp(this);
                }
            }
        }
        
        public void ThrowItem()
        {
            if (_playerInventory.HeldItem is not null)
            {
                _playerInventory.HeldItem.Throw(_holdPoint.forward * _throwForce);
                _playerInventory.HeldItem = null;
            }
        }
    }
}