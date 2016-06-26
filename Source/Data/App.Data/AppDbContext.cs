namespace App.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using App.Data.Common;
    using App.Data.Common.Models;
    using App.Data.Migrations;
    using App.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext()
            : base(Constants.ConnectionString, throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
        }

        public virtual IDbSet<Entity> Entities { get; set; }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync();
        }

        private void ApplyAuditInfoRules()
        {
            foreach (var entry in this.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditInfo &&
                ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
