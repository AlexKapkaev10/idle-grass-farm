using System;
using Project.Game;
using UnityEngine;
using VContainer;

namespace Project.Services
{
    public class BankService : IBankService
    {
        private int _firstCurrency;
        private int _secondCurrency;

        public event Action<BankMessageData> BankUpdated;

        [Inject]
        public BankService()
        {
            SetCurrencyAmount(ResourceType.First, 10);
            SetCurrencyAmount(ResourceType.Second, 10);
        }

        public int GetCurrencyAmount(ResourceType resourceType)
        {
            return resourceType == ResourceType.First ? _firstCurrency : _secondCurrency;
        }

        public void SetCurrencyAmount(ResourceType resourceType, int amount)
        {
            switch (resourceType)
            {
                case ResourceType.First:
                    _firstCurrency += amount;
                    break;
                case ResourceType.Second:
                    _secondCurrency += amount;
                    break;
            }

            var newAmount = resourceType == ResourceType.First
                ? _firstCurrency
                : _secondCurrency;
            
            Debug.Log("Set currency");
            
            BankUpdated?.Invoke(new BankMessageData(resourceType, 
                newAmount - amount,
                newAmount));
        }
    }
}