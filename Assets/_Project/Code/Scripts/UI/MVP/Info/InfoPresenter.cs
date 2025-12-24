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
        private readonly InfoPresenterConfig _config;

        private IInfoView _view;

        [Inject]
        public InfoPresenter(IInventoryService inventoryService, InfoPresenterConfig config)
        {
            _config = config;
            _inventoryService = inventoryService;
            
            _inventoryService.InventoryUpdated += OnInventoryUpdate;
        }

        private void OnInventoryUpdate(ResourceType type, int amount)
        {
            switch (type)
            {
                case ResourceType.Green:
                    _view.GreenSlider.SetAmount(amount);
                    _view.GreenSlider
                        .UpdateText($"{_inventoryService.GetResourceAmount(ResourceType.Green)}/{_inventoryService.GetCapacity()}");
                    break;
                case ResourceType.Yellow:
                    _view.YellowSlider.SetAmount(amount);
                    _view.YellowSlider
                        .UpdateText($"{_inventoryService.GetResourceAmount(ResourceType.Yellow)}/{_inventoryService.GetCapacity()}");
                    break;
            }
        }

        public void SetActiveView(bool isActive)
        {
            if (isActive)
            {
                _view = Object.Instantiate(_config.InfoViewPrefab);
                
                _view.GreenSlider.SetData(
                    _inventoryService.GetResourceAmount(ResourceType.Green), 
                    _inventoryService.GetCapacity());
                
                _view.YellowSlider.SetData(
                    _inventoryService.GetResourceAmount(ResourceType.Yellow), 
                    _inventoryService.GetCapacity());
                
                _view.GreenSlider
                    .UpdateText($"{_inventoryService.GetResourceAmount(ResourceType.Green)}/{_inventoryService.GetCapacity()}");
                
                _view.YellowSlider
                    .UpdateText($"{_inventoryService.GetResourceAmount(ResourceType.Yellow)}/{_inventoryService.GetCapacity()}");
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
        }
    }
}