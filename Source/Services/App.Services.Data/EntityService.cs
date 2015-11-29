using System.Linq;

using App.Data.Models;
using App.Data.Repository;
using App.Services.Data.Contracts;

namespace App.Services.Data
{
    public class EntityService : IEntityService
    {
        private readonly IRepository<Entity> entities;

        public EntityService(IRepository<Entity> entities)
        {
            this.entities = entities;
        }

        public IQueryable<Entity> GetAllEntities()
        {
            return this.entities.GetAll();
        }

        public IQueryable<Entity> GetEntityById(int id)
        {
            return this.entities.GetAll()
                .Where(e => e.Id == id);
        }
    }
}
