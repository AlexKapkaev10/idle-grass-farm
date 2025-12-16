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

        public GameSceneService(ILoaderPresenter loaderPresenter)
        {
            _loaderPresenter = loaderPresenter;
        }

        public void Initialize()
        {
            _loaderPresenter.SetActiveView(false);
        }
    }
}