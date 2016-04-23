namespace App.Server.Api.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    using App.Data.Models;
    using App.Server.Api.Config;
    using App.Server.DataTransferModels.Entity;
    using App.Services.Data.Contracts;
    using App.Services.Logic.Mapping;

    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity.Owin;

    [AllowAnonymous] // TODO: For testing purpose only!
    public class EntityController : BaseController
    {
        private readonly IMappingService mappingService;
        private readonly IEntityService entityService;

        public EntityController(IMappingService mappingService, IEntityService entityService)
        {
            this.mappingService = mappingService;
            this.entityService = entityService;
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
                return this.BadRequest("The entity list is empty!");
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
                return this.BadRequest("Entity not found!");
            }

            return this.Ok(model);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(EntityRequestModel requestModel)
        {
            var user = await UserManager.FindByNameAsync(requestModel.UserId);

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
            responseModel.UserName = user.UserName;

            return this.Ok(responseModel);
        }

        [HttpPatch]
        public async Task<IHttpActionResult> Edit(EntityRequestModel requestModel)
        {
            var entity = this.mappingService.Map<Entity>(requestModel);

            var editedEntity = await this.entityService.EditEntity(entity);

            if (editedEntity == null)
            {
                return this.BadRequest("Entity not found!");
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
