using System.Linq;

using App.Data.Models;

namespace App.Services.Data.Contracts
{
    public interface IEntityService
    {
        IQueryable<Entity> GetEntityById(int id);
    }
}
