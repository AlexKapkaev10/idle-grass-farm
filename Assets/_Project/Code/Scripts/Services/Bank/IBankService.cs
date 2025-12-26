using System;
using Project.Game;

namespace Project.Services
{
    public interface IBankService
    {
        event Action<BankMessageData> BankUpdated;
        int GetCurrencyAmount(ResourceType resourceType);
        void SetCurrencyAmount(ResourceType resourceType, int amount);
    }
}