using Project.ScriptableObjects;
using VContainer;

namespace Project.Services
{
    public sealed class InventoryService : IInventoryService
    {
        private readonly InventoryServiceConfig _config;
        
        private int _greenResourceCount;
        private int _yellowResourceCount;
        private int _level;

        [Inject]
        public InventoryService(InventoryServiceConfig config)
        {
            _config = config;
            _level = 1;
        }

        public void SetResourceCount(ResourceType type, int value = 1)
        {
            switch (type)
            {
                case ResourceType.Green:
                    _greenResourceCount += value;
                    break;
                case ResourceType.Yellow:
                    _yellowResourceCount += value;
                    break;
            }
        }

        public void UpdateLevel()
        {
            _level++;
        }

        public bool CanAddResource()
        {
            return _greenResourceCount + _yellowResourceCount 
                   < _config.GetMaxAmountByLevel(_level);
        }
    }
}