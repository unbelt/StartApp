namespace App.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using App.Data.Models;
    using App.Data.Repositories;
    using App.Services.Data.Contracts;

    public class CategoryService : ICategoryService
    {
        private readonly IDbRepository<Category> categories;

        public CategoryService(IDbRepository<Category> categories)
        {
            this.categories = categories;
        }

        public async Task<Category> AddCategory(Category category)
        {
            this.categories.Add(category);

            await this.categories.SaveChangesAsync();

            return category;
        }

        public async Task DeleteEntity(int id)
        {
            this.categories.Delete(id);

            await this.categories.SaveChangesAsync();
        }

        public async Task<Category> EditCategory(Category category)
        {
            var categoryToEdit = this.GetCategoryById(category.Id).FirstOrDefault();

            if (categoryToEdit == null)
            {
                return null;
            }

            categoryToEdit.Name = category.Name;

            await this.categories.SaveChangesAsync();

            return categoryToEdit;
        }

        public IQueryable<Category> GetAllCategories()
        {
            return this.categories.GetAll();
        }

        public IQueryable<Category> GetCategoryById(int id)
        {
            return this.categories
                .GetAll()
                .Where(c => c.Id == id);
        }
    }
}
