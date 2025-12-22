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
        private int _reservedCount;
        private int _level;

        public event Action<ResourceType, int> InventoryUpdated;

        [Inject]
        public InventoryService(InventoryServiceConfig config)
        {
            _config = config;
            _level = 1;
        }

        public void Commit(ResourceType type, int amount = 1)
        {
            _reservedCount -= amount;

            switch (type)
            {
                case ResourceType.Green:
                    _greenResourceCount += amount;
                    
                    break;
                case ResourceType.Yellow:
                    _yellowResourceCount += amount;
                    break;
            }
            
            InventoryUpdated?.Invoke(type, type == ResourceType.Green ? _greenResourceCount : _yellowResourceCount);
        }

        public void UpdateLevel()
        {
            _level++;
        }

        public bool TryReserve(int amount = 1)
        {
            if (!CanReserve(amount))
            {
                return false;
            }

            _reservedCount += amount;
            return true;
        }

        public void Reset()
        {
            _greenResourceCount = 0;
            _yellowResourceCount = 0;
            _reservedCount = 0;
        }

        private bool CanReserve(int amount = 1)
        {
            return _greenResourceCount
                   + _yellowResourceCount
                   + _reservedCount
                   + amount
                   <= _config.GetMaxAmountByLevel(_level);
        }
    }
}
