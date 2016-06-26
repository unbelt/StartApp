namespace App.Server.Api.Controllers
{
    using System.Net.Http;
    using System.Web.Http;

    using App.Server.Api.Config;
    using App.Services.Web;

    using Microsoft.AspNet.Identity.Owin;
    using Ninject;

    public abstract class BaseController : ApiController
    {
        private ApplicationUserManager userManager;

        [Inject]
        public ICacheService CacheService { get; set; }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? this.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.userManager != null)
            {
                this.userManager.Dispose();
                this.userManager = null;
            }

            base.Dispose(disposing);
        }
    }
}
