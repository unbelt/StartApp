namespace App.Server.Api
{
    using System.Reflection;
    using System.Web.Http;

    using App.Server.Api.Config;
    using App.Server.Common;

    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load(Constants.DataTransferModelsAssembly));

            var httpConfig = new HttpConfiguration();

            WebApiConfig.Register(httpConfig);
            Config.Startup.ConfigureAuth(app);

            httpConfig.EnsureInitialized();

            app
                .UseNinjectMiddleware(NinjectConfig.CreateKernel)
                .UseNinjectWebApi(httpConfig);
        }
    }
}
