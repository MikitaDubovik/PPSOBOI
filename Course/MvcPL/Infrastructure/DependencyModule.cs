using DependencyResolver;
using Ninject.Modules;

namespace MvcPL.Infrastructure
{
    public class DependencyModule : NinjectModule
    {
        public override void Load() =>
            ResolverConfig.Configure(Kernel);
    }
}