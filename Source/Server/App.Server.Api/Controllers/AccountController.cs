namespace App.Server.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;

    using App.Data.Models;
    using App.Server.DataTransferModels.User;
    using App.Services.Logic.Mapping;

    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Testing;

    public class AccountController : BaseController
    {
        private readonly IMappingService mappingService;

        public AccountController(IMappingService mappingService)
        {
            this.mappingService = mappingService;
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // GET api/Account/GetAll
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var users = await UserManager.Users.ToListAsync();
            var response = this.mappingService.Map<IList<UserResponseModel>>(users);

            if (!response.Any())
            {
                return this.BadRequest("The user list is empty!");
            }

            return this.Ok(response);
        }

        // GET api/Account/Get/{username}
        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                user = await UserManager.FindByNameAsync(id);

                if (user == null)
                {
                    return this.BadRequest("User not found!");
                }
            }

            var responseModel = this.mappingService.Map<UserResponseModel>(user);

            return this.Ok(responseModel);
        }

        // POST api/Account/Login
        [HttpPost]
        public async Task<IHttpActionResult> Login(LoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var user = UserManager.FindAsync(model.UserName, model.Password);

            if (user == null)
            {
                return this.BadRequest("Wrong username or password!");
            }

            // Invoke the "token" OWIN service to perform the login (POST /api/token)
            var testServer = TestServer.Create<Startup>();
            var requestParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", model.UserName),
                new KeyValuePair<string, string>("password", model.Password)
            };

            var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);

            var tokenServiceResponse = await testServer.HttpClient
                .PostAsync(Common.Constants.TokenEndpointPath, requestParamsFormUrlEncoded);

            return this.ResponseMessage(tokenServiceResponse);
        }

        // POST api/Account/Register
        [HttpPost]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var user = this.mappingService.Map<User>(model);
            var result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            var loginBindingModel =  this.mappingService.Map<LoginBindingModel>(model);
            var loginResult = await this.Login(loginBindingModel);

            return loginResult;
        }

        // POST api/Account/Logout
        [HttpPost]
        [Authorize]
        public IHttpActionResult Logout()
        {
            this.Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return this.Ok();
        }

        // POST api/Account/ChangePassword
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var result = await UserManager
                .ChangePasswordAsync(this.User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok();
        }

        // POST api/Account/SetPassword
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var result = await UserManager
                .AddPasswordAsync(this.User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok();
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
                        this.ModelState.AddModelError(string.Empty, error);
                    }
                }

                if (this.ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return this.BadRequest();
                }

                return this.BadRequest(this.ModelState);
            }

            return null;
        }

        private static class RandomOAuthStateGenerator
        {
            private static readonly RandomNumberGenerator Random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int BitsPerByte = 8;

                if (strengthInBits % BitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / BitsPerByte;

                byte[] data = new byte[strengthInBytes];
                Random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
