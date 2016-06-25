namespace App.Server.Api.Config
{
    using System;
    using System.Data.Entity;
    using System.Web;

    using App.Data;
    using App.Data.Repositories;
    using App.Server.Common;
    using App.Server.Infrastructure.Auth;
    using App.Services.Data.Contracts;
    using App.Services.Logic;

    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;

    public static class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                ObjectFactory.Initialize(kernel);
                RegisterServices(kernel);

                return kernel;
            }
            catch (Exception)
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IDbRepository<>)).To(typeof(DbRepository<>));
            kernel.Bind<DbContext>().To<AppDbContext>().InRequestScope();
            kernel.Bind<IUserProvider>().To<AspNetUserProvider>();

            kernel.Bind(k => k
                .From(
                    Constants.InfrastructureAssembly,
                    Constants.DataServicesAssembly,
                    Constants.LogicServicesAssembly)
                .SelectAllClasses()
                .InheritedFrom<IService>()
                .BindDefaultInterface());
        }
    }
}
