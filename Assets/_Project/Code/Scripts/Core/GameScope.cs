using Project.Game;
using Project.ScriptableObjects;
using Project.Services;
using Project.UI.MVP;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Core
{
    public sealed class GameScope : LifetimeScope
    {
        [Header("Services")]
        [SerializeField] private GameSceneServiceConfig _gameSceneServiceConfig;
        [SerializeField] private PlayerServiceConfig _playerServiceConfig;
        [SerializeField] private CameraServiceConfig _cameraServiceConfig;
        [SerializeField] private AbilityServiceConfig _abilityServiceConfig;
        [SerializeField] private InventoryServiceConfig _inventoryServiceConfig;
        [SerializeField] private TradeServiceConfig _tradeServiceConfig;
        [Header("Presenters")]
        [SerializeField] private JoystickPresenterConfig _joystickPresenterConfig;
        [SerializeField] private InfoPresenterConfig _infoPresenterConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterServices(builder);
            RegisterMVP(builder);
            RegisterMVC(builder);
        }

        private void RegisterServices(IContainerBuilder builder)
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
            
            builder.Register<InventoryService>(Lifetime.Scoped)
                .As<IInventoryService>()
                .WithParameter(_inventoryServiceConfig);

            builder.Register<BankService>(Lifetime.Scoped)
                .As<IBankService>();
            
            builder.Register<TradeService>(Lifetime.Scoped)
                .As<ITradeService>()
                .WithParameter(_tradeServiceConfig);
        }

        private static void RegisterMVC(IContainerBuilder builder)
        {
            builder.Register<GardenController>(Lifetime.Transient)
                .As<IGardenController>();
            
            builder.Register<UpgradeController>(Lifetime.Transient)
                .As<IUpgradeController>();
            
            builder.Register<CashboxController>(Lifetime.Scoped)
                .As<ICashboxController>();
        }

        private void RegisterMVP(IContainerBuilder builder)
        {
            builder.Register<JoystickPresenter>(Lifetime.Scoped)
                .As<IJoystickPresenter>()
                .WithParameter(_joystickPresenterConfig);
            
            builder.Register<InfoPresenter>(Lifetime.Scoped)
                .As<IInfoPresenter>()
                .WithParameter(_infoPresenterConfig);
        }
    }
}