using Project.Game;
using Project.ScriptableObjects;
using Project.Services;
using Project.UI.MVP;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Core
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private GameSceneServiceConfig _gameSceneServiceConfig;
        [SerializeField] private PlayerServiceConfig _playerServiceConfig;
        [SerializeField] private CameraServiceConfig _cameraServiceConfig;
        [SerializeField] private AbilityServiceConfig _abilityServiceConfig;
        [SerializeField] private JoystickPresenterConfig _joystickPresenterConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameSceneService>(Lifetime.Scoped)
                .As<IGameSceneService>()
                .WithParameter(_gameSceneServiceConfig);
            
            builder.RegisterEntryPoint<PlayerService>(Lifetime.Scoped)
                .As<IPlayerService>()
                .WithParameter(_playerServiceConfig);
            
            builder.Register<CameraService>(Lifetime.Scoped)
                .As<ICameraService>()
                .WithParameter(_cameraServiceConfig);

            builder.Register<AbilityService>(Lifetime.Scoped)
                .As<IAbilityService>()
                .WithParameter(_abilityServiceConfig);

            builder.Register<JoystickPresenter>(Lifetime.Scoped)
                .As<IJoystickPresenter>()
                .WithParameter(_joystickPresenterConfig);
        }
    }
}