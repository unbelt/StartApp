namespace App.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using App.Data.Models;

    public interface IEntityService : IService
    {
        IQueryable<Entity> GetAllEntities();

        IQueryable<Entity> GetEntityById(int id);

        Task<Entity> AddEntity(Entity entity);

        Task<Entity> EditEntity(Entity entity);

        Task DeleteEntity(int id);
    }
}
