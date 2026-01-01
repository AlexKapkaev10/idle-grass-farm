using System;
using Project.Game;

namespace Project.Services
{
    public interface IInventoryService
    {
        int Level { get; }
        event Action<ResourceType, int> InventoryUpdated;
        event Action InventoryUpgraded;
        void UpgradeLevel();
        void Commit(ResourceType configResourceType);
        int GetResourceAmount(ResourceType resourceType);
        int GetCapacity();
        bool HasCommit(ResourceType resourceType);
        bool TryReserve(ResourceType type, int amount = 1);
        bool TrySold(ResourceType type, int amount = 1);
    }
}