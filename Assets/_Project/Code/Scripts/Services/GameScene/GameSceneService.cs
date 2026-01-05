using Project.Game;
using Project.Input;
using Project.ScriptableObjects;
using Project.UI.MVP;
using VContainer;
using VContainer.Unity;

namespace Project.Services
{
    public interface IGameSceneService : IInitializable
    {
        
    }
    
    public sealed class GameSceneService : IGameSceneService
    {
        private readonly IInputService _inputService;
        private readonly ITradeService _tradeService;
        private readonly ILoaderPresenter _loaderPresenter;
        private readonly IJoystickPresenter _joystickPresenter;
        private readonly IInfoPresenter _infoPresenter;
        private readonly GameSceneServiceConfig _config;

        [Inject]
        public GameSceneService(IInputService inputService,
            ITradeService tradeService,
            ILoaderPresenter loaderPresenter,
            IJoystickPresenter joystickPresenter,
            IInfoPresenter infoPresenter,
            GameSceneServiceConfig config)
        {
            _inputService = inputService;
            _tradeService = tradeService;
            _loaderPresenter = loaderPresenter;
            _joystickPresenter = joystickPresenter;
            _infoPresenter = infoPresenter;
            _config = config;
        }

        public void Initialize()
        {
            _loaderPresenter.SetActiveView(false);
            _inputService.SwitchMap(InputMapType.Player);
            _tradeService.Initialize();

            if (_config.NeedJoystick)
            {
                _joystickPresenter.SetActiveView(true);
            }
            
            _infoPresenter.SetActiveView(true);
        }
    }
}