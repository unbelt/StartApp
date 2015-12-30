using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

using App.Data.Models;
using App.Services.Data.Contracts;
using App.Services.Logic.Mapping;
using App.Server.DataTransferModels.Entity;

using AutoMapper.QueryableExtensions;

namespace App.Server.Api.Controllers
{
    [AllowAnonymous] // TODO: For testing porpouse only!
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
                return this.NotFound();
            }

            return this.Ok(model);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(EntityRequestModel requestModel)
        {
            var entity = this.mappingService.Map<Entity>(requestModel);
            var addedEntity = await this.entityService.AddEntity(entity);
            var responseModel = this.mappingService.Map<EntityResponseModel>(addedEntity);

            return this.Ok(responseModel);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Edit(EntityRequestModel requestModel)
        {
            var entity = this.mappingService.Map<Entity>(requestModel);
            var editedEntity = await this.entityService.EditEntity(entity);
            var responseModel = this.mappingService.Map<EntityRequestModel>(editedEntity);

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
