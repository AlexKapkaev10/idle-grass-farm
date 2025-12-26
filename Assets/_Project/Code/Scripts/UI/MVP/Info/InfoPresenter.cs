using System;
using Project.Game;
using Project.ScriptableObjects;
using Project.Services;
using VContainer;
using Object = UnityEngine.Object;

namespace Project.UI.MVP
{
    public interface IInfoPresenter : IPresenter, IDisposable
    {
        
    }
    
    public sealed class InfoPresenter : IInfoPresenter
    {
        private readonly IInventoryService _inventoryService;
        private readonly IBankService _bankService;
        private readonly InfoPresenterConfig _config;

        private IInfoView _view;

        [Inject]
        public InfoPresenter(IInventoryService inventoryService, IBankService bankService, InfoPresenterConfig config)
        {
            _inventoryService = inventoryService;
            _bankService = bankService;
            _config = config;

            _inventoryService.InventoryUpdated += OnInventoryUpdate;
            _bankService.BankUpdated += OnBankUpdated;
        }

        private void OnBankUpdated(BankMessageData data)
        {
            _view.SetCurrencyAmount(data.ResourceType, data.OldAmount, data.NewAmount);
        }

        private int test;

        private void OnInventoryUpdate(ResourceType type, int amount)
        {
            switch (type)
            {
                case ResourceType.First:
                    _view.GreenSlider.SetAmount(amount);
                    _view.GreenSlider
                        .UpdateText($"{_inventoryService.GetResourceAmount(ResourceType.First)}/{_inventoryService.GetCapacity()}");
                    break;
                case ResourceType.Second:
                    _view.YellowSlider.SetAmount(amount);
                    _view.YellowSlider
                        .UpdateText($"{_inventoryService.GetResourceAmount(ResourceType.Second)}/{_inventoryService.GetCapacity()}");
                    break;
            }
        }

        public void SetActiveView(bool isActive)
        {
            if (isActive)
            {
                _view = Object.Instantiate(_config.InfoViewPrefab);
                
                _view.GreenSlider.SetData(
                    _inventoryService.GetResourceAmount(ResourceType.First), 
                    _inventoryService.GetCapacity());
                
                _view.YellowSlider.SetData(
                    _inventoryService.GetResourceAmount(ResourceType.Second), 
                    _inventoryService.GetCapacity());
                
                _view.GreenSlider
                    .UpdateText($"{_inventoryService.GetResourceAmount(ResourceType.First)}/{_inventoryService.GetCapacity()}");
                
                _view.YellowSlider
                    .UpdateText($"{_inventoryService.GetResourceAmount(ResourceType.Second)}/{_inventoryService.GetCapacity()}");
                
                _view.SetCurrencyAmount(ResourceType.First, 0, _bankService.GetCurrencyAmount(ResourceType.First));
                _view.SetCurrencyAmount(ResourceType.Second, 0, _bankService.GetCurrencyAmount(ResourceType.Second));
            }
            else
            {
                _view?.Destroy();
                _view = null;
            }
        }

        public void Dispose()
        {
            _inventoryService.InventoryUpdated -= OnInventoryUpdate;
            _bankService.BankUpdated -= OnBankUpdated;
        }
    }
}