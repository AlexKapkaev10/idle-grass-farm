using Project.Input;
using Project.ScriptableObjects;
using Project.Services;
using Project.UI.MVP;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Core
{
    public class ProjectScope : LifetimeScope
    {
        [SerializeField] private LoaderPresenterConfig _loaderPresenterConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneLoadService>(Lifetime.Singleton)
                .As<ISceneLoadService>();
            
            builder.Register<InputService>(Lifetime.Singleton)
                .As<IInputService>();
            
            builder.Register<LoaderPresenter>(Lifetime.Singleton)
                .As<ILoaderPresenter>()
                .WithParameter(_loaderPresenterConfig);
        }
    }
}