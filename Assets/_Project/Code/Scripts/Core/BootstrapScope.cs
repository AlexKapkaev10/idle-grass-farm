using Project.Services;
using VContainer;
using VContainer.Unity;

namespace Project.Core
{
    public class BootstrapScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BootstrapSceneService>(Lifetime.Scoped)
                .As<IBootstrapSceneService>();
        }
    }
}