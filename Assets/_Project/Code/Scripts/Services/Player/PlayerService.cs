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
            _cameraService.SetTarget(_player.Transform);
        }

        public void Tick()
        {
            _movement.Move(_inputService.MoveDirection, _config.MoveSpeed);
            _movement.UpdateRotation(_inputService.MoveDirection, _config.RotateSpeed);
        }
    }
}