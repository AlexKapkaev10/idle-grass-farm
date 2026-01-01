using Project.ScriptableObjects;
using Project.Services;
using VContainer;

namespace Project.Game
{
    public class AbilityService : IAbilityService
    {
        private readonly IBankService _bankService;
        private readonly IInventoryService _inventoryService;
        private readonly AbilityServiceConfig _config;

        private int _toolLevel = 1;

        public float MowRange { get; private set; }

        [Inject]
        public AbilityService(IBankService bankService, IInventoryService inventoryService, AbilityServiceConfig config)
        {
            _bankService = bankService;
            _inventoryService = inventoryService;
            _config = config;

            _toolLevel = _config.StartToolLevel;
            SetMowRange(_config.GetToolRangeByLevel(_toolLevel));
        }

        public void UpdateLevel(AbilityType type)
        {
            switch (type)
            {
                case AbilityType.Tool:
                    _toolLevel++;
                    SetMowRange(_config.GetToolRangeByLevel(_toolLevel));
                    break;
                case AbilityType.Inventory:
                    _inventoryService.UpgradeLevel();
                    break;
            }
        }

        public void TryUpgrade(AbilityType type, UpgradeRecipe recipe)
        {
            if (recipe != null)
            {
                if (_bankService.Has(ResourceType.First, recipe.FirstCurrencyAmount) &&
                    _bankService.Has(ResourceType.Second, recipe.SecondCurrencyAmount))
                {
                    _bankService.SetCurrencyAmount(ResourceType.First, -recipe.FirstCurrencyAmount);
                    _bankService.SetCurrencyAmount(ResourceType.Second, -recipe.SecondCurrencyAmount);
                    UpdateLevel(type);
                }
            }
        }

        public int GetNextLevel(AbilityType type)
        {
            return type == AbilityType.Tool ? _toolLevel + 1 : _inventoryService.Level + 1;
        }

        private void SetMowRange(float mowRange)
        {
            MowRange = mowRange;
        }
    }
}