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
        private readonly AbilityServiceConfig _config;

        [Inject]
        public AbilityService(IBankService bankService, 
            IInventoryService inventoryService,
            IToolService toolService,
            AbilityServiceConfig config)
        {
            _bankService = bankService;
            _inventoryService = inventoryService;
            _toolService = toolService;
            _config = config;
            
            _toolService.UpgradeLevel(_config.GetRecipe(AbilityType.Tool, 
                GetNextLevel(AbilityType.Tool)).Value);
            
            _inventoryService.UpgradeLevel(_config.GetRecipe(AbilityType.Inventory, 
                GetNextLevel(AbilityType.Inventory)).Value);
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
    }
}