using System;
using Project.Game;
using Project.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace Project.Services
{
    public sealed class InventoryService : IInventoryService
    {
        private readonly InventoryServiceConfig _config;

        private int _greenResourceCount;
        private int _yellowResourceCount;
        private int _reservedGreenCount;
        private int _reservedYellowCount;
        private int _level;

        public event Action<ResourceType, int> InventoryUpdated;

        [Inject]
        public InventoryService(InventoryServiceConfig config)
        {
            _config = config;
            _level = 1;
        }

        public void Commit(ResourceType type)
        {
            int committedAmount = 0;

            switch (type)
            {
                case ResourceType.Green:
                    committedAmount = _reservedGreenCount;
                    _greenResourceCount += committedAmount;
                    _reservedGreenCount = 0;
                    break;

                case ResourceType.Yellow:
                    committedAmount = _reservedYellowCount;
                    _yellowResourceCount += committedAmount;
                    _reservedYellowCount = 0;
                    break;
            }

            if (committedAmount <= 0)
            {
                return;
            }

            InventoryUpdated?.Invoke(type,
                type == ResourceType.Green ? _greenResourceCount : _yellowResourceCount
            );
        }

        public int GetResourceAmount(ResourceType resourceType)
        {
            return resourceType == ResourceType.Green ? _greenResourceCount : _yellowResourceCount;
        }

        public int GetCapacity()
        {
            return _config.GetCapacityByLevel(_level);
        }

        public void SoldResource(ResourceType type, int amount = 1)
        {
            switch (type)
            {
                case ResourceType.Green:
                    _greenResourceCount -= amount;
                    if (_greenResourceCount <= 0)
                    {
                        _greenResourceCount = 0;
                    }
                    
                    break;
                case ResourceType.Yellow:
                    _yellowResourceCount -= amount;
                    if (_yellowResourceCount <= 0)
                    {
                        _yellowResourceCount = 0;
                    }
                    
                    break;
            }
            
            InventoryUpdated?.Invoke(type,
                type == ResourceType.Green ? _greenResourceCount : _yellowResourceCount
            );
        }

        public void UpdateLevel()
        {
            _level++;
        }

        public bool HasCommit(ResourceType type)
        {
            switch (type)
            {
                case ResourceType.Green:
                    return _reservedGreenCount != 0;
                case ResourceType.Yellow:
                    return _reservedYellowCount != 0;
            }
            
            return false;
        }

        public bool TryReserve(ResourceType type, int amount = 1)
        {
            if (!CanReserve(amount))
            {
                return false;
            }

            switch (type)
            {
                case ResourceType.Green:
                    _reservedGreenCount += amount;
                    break;
                case ResourceType.Yellow:
                    _reservedYellowCount += amount;
                    break;
            }
            
            return true;
        }

        private bool CanReserve(int amount = 1)
        {
            return _greenResourceCount + _yellowResourceCount + _reservedGreenCount + _reservedYellowCount + amount
                   <= _config.GetCapacityByLevel(_level);
        }
    }
}
