using System;
using Project.Game;
using Project.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace Project.Services
{
    public class BankService : IBankService
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly BankServiceConfig _config;
        private int _firstCurrency;
        private int _secondCurrency;

        public event Action<BankMessageData> BankUpdated;

        [Inject]
        public BankService(ISaveLoadService saveLoadService, BankServiceConfig config)
        {
            _saveLoadService = saveLoadService;
            _config = config;
            SetCurrencyAmount(ResourceType.First, 
                _saveLoadService.LoadInt(_config.GetCurrencySaveKeyByType(ResourceType.First)));
            SetCurrencyAmount(ResourceType.Second, 
                _saveLoadService.LoadInt(_config.GetCurrencySaveKeyByType(ResourceType.Second)));
        }

        public bool Has(ResourceType resourceType, int amount)
        {
            return GetCurrencyAmount(resourceType) >= amount;
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
            
            BankUpdated?.Invoke(new BankMessageData(resourceType, 
                newAmount - amount,
                newAmount));
        }

        public void Dispose()
        {
            _saveLoadService.SaveInt(_firstCurrency, _config.GetCurrencySaveKeyByType(ResourceType.First));
            _saveLoadService.SaveInt(_secondCurrency, _config.GetCurrencySaveKeyByType(ResourceType.Second));
        }
    }
}