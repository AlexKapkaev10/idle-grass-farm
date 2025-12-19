using System;
using Project.Game;
using Project.Input;
using Project.ScriptableObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IInputService _inputService;
        private readonly ICameraService _cameraService;
        private readonly PlayerServiceConfig _config;

        private IPlayer _player;
        private IAnimatorComponent _animatorComponent;
        private IMovement _movement;
        private ISearchModel _searchModel;
        private ITool _tool;

        public event Action Mowed;
        public Transform Transform => _player.Transform;

        public PlayerService(IInputService inputService, ICameraService cameraService, PlayerServiceConfig config)
        {
            _inputService = inputService;
            _cameraService = cameraService;
            _config = config;
        }

        public void Initialize()
        {
            _player = Object.Instantiate(_config.PlayerPrefab);
            
            _movement = _player.Movement;
            _animatorComponent = _player.AnimatorComponent;
            _cameraService.SetTarget(_player.Transform);
            
            _searchModel = new SearchModel(_player.GroundTransform, _config.SearchModelConfig);
            
            _movement.Running += OnRun;
            _animatorComponent.EventsReceiver.Mowed += OnMow;
        }

        public void SetAnimationBool(int id, bool value)
        {
            _animatorComponent.SetBool(id, value);
        }

        public void SetTool(bool isActive)
        {
            if (isActive)
            {
                if (_tool == null)
                {
                    _tool = Object.Instantiate(_config.ToolPrefab, _player.ToolParent);
                }
            }
            else
            {
                _tool?.Destroy();
                _tool = null;
            }
        }

        public void SetMow(int animationID, bool isActive)
        {
            SetAnimationBool(animationID, isActive);
            SetTool(isActive);
        }

        private void OnMow()
        {
            Mowed?.Invoke();
        }

        public void Tick()
        {
            _movement.Move(_inputService.MoveDirection, _config.MoveSpeed);
            _movement.UpdateRotation(_inputService.MoveDirection, _config.RotateSpeed);
        }

        public void FixedTick()
        {
            _searchModel.Process();
        }

        public void Dispose()
        {
            _movement.Running -= OnRun;
            _animatorComponent.EventsReceiver.Mowed -= OnMow;
            
            _searchModel?.Dispose();
        }

        private void OnRun(bool isRunning)
        {
            SetAnimationBool(_config.IsRun, isRunning);
        }
    }
}