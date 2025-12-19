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
    
    public class GameSceneService : IGameSceneService
    {
        private readonly ILoaderPresenter _loaderPresenter;
        private readonly IInputService _inputService;
        private readonly IJoystickPresenter _joystickPresenter;
        private readonly GameSceneServiceConfig _config;

        [Inject]
        public GameSceneService(IInputService inputService, 
            ILoaderPresenter loaderPresenter,
            IJoystickPresenter joystickPresenter,
            GameSceneServiceConfig config)
        {
            _inputService = inputService;
            _loaderPresenter = loaderPresenter;
            _joystickPresenter = joystickPresenter;
            _config = config;
        }

        public void Initialize()
        {
            _loaderPresenter.SetActiveView(false);
            _inputService.SwitchMap(InputMapType.Player);

            if (_config.NeedJoystick)
            {
                _joystickPresenter.SetActiveView(true);
            }
        }
    }
}