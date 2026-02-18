using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IPoolService _poolService;
        
        private List<IResourceItem> _resourceItems = new ();
        private IGardenItem[] _items;
        private AudioSource _audioSource;
        private GardenConfig _config;

        [Inject]
        public GardenController(
            IPlayerService playerService, 
            IAbilityService abilityService, 
            IInventoryService inventoryService,
            IPoolService poolService)
        {
            _playerService = playerService;
            _abilityService = abilityService;
            _inventoryService = inventoryService;
            _poolService = poolService;
        }

        public void Initialize(IGardenItem[] items, AudioSource audioSource, GardenConfig config)
        {
            _config = config;
            _audioSource = audioSource;
            _items = items;
            
            foreach (var item in _items)
            {
                item.Initialize(_config.CellMaterial);
            }
        }

        public void Enter()
        {
            CollectOld();
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

        private void CollectOld()
        {
            var playerPosition = _playerService.Transform.position;
            
            var sortedItems = _resourceItems
                .OrderBy(i => (i.Transform.position - playerPosition).sqrMagnitude)
                .ToList();

            foreach (var item in sortedItems.ToList())
            {
                if (!_inventoryService.TryReserve(_config.ResourceType))
                {
                    break;
                }
                
                SendResource(item);
                sortedItems.Remove(item);
            }
            
            _resourceItems = sortedItems;
        }

        private void EndMow()
        {
            _playerService.Mowed -= OnMow;
            _playerService.SetMow(_config.InteractAnimation, false);
        }

        private void OnMow()
        {
            PlayAudio(_config.MowAudioClip);

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

                var resourceItem = _poolService.GetGardenResource();

                item.Mow(_config.ResourceMaterial, resourceItem);
                
                if (_inventoryService.TryReserve(_config.ResourceType))
                {
                    SendResource(resourceItem, 1.0f);
                }
                else
                {
                    _resourceItems.Add(resourceItem);
                }
            }
        }

        private void PlayAudio(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }

        private void SendResource(IResourceItem item, float delay = 0.0f)
        {
            item.Collected += OnResourceCollected;
            item.MoveToTarget(_playerService.Transform, delay, CollectResource);
        }

        private void OnResourceCollected(IResourceItem item)
        {
            item.Collected -= OnResourceCollected;
            
            _poolService.ReleaseResourceItem(item);
        }

        private void CollectResource(int amount)
        {
            if (!_inventoryService.HasCommit(_config.ResourceType))
            {
                return;
            }
            
            _inventoryService.Commit(_config.ResourceType);
        }

        private bool HasDistanceToMow(IGardenItem item)
        {
            Vector3 toTarget = item.Transform.position - _playerService.BodyTransform.position;
            toTarget.y = 0f;

            return toTarget.magnitude <= _abilityService.GetMowRange() &&
                   Vector3.Dot(_playerService.BodyTransform.forward, toTarget.normalized) > 0f;
        }

        void IDisposable.Dispose()
        {
            _resourceItems.Clear();
        }
    }
}