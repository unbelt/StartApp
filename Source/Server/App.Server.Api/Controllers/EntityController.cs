using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;

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
        public async Task<IHttpActionResult> Get(int id)
        {
            var model = await this.entityService
                .GetEntityById(id)
                .Project().To<EntityResponseModel>()
                .FirstAsync();

            return this.Ok(model);
        }
    }
}
