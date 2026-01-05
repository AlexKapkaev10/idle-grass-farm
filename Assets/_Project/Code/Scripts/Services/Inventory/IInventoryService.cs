using System;
using Project.Game;

namespace Project.Services
{
    public interface IInventoryService
    {
        event Action<ResourceType, int> InventoryUpdated;
        event Action InventoryUpgraded;
        void Initialize(int level, float capacity);
        void UpgradeLevel(float capacity);
        void Commit(ResourceType configResourceType);
        int GetLevel();
        int GetCapacity();
        int GetResourceAmount(ResourceType resourceType);
        bool HasCommit(ResourceType resourceType);
        bool TryReserve(ResourceType type, int amount = 1);
        bool TrySold(ResourceType type, int amount = 1);
    }
}