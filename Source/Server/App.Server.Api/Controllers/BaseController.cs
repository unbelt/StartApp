namespace App.Server.Api.Controllers
{
    using System.Net.Http;
    using System.Web.Http;

    using App.Server.Api.Config;

    using Microsoft.AspNet.Identity.Owin;

    public class BaseController : ApiController
    {
        private ApplicationUserManager userManager;

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
