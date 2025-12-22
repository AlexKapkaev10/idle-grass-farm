namespace Project.Services
{
    public interface IInventoryService
    {
        void SetResourceCount(ResourceType type, int value = 1);
        void UpdateLevel();
        bool CanAddResource();
    }
}