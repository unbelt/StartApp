using System.Threading;

using Microsoft.AspNet.Identity;

namespace App.Server.Infrastructure.Auth
{
    public class AspNetUserProvider : IUserProvider
    {
        public string GetUserId()
        {
            return Thread.CurrentPrincipal.Identity.GetUserId();
        }
    }
}
