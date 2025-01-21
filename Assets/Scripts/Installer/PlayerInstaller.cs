using PlayerSettings;
using PlayerSettings.Abstraction;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInputAdapter _playerInputAdapter;

        public override void InstallBindings()
        {
            Container.Bind<IPlayerInput>().To<PlayerInputAdapter>().FromComponentOn(_playerInputAdapter.gameObject)
                .AsSingle()
                .NonLazy();
        }
    }
}