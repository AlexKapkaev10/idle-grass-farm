using Project.Game;
using Project.Input;
using Project.ScriptableObjects;
using UnityEngine;

namespace Project.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IInputService _inputService;
        private readonly ICameraService _cameraService;
        private readonly PlayerServiceConfig _config;

        private IPlayer _player;
        private IMovement _movement;
        private IAnimatorComponent _animatorComponent;

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
            
            _movement.Running += OnRun;
        }

        private void OnRun(bool isRunning)
        {
            _animatorComponent.SetBool(_config.IsRun, isRunning);
        }

        public void Tick()
        {
            _movement.Move(_inputService.MoveDirection, _config.MoveSpeed);
            _movement.UpdateRotation(_inputService.MoveDirection, _config.RotateSpeed);
        }
    }
}