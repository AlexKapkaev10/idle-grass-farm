using System;
using Project.Game;

namespace Project.Services
{
    public interface IBankService : IDisposable
    {
        event Action<BankMessageData> BankUpdated;
        bool Has(ResourceType resourceType, int amount);
        int GetCurrencyAmount(ResourceType resourceType);
        void SetCurrencyAmount(ResourceType resourceType, int amount);
    }
}