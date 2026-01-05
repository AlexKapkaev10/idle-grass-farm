using Project.ScriptableObjects;
using Project.Services;
using VContainer;

namespace Project.Game
{
    public class AbilityService : IAbilityService
    {
        private readonly IBankService _bankService;
        private readonly IInventoryService _inventoryService;
        private readonly IToolService _toolService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly AbilityServiceConfig _config;

        [Inject]
        public AbilityService(IBankService bankService, 
            IInventoryService inventoryService,
            IToolService toolService,
            ISaveLoadService saveLoadService,
            AbilityServiceConfig config)
        {
            _bankService = bankService;
            _inventoryService = inventoryService;
            _toolService = toolService;
            _saveLoadService = saveLoadService;
            _config = config;

            var toolLevel = _saveLoadService.LoadInt(_config.GetSaveToolLevelKey(), 1);
            var inventoryLevel = _saveLoadService.LoadInt(_config.GetSaveInventoryLevelKey(), 1);
            
            _toolService.Initialize(toolLevel, 
                _config.GetRecipe(AbilityType.Tool, toolLevel).Value);
            
            _inventoryService.Initialize(inventoryLevel, 
                _config.GetRecipe(AbilityType.Inventory, inventoryLevel).Value);
        }

        public void UpdateLevel(AbilityType type)
        {
            var recipe = _config.GetRecipe(type, GetNextLevel(type));
            
            _bankService.SetCurrencyAmount(ResourceType.First, -recipe.FirstCurrencyAmount);
            _bankService.SetCurrencyAmount(ResourceType.Second, -recipe.SecondCurrencyAmount);
            
            switch (type)
            {
                case AbilityType.Tool:
                    _toolService.UpgradeLevel(recipe.Value);
                    break;
                case AbilityType.Inventory:
                    _inventoryService.UpgradeLevel(recipe.Value);
                    break;
            }
        }

        public bool HasUpgrade(AbilityType type)
        {
            var recipe = _config.GetRecipe(type, GetNextLevel(type));
            
            if (recipe == null)
            {
                return false;
            }

            if (_bankService.Has(ResourceType.First, recipe.FirstCurrencyAmount) &&
                _bankService.Has(ResourceType.Second, recipe.SecondCurrencyAmount))
            {
                return true;
            }
            
            return false;
        }

        public float GetMowRange()
        {
            return _toolService.GetMowRange();
        }

        public int GetNextLevel(AbilityType type)
        {
            return type == AbilityType.Tool 
                ? _toolService.GetLevel() + 1 
                : _inventoryService.GetLevel() + 1;
        }

        public void Dispose()
        {
            _saveLoadService.SaveInt(_inventoryService.GetLevel(), _config.GetSaveInventoryLevelKey());
            _saveLoadService.SaveInt(_toolService.GetLevel(), _config.GetSaveToolLevelKey());
        }
    }
}