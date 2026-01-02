using System;
using Project.ScriptableObjects;
using Project.Services;
using UnityEngine;
using VContainer;

namespace Project.Game
{
    public interface IUpgradeController : IDisposable
    {
        void Initialize(UpgradeViewItem viewItem, UpgradeConfig config);
        void Enter();
        void Exit();
    }
    
    public sealed class UpgradeController : IUpgradeController
    {
        private readonly IAbilityService _abilityService;
        private readonly IBankService _bankService;
        private readonly IUpgradeModel _model = new UpgradeModel();

        private UpgradeViewItem _viewItem;

        [Inject]
        public UpgradeController(IBankService bankService, IAbilityService abilityService)
        {
            _abilityService = abilityService;
            _bankService = bankService;
        }

        public void Initialize(UpgradeViewItem viewItem, UpgradeConfig config)
        {
            _viewItem = viewItem;
            _model.Initialize(config.Type);
            _bankService.BankUpdated += OnBankUpdate;

            CheckHasUpgrade(_model.GetType());
        }

        private void CheckHasUpgrade(AbilityType type)
        {
            _viewItem.SetActiveIndicator(_abilityService.HasUpgrade(type));
        }

        private void OnBankUpdate(BankMessageData data)
        {
            CheckHasUpgrade(_model.GetType());
        }

        public void Enter()
        {
            if (!_abilityService.HasUpgrade(_model.GetType()))
            {
                return;
            }
            
            _abilityService.UpdateLevel(_model.GetType());
            CheckHasUpgrade(_model.GetType());
        }

        public void Exit()
        {
            
        }

        public void Dispose()
        {
            _bankService.BankUpdated -= OnBankUpdate;
        }
    }
}