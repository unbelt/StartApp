namespace App.Server.Infrastructure.Auth
{
    using System.Threading;

    using Microsoft.AspNet.Identity;

    public class AspNetUserProvider : IUserProvider
    {
        public string GetUserId()
        {
            return Thread.CurrentPrincipal.Identity.GetUserId();
        }
    }
}
