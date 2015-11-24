using System.Linq;

using App.Data.Models;
using App.Data.Repository;
using App.Services.Data.Contracts;

namespace App.Services.Data
{
    public class EntityService : IEntityService
    {
        private readonly IRepository<Entity> posts;

        public EntityService(IRepository<Entity> posts)
        {
            this.posts = posts;
        }

        public IQueryable<Entity> GetEntityById(int id)
        {
            return this.posts.GetAll()
                .Where(e => e.Id == id);
        }
    }
}
