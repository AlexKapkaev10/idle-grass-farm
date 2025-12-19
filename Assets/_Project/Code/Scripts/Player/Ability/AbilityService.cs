using Project.ScriptableObjects;
using VContainer;

namespace Project.Game
{
    public class AbilityService : IAbilityService
    {
        private readonly AbilityServiceConfig _config;

        private int _toolLevel = 1;
        private int _inventoryLevel = 1;
        public float MowRange { get; private set; }

        [Inject]
        public AbilityService(AbilityServiceConfig config)
        {
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
                    _inventoryLevel++;
                    break;
            }
        }

        private void SetMowRange(float mowRange)
        {
            MowRange = mowRange;
        }
    }
}