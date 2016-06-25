namespace App.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using App.Data.Models;

    public interface IDbRepository<T> : IRepository<T, int>
        where T : BaseModel<int>
    {
    }

    public interface IRepository<T, in TKey> : IDisposable
        where T : BaseModel<TKey>
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetAllWithDeleted();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        void HardDelete(T entity);

        void HardDelete(object id);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
