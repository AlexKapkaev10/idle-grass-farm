using Project.ScriptableObjects;
using VContainer;

namespace Project.Services
{
    public interface IInventoryService
    {
        void SetResourceCount(ResourceType type, int value);
        bool CanAddResource();
    }
    
    public class InventoryService : IInventoryService
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

        public void SetResourceCount(ResourceType type, int value)
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

        public bool CanAddResource()
        {
            return _greenResourceCount + _yellowResourceCount 
                   < _config.GetMaxAmountByLevel(_level);
        }
    }
}