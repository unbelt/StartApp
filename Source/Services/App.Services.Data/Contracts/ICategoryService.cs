namespace App.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using App.Data.Models;

    public interface ICategoryService
    {
        IQueryable<Category> GetAllCategories();

        IQueryable<Category> GetCategoryById(int id);

        Task<Category> AddCategory(Category category);

        Task<Category> EditCategory(Category category);

        Task DeleteEntity(int id);
    }
}
