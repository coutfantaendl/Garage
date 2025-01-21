using InteractableItemSettings;
using PlayerSettings;
using PlayerSettings.Abstraction;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInputAdapter _playerInputAdapter;
        [SerializeField] private PlayerInventory _playerInventory;
        [SerializeField] private PlayerController _playerController;

        public override void InstallBindings()
        {
            Container.Bind<IPlayerInput>().To<PlayerInputAdapter>().FromComponentOn(_playerInputAdapter.gameObject)
                .AsSingle()
                .NonLazy();

            Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle().NonLazy();

            Container.Bind<PlayerInventory>().FromInstance(_playerInventory).AsSingle().NonLazy();
        }
    }
}