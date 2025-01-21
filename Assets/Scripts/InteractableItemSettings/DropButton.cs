using PlayerSettings;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace InteractableItemSettings
{
    public class DropButton : MonoBehaviour
    {
        [SerializeField] private Button dropButton;
        private PlayerController _playerController;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }

        private void Start()
        {
            dropButton.onClick.AddListener(OnDropButtonPressed);
        }

        private void OnDropButtonPressed()
        {
            _playerController.ThrowItem();
        }
    }
}