using System;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using App.Data.Models;
using App.Server.Api.Config;
using App.Services.Data.Contracts;
using App.Services.Logic.Mapping;
using App.Server.DataTransferModels.Entity;

using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity.Owin;

namespace App.Server.Api.Controllers
{
    [AllowAnonymous] // TODO: For testing porpouse only!
    public class EntityController : BaseController
    {
        private readonly IMappingService mappingService;
        private readonly IEntityService entityService;

        private ApplicationUserManager userManager;

        public EntityController(IMappingService mappingService, IEntityService entityService)
        {
            this.mappingService = mappingService;
            this.entityService = entityService;
        }

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

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var entities = await this.entityService
                .GetAllEntities()
                .ProjectTo<EntityResponseModel>()
                .ToListAsync();

            if (!entities.Any())
            {
                return this.NotFound();
            }

            return this.Ok(entities);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var model = await this.entityService
                .GetEntityById(id)
                .ProjectTo<EntityResponseModel>()
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return this.BadRequest("Not found!");
            }

            return this.Ok(model);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(EntityRequestModel requestModel)
        {
            var user = await this.UserManager.FindByNameAsync("admin");

            if (user == null)
            {
                return this.BadRequest("User not found!");
            }

            requestModel.UserId = user.Id;

            if (requestModel.DateCreated == null)
            {
                requestModel.DateCreated = DateTime.Now.ToLocalTime();
            }

            var entity = this.mappingService.Map<Entity>(requestModel);
            var addedEntity = await this.entityService.AddEntity(entity);
            var responseModel = this.mappingService.Map<EntityResponseModel>(addedEntity);

            return this.Ok(responseModel);
        }

        [HttpPatch]
        public async Task<IHttpActionResult> Edit(EntityRequestModel requestModel)
        {
            var entity = this.mappingService.Map<Entity>(requestModel);

            var editedEntity = await this.entityService.EditEntity(entity);

            if (editedEntity == null)
            {
                return this.NotFound();
            }

            var responseModel = this.mappingService.Map<EntityResponseModel>(editedEntity);

            return this.Ok(responseModel);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await this.entityService.DeleteEntity(id);

            return this.Ok();
        }
    }
}
