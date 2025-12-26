using System;
using Project.Game;

namespace Project.Services
{
    public interface IInventoryService
    {
        event Action<ResourceType, int> InventoryUpdated;
        void UpdateLevel();
        void Commit(ResourceType configResourceType);
        int GetResourceAmount(ResourceType resourceType);
        int GetCapacity();
        bool HasCommit(ResourceType resourceType);
        bool TryReserve(ResourceType type, int amount = 1);
        bool TrySold(ResourceType type, int amount = 1);
    }
}