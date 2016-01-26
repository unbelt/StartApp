using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using System.Web.Http;

using App.Data.Models;
using App.Server.Api.Config;
using App.Server.DataTransferModels.User;
using App.Services.Data.Contracts;
using App.Services.Logic.Mapping;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace App.Server.Api.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IMappingService mappingService;

        private ApplicationUserManager userManager;

        public AccountController(IMappingService mappingService)
        {
            this.mappingService = mappingService;
        }

        //public AccountController(IMappingService mappingService,
        //    IUserService userService,
        //    ApplicationUserManager userManager,
        //    ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        //{
        //    this.mappingService = mappingService;
        //    this.userService = userService;

        //    this.UserManager = userManager;
        //    this.AccessTokenFormat = accessTokenFormat;
        //}

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

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var users = await this.UserManager.Users.ToListAsync();
            var response = this.mappingService.Map<IList<User>>(users);

            if (!response.Any())
            {
                return this.NotFound();
            }

            return this.Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string userName)
        {
            var user = await this.UserManager.FindByNameAsync(userName);
            var responseModel = this.mappingService.Map<UserResponseModel>(user);

            if (responseModel == null)
            {
                return this.NotFound();
            }

            return this.Ok(responseModel);
        }

        // POST api/Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Login(LoginBindingModel model)
        {
            var user = await this.UserManager.FindAsync(model.Email, model.Password);
            var responseModel = this.mappingService.Map<UserResponseModel>(user);

            if (responseModel == null)
            {
                return this.BadRequest("Wrong username or password!");
            }

            return this.Ok(responseModel);
        }

        // POST api/Account/Register
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = this.mappingService.Map<User>(model);
            var result = await this.UserManager.CreateAsync(user, model.Password);
            var responseModel = this.mappingService.Map<UserResponseModel>(user);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok(responseModel);
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            this.Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return this.Ok();
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok();
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

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return this.Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return this.InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        this.ModelState.AddModelError("", error);
                    }
                }

                if (this.ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return this.BadRequest();
                }

                return this.BadRequest(ModelState);
            }

            return null;
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
