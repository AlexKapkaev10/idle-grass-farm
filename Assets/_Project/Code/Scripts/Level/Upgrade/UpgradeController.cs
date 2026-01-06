using System;
using Project.ScriptableObjects;
using Project.Services;
using UnityEngine;
using VContainer;

namespace Project.Game
{
    public interface IUpgradeController : IDisposable
    {
        void Initialize(UpgradeViewItem viewItem, AudioSource audioSource, UpgradeConfig config);
        void Enter();
        void Exit();
    }
    
    public sealed class UpgradeController : IUpgradeController
    {
        private readonly IAbilityService _abilityService;
        private readonly IBankService _bankService;
        private readonly IUpgradeModel _model = new UpgradeModel();

        private AudioSource _audioSource;
        private UpgradeViewItem _viewItem;
        private UpgradeConfig _config;

        [Inject]
        public UpgradeController(IBankService bankService, IAbilityService abilityService)
        {
            _abilityService = abilityService;
            _bankService = bankService;
        }

        public void Initialize(UpgradeViewItem viewItem, AudioSource audioSource, UpgradeConfig config)
        {
            _config = config;
            _audioSource = audioSource;

            _model.Initialize(config.Type);
            _bankService.BankUpdated += OnBankUpdate;

            _viewItem = viewItem;
            _viewItem.SetHeader(_config.TextHeader);
            _viewItem.SetDescription($"{_config.TextDescription} {_abilityService.GetLevelByType(_model.GetType())}");
            CheckHasUpgrade(_model.GetType());
        }

        void IUpgradeController.Enter()
        {
            if (!_abilityService.HasUpgrade(_model.GetType()))
            {
                return;
            }
            
            Upgrade();
            CheckHasUpgrade(_model.GetType());
        }

        private void Upgrade()
        {
            _abilityService.UpdateLevel(_model.GetType());
            _viewItem.SetDescription($"{_config.TextDescription} {_abilityService.GetLevelByType(_model.GetType())}");
            PlayAudio(_config.UpgradeAudioClip);
        }

        private void PlayAudio(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }

        void IUpgradeController.Exit()
        {
            
        }

        void IDisposable.Dispose()
        {
            _bankService.BankUpdated -= OnBankUpdate;
        }

        private void CheckHasUpgrade(AbilityType type)
        {
            _viewItem.SetActiveIndicator(_abilityService.HasUpgrade(type));
        }

        private void OnBankUpdate(BankMessageData data)
        {
            CheckHasUpgrade(_model.GetType());
        }
    }
}