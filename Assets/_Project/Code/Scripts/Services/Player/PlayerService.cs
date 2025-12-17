using Project.Game;
using Project.Input;
using Project.ScriptableObjects;
using UnityEngine;
using VContainer.Unity;

namespace Project.Services
{
    public interface IPlayerService : IInitializable, IFixedTickable, ILateTickable
    {
        
    }
    
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

        public void FixedTick()
        {
            _movement.Move(_inputService.MoveDirection, _config.MoveSpeed);
        }

        public void LateTick()
        {
            _movement.UpdateRotation(_inputService.MoveDirection, _config.RotateSpeed);
            _cameraService.Follow();
        }
    }
}