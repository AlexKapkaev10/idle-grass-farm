using Project.UI.MVP;

namespace Project.Services
{
    public sealed class BootstrapSceneService : IBootstrapSceneService
    {
        private readonly ISceneLoadService _sceneLoadService;
        private readonly ILoaderPresenter _loaderPresenter;

        public BootstrapSceneService(ISceneLoadService sceneLoadService, ILoaderPresenter loaderPresenter)
        {
            _sceneLoadService = sceneLoadService;
            _loaderPresenter = loaderPresenter;
        }
        
        public void Initialize()
        {
            _loaderPresenter.SetActiveView(true);
            _sceneLoadService.LoadScene(SceneNameType.Game);
        }
    }
}