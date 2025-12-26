using Project.Game;

namespace Project.Services
{
    public readonly struct BankMessageData
    {
        public readonly ResourceType ResourceType;
        public readonly int OldAmount;
        public readonly int NewAmount;

        public BankMessageData(ResourceType resourceType, int oldAmount, int newAmount)
        {
            ResourceType = resourceType;
            NewAmount = newAmount;
            OldAmount = oldAmount;
        }
    }
}