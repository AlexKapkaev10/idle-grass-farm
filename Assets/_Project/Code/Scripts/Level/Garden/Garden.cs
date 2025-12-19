using Project.ScriptableObjects;
using Project.Services;
using UnityEngine;
using VContainer;

namespace Project.Game
{
    public class Garden : MonoBehaviour, IInteractable
    {
        [SerializeField] private GardenConfig _config;

        private IGardenItem[] _items;
        private IPlayerService _playerService;
        private IAbilityService _abilityService;

        [Inject]
        private void Construct(IPlayerService playerService, IAbilityService abilityService)
        {
            _playerService = playerService;
            _abilityService = abilityService;
        }
        
        private void Awake()
        {
            _items = GetComponentsInChildren<IGardenItem>();

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
                if (item.CanMow)
                {
                    var distance = Vector3.Distance(_playerService.Transform.position, item.Transform.position);

                    if (distance < _abilityService.MowRange)
                    {
                        item.Mow();
                    }
                }
            }
        }
    }
}