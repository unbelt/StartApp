namespace App.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using App.Data.Common;
    using App.Data.Models;

    public class DbRepository<T> : IDbRepository<T>
        where T : BaseModel<int>
    {
        public DbRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException(Constants.DbContextValidationError, "context");
            }

            this.Context = context;
            this.DbSet = context.Set<T>();
        }

        protected DbSet<T> DbSet { get; set; }

        protected DbContext Context { get; set; }

        public IQueryable<T> GetAll()
        {
            return this.DbSet.Where(x => !x.IsDeleted).AsQueryable();
        }

        public IQueryable<T> GetAllWithDeleted()
        {
            return this.DbSet;
        }

        public T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public void Add(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Added);
        }

        public void Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
        }

        public void Delete(object id)
        {
            var entity = this.GetById(id);
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;
        }

        public void HardDelete(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Deleted);
        }

        public void HardDelete(object id)
        {
            this.ChangeEntityState(this.GetById(id), EntityState.Deleted);
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return this.Context.SaveChangesAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing && this.Context != null)
                {
                    this.Context.Dispose();
                }

                this.disposedValue = true;
            }
        }
        #endregion

        private T ChangeEntityState(T entity, EntityState state)
        {
            var entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = state;

            return entity;
        }
    }
}
