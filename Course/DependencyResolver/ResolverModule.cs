using BLL.Interface.Services;
using BLL.Services;
using DAL;
using DAL.Interface.Repository;
using Ninject;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        //public static void ConfigurateResolverWeb(this IKernel kernel)
        //{
        //    Configure(kernel, true);
        //}

        public static void Configure(IKernel kernel)
        {
            kernel.Bind<IPostService>().To<PostService>();
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IPayService>().To<PayService>();
            kernel.Bind<IPostRepository>().To<PostRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<ICommentRepository>().To<CommentRepository>();
            kernel.Bind<IPayRepository>().To<PayRepository>();
            kernel.Bind<IServiceBusConnector>().To<ServiceBusConnector>();
        }
    }
}
