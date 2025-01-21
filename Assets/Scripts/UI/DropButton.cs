using PlayerSettings;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using UnityEngine.UI;

namespace InteractableItemSettings
{
    public class DropButton : MonoBehaviour
    {
        [SerializeField] private Button _dropButton;
        
        private PlayerInteractor _playerInteractor;
        
        [Inject]
        public void Construct(PlayerInteractor playerInteractor)
        {
            _playerInteractor = playerInteractor;
        }
        
        private void Start()
        {
            _dropButton.onClick.AddListener(OnDropButtonPressed);
        }
        
        private void OnDropButtonPressed()
        {
            _playerInteractor.ThrowItem();
        }
    }
}