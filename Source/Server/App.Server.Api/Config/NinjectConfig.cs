using System;
using System.Data.Entity;
using System.Reflection;

using App.Data;
using App.Data.Repository;
using App.Server.Common;
using App.Server.Infrastructure.Auth;
using App.Services.Data.Contracts;

using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;

namespace App.Server.Api.Config
{
    public static class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            try
            {
                kernel.Load(Assembly.GetExecutingAssembly());
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
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
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
