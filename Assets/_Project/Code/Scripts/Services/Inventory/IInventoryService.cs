using System;
using Project.Game;

namespace Project.Services
{
    public interface IInventoryService
    {
        event Action<ResourceType, int> InventoryUpdated;
        void UpdateLevel();
        void Reset();
        bool TryReserve(int amount = 1);
        void Commit(ResourceType configResourceType, int amount);
    }
}