using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Data.Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> GetAll();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
