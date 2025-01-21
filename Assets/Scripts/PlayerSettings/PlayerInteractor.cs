using InteractableItemSettings;
using PlayerSettings.Abstraction;
using UnityEngine;
using Zenject;
namespace PlayerSettings
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private Transform _holdPoint;
        [SerializeField] private float _throwForce = 5f;

        private IPlayerInput _playerInput;
        private PlayerInventory _playerInventory;

        [Inject]
        public void Construct(IPlayerInput playerInput, PlayerInventory playerInventory)
        {
            _playerInput = playerInput;
            _playerInventory = playerInventory;
        }

        private void Update()
        {
            if (_playerInput.IsInteractPressed())
                TryInteract();

            if (_playerInput.IsDropPressed())
                ThrowItem();
        }

        private void TryInteract()
        {
            if (_playerInventory.HeldItem is not null) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 2f))
            {
                var interactable = hit.collider.GetComponent<InteractableItem>();
                
                if (interactable is not null)
                {
                    interactable.PickUp(_holdPoint);
                    _playerInventory.SetHeldItem(interactable);
                }
            }
        }
        
        public void ThrowItem()
        {
            if (_playerInventory.HeldItem is not null)
            {
                _playerInventory.HeldItem.Throw(_holdPoint.forward * _throwForce);
                _playerInventory.ClearHeldItem();
            }
        }
    }
}