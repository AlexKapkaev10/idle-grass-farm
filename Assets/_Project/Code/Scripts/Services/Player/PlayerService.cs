using System;
using Project.Game;
using Project.Input;
using Project.ScriptableObjects;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Project.Services
{
    public sealed class PlayerService : IPlayerService
    {
        private readonly IInputService _inputService;
        private readonly ICameraService _cameraService;
        private readonly IAbilityService _abilityService;
        private readonly IInventoryService _inventoryService;
        private readonly PlayerServiceConfig _config;

        private IPlayer _player;
        private IAnimatorComponent _animatorComponent;
        private IMovement _movement;
        private ISearchModel _searchModel;
        private ITool _tool;

        public event Action Mowed;
        public Transform Transform => _player.Transform;
        public Transform BodyTransform => _player.BodyTransform;

        public PlayerService(IInputService inputService, 
            ICameraService cameraService, 
            IAbilityService abilityService, 
            IInventoryService inventoryService,
            PlayerServiceConfig config)
        {
            _inputService = inputService;
            _cameraService = cameraService;
            _abilityService = abilityService;
            _inventoryService = inventoryService;
            _config = config;
        }

        public void Initialize()
        {
            _player = Object.Instantiate(_config.PlayerPrefab);
            
            _movement = _player.Movement;
            _animatorComponent = _player.AnimatorComponent;
            _cameraService.SetTarget(_player.BodyTransform);
            _player.ToolRangeTransform.gameObject.SetActive(false);
            
            _searchModel = new SearchModel(_player.GroundTransform, _config.SearchModelConfig);
            
            _movement.Running += OnRun;
            _animatorComponent.EventsReceiver.Mowed += OnMow;
            _abilityService.AbilitiesUpdated += OnAbilitiesUpdated;
        }

        private void OnInventoryUpgraded(ResourceType type, int amount)
        {
            ShowEffect(EffectType.CollectResource);
        }

        private void OnAbilitiesUpdated(AbilityType type)
        {
            ShowEffect(EffectType.Upgrade);
        }

        public void SetMow(int animationID, bool isActive)
        {
            if (isActive)
            {
                _inventoryService.InventoryUpdated += OnInventoryUpgraded;
            }
            else
            {
                _inventoryService.InventoryUpdated -= OnInventoryUpgraded;
            }
            
            DisplayToolRange(isActive);
            SetAnimationBool(animationID, isActive);
            SetTool(isActive);
        }

        public void ShowEffect(EffectType effectType)
        {
            switch (effectType)
            {
                case EffectType.Upgrade:
                    var upgradeEffect = Object.Instantiate(_config.UpgradeVFXPrefab, _player.Transform);
                    Object.Destroy(upgradeEffect.gameObject, 3);
                    break;
                case EffectType.CollectResource:
                    var collectEffect = Object.Instantiate(_config.CollectVFXPrefab, _player.Transform);
                    Object.Destroy(collectEffect.gameObject, 3);
                    break;
            }
        }

        void IDisposable.Dispose()
        {
            _movement.Running -= OnRun;
            _animatorComponent.EventsReceiver.Mowed -= OnMow;
            _abilityService.AbilitiesUpdated -= OnAbilitiesUpdated;
            _inventoryService.InventoryUpdated -= OnInventoryUpgraded;
            
            _searchModel?.Dispose();
        }

        void ITickable.Tick()
        {
            _movement.Move(_inputService.MoveDirection, _config.MoveSpeed);
            _movement.UpdateRotation(_inputService.MoveDirection, _config.RotateSpeed);
        }

        void IFixedTickable.FixedTick()
        {
            _searchModel.Process();
        }

        private void OnRun(bool isRunning)
        {
            SetAnimationBool(_config.IsRun, isRunning);
        }

        private void OnMow()
        {
            Mowed?.Invoke();
        }

        private void SetAnimationBool(int id, bool value)
        {
            _animatorComponent.SetBool(id, value);
        }

        private void SetTool(bool isActive)
        {
            if (isActive)
            {
                if (_tool != null)
                {
                    return;
                }
                
                _tool = Object.Instantiate(_config.ToolPrefab, _player.ToolParent);
            }
            else
            {
                _tool?.Destroy();
                _tool = null;
            }
        }

        private void DisplayToolRange(bool isActive)
        {
            if (isActive)
            {
                var range = _abilityService.GetMowRange();
                _player.ToolRangeTransform.localScale = new Vector3(range, range, range);
            }

            _player.ToolRangeTransform.gameObject.SetActive(isActive);
        }
    }
}