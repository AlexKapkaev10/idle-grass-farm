using Project.ScriptableObjects;
using Project.Services;
using UnityEngine;
using VContainer;

namespace Project.Game
{
    public sealed class GardenController : IGardenController
    {
        private readonly IPlayerService _playerService;
        private readonly IAbilityService _abilityService;
        private readonly IInventoryService _inventoryService;

        private IGardenItem[] _items;
        private GardenConfig _config;

        [Inject]
        public GardenController(IPlayerService playerService, 
            IAbilityService abilityService, 
            IInventoryService inventoryService)
        {
            _playerService = playerService;
            _abilityService = abilityService;
            _inventoryService = inventoryService;
        }

        public void Initialize(IGardenItem[] items, GardenConfig config)
        {
            _config = config;

            _items = items;
            foreach (var item in _items)
            {
                item.Initialize(_config.Material);
            }
        }

        public void Enter()
        {
            StartMow();
        }

        public void Exit()
        {
            EndMow();
        }

        private void StartMow()
        {
            _playerService.Mowed += OnMow;
            
            foreach (var item in _items)
            {
                if (!item.CanMow)
                {
                    continue;
                }

                _playerService.SetMow(_config.InteractAnimation, true);
                break;
            }
        }

        private void EndMow()
        {
            _playerService.Mowed -= OnMow;
            _playerService.SetMow(_config.InteractAnimation, false);
        }

        private void OnMow()
        {
            foreach (var item in _items)
            {
                if (!item.CanMow)
                {
                    continue;
                }
                
                if (!HasDistanceToMow(item))
                {
                    continue;
                }

                item.Mow();
                if (!_inventoryService.CanAddResource())
                {
                    continue;
                }
                
                _inventoryService.SetResourceCount(_config.ResourceType);
            }
        }

        private bool HasDistanceToMow(IGardenItem item)
        {
            return Vector3.Distance(_playerService.Transform.position, item.Transform.position) 
                   < _abilityService.MowRange;
        }
    }
}