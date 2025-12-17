using Project.ScriptableObjects;
using Project.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Core
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private PlayerServiceConfig _playerServiceConfig;
        [SerializeField] private CameraServiceConfig _cameraServiceConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameSceneService>(Lifetime.Scoped)
                .As<IGameSceneService>();
            
            builder.RegisterEntryPoint<PlayerService>(Lifetime.Scoped)
                .As<IPlayerService>()
                .WithParameter(_playerServiceConfig);
            
            builder.Register<CameraService>(Lifetime.Scoped)
                .As<ICameraService>()
                .WithParameter(_cameraServiceConfig);
        }
    }
}