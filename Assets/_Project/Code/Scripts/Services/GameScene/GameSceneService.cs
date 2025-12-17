using Project.Input;
using Project.UI.MVP;
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

        public GameSceneService(ILoaderPresenter loaderPresenter, IInputService inputService)
        {
            _loaderPresenter = loaderPresenter;
            _inputService = inputService;
        }

        public void Initialize()
        {
            _loaderPresenter.SetActiveView(false);
            _inputService.SwitchMap(InputMapType.Player);
        }
    }
}