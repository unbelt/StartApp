namespace App.Server.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using App.Server.DataTransferModels.Category;
    using App.Services.Data.Contracts;

    using AutoMapper.QueryableExtensions;

    [AllowAnonymous] // TODO: For testing purpose only!
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var categories = CacheService
                .Get("categories", () => this.categoryService.GetAllCategories().ProjectTo<CategoryResponseModel>().ToList(), 30 * 60);

            if (!categories.Any())
            {
                return this.BadRequest("The entity list is empty!");
            }

            return this.Ok(categories);
        }
    }
}
