using MarketingPostManager.Web.Configuration;
using MediatR;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Modules;
using Ninject.Planning.Bindings.Resolvers;

namespace MarketingPostManager.Web.App_Start
{
    public class NinjectConfiguration : NinjectModule
        {
        // use this when I set up automapper
        //static NinjectConfiguration()
        //{
        //    Mapper.Initialize(cfg => { cfg.AddProfile<PublisherManagementProfile>(); });
        //}

        public override void Load()
        {
            Kernel.Components.Add<IBindingResolver, ContravariantBindingResolver>();

            Kernel.Bind(scan => scan.FromThisAssembly().SelectAllClasses().BindAllInterfaces());
            Kernel.Bind(scan => scan.FromAssemblyContaining<IMediator>().SelectAllClasses().BindDefaultInterface());

            // authentication
            //Kernel.BindHttpFilter<ApiKeyAuthenticationFilter>(FilterScope.Global).InSingletonScope();
            
            Bind<SingleInstanceFactory>().ToMethod(ctx => t => ctx.Kernel.Get(t));
            Bind<MultiInstanceFactory>().ToMethod(ctx => t => ctx.Kernel.GetAll(t));

            // authentication
            //Bind<IPrincipal>().ToMethod(context => HttpContext.Current.User).InRequestScope();

            Rebind<IConnectionStringConfiguration>().To<ConnectionStringConfiguration>().InSingletonScope();
        }
    }
}