using System;
using Project.Game;

namespace Project.Services
{
    public interface IInventoryService
    {
        event Action<ResourceType, int> InventoryUpdated;
        void UpdateLevel();
        bool HasCommit(ResourceType resourceType);
        bool TryReserve(ResourceType type, int amount = 1);
        void Commit(ResourceType configResourceType);
        int GetResourceAmount(ResourceType resourceType);
        int GetCapacity();
        void SoldResource(ResourceType type, int amount = 1);
    }
}