﻿namespace App.Data
{
    using System.Data.Entity;

    using App.Data.Common;
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
    }
}
