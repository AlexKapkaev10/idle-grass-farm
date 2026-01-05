using System;
using Project.Game;
using Project.ScriptableObjects;
using VContainer;

namespace Project.Services
{
    public sealed class InventoryService : IInventoryService
    {
        private readonly IBankService _bankService;
        private readonly InventoryServiceConfig _config;

        private int _firstResourceCount;
        private int _secondResourceCount;
        private int _reservedFirstCount;
        private int _reservedSecondCount;
        private int _level;
        private int _capacity;

        public event Action<ResourceType, int> InventoryUpdated;
        public event Action InventoryUpgraded;

        [Inject]
        public InventoryService(IBankService bankService, InventoryServiceConfig config)
        {
            _bankService = bankService;
            _config = config;
        }

        void IInventoryService.Initialize(int level, float capacity)
        {
            _level = level;
            _capacity = (int)capacity;
        }

        public void Commit(ResourceType type)
        {
            int committedAmount = 0;

            switch (type)
            {
                case ResourceType.First:
                    committedAmount = _reservedFirstCount;
                    _firstResourceCount += committedAmount;
                    _reservedFirstCount = 0;
                    break;

                case ResourceType.Second:
                    committedAmount = _reservedSecondCount;
                    _secondResourceCount += committedAmount;
                    _reservedSecondCount = 0;
                    break;
            }

            if (committedAmount <= 0)
            {
                return;
            }

            InventoryUpdated?.Invoke(type,
                type == ResourceType.First ? _firstResourceCount : _secondResourceCount
            );
        }

        public int GetLevel()
        {
            return _level;
        }

        public int GetResourceAmount(ResourceType resourceType)
        {
            return resourceType == ResourceType.First ? _firstResourceCount : _secondResourceCount;
        }

        public int GetCapacity()
        {
            return _capacity;
        }

        public bool TrySold(ResourceType type, int amount = 1)
        {
            if (!CanSold(type, amount))
            {
                return false;
            }
            
            switch (type)
            {
                case ResourceType.First:
                    _firstResourceCount -= amount;
                    break;
                case ResourceType.Second:
                    _secondResourceCount -= amount;
                    break;
            }
            
            _bankService.SetCurrencyAmount(type, 10);
            
            InventoryUpdated?.Invoke(type,
                type == ResourceType.First ? _firstResourceCount : _secondResourceCount
            );
            
            return true;
        }

        public void UpgradeLevel(float capacity)
        {
            _level++;
            _capacity = (int)capacity;
            InventoryUpgraded?.Invoke();
        }

        public bool HasCommit(ResourceType type)
        {
            switch (type)
            {
                case ResourceType.First:
                    return _reservedFirstCount != 0;
                case ResourceType.Second:
                    return _reservedSecondCount != 0;
            }
            
            return false;
        }

        public bool TryReserve(ResourceType type, int amount = 1)
        {
            if (!CanReserve(type, amount))
            {
                return false;
            }

            switch (type)
            {
                case ResourceType.First:
                    _reservedFirstCount += amount;
                    break;
                case ResourceType.Second:
                    _reservedSecondCount += amount;
                    break;
            }
            
            return true;
        }

        private bool CanReserve(ResourceType type, int amount = 1)
        {
            switch (type)
            {
                case ResourceType.First:
                    return _firstResourceCount 
                        + _reservedFirstCount 
                        + amount <= _capacity;
                case ResourceType.Second:
                    return _secondResourceCount
                        + _reservedSecondCount 
                        + amount <= _capacity;
            }
            
            return false;
        }

        private bool CanSold(ResourceType type, int amount = 1)
        {
            switch (type)
            {
                case ResourceType.First:
                    return _firstResourceCount >= amount;
                case ResourceType.Second:
                    return _secondResourceCount >= amount;
            }
            
            return false;
        }
    }
}
