using Project.ScriptableObjects;
using Project.Services;
using VContainer;

namespace Project.Game
{
    public interface IUpgradeController
    {
        void Initialize(UpgradeConfig config);
        void Enter();
        void Exit();
    }
    
    public sealed class UpgradeController : IUpgradeController
    {
        private readonly IAbilityService _abilityService;
        private UpgradeConfig _config;

        [Inject]
        public UpgradeController(IAbilityService abilityService)
        {
            _abilityService = abilityService;
        }

        public void Initialize(UpgradeConfig config)
        {
            _config = config;
        }

        public void Enter()
        {
            _abilityService.TryUpgrade(
                _config.Type, 
                _config.GetRecipeByLevel(_abilityService.GetNextLevel(_config.Type)));
        }

        public void Exit()
        {
            
        }
    }
}