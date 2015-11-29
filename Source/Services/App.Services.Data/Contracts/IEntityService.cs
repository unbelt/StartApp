using System.Linq;

using App.Data.Models;

namespace App.Services.Data.Contracts
{
    public interface IEntityService : IService
    {
        IQueryable<Entity> GetAllEntities();

        IQueryable<Entity> GetEntityById(int id);
    }
}
