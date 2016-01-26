namespace App.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using App.Data.Common;

    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(DbContext context)
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

        public void Add(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Added);
        }

        public void Delete(object id)
        {
            this.ChangeEntityState(this.GetById(id), EntityState.Deleted);
        }

        public void Delete(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Deleted);
        }

        public IQueryable<T> GetAll()
        {
            return this.DbSet.AsQueryable();
        }

        public T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return this.Context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
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
