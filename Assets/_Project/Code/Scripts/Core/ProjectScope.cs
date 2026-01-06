using Project.Input;
using Project.ScriptableObjects;
using Project.Services;
using Project.UI.MVP;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Core
{
    public sealed class ProjectScope : LifetimeScope
    {
        [SerializeField] private LoaderPresenterConfig _loaderPresenterConfig;
        [SerializeField] private AudioServiceConfig _audioServiceConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneLoadService>(Lifetime.Singleton)
                .As<ISceneLoadService>();
            
            builder.Register<SaveLoadService>(Lifetime.Singleton)
                .As<ISaveLoadService>();
            
            builder.Register<InputService>(Lifetime.Singleton)
                .As<IInputService>();
            
            builder.Register<LoaderPresenter>(Lifetime.Singleton)
                .As<ILoaderPresenter>()
                .WithParameter(_loaderPresenterConfig);
            
            builder.Register<AudioService>(Lifetime.Singleton)
                .As<IAudioService>()
                .As<IStartable>()
                .WithParameter(_audioServiceConfig);
        }
    }
}