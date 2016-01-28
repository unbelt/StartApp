namespace App.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using App.Data.Models;

    using Microsoft.AspNet.Identity;

    public sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true; // TODO: Remove in production
        }

        protected override void Seed(AppDbContext context)
        {
            SeedUsers(context);
            SeedEntities(context);
        }

        private static void SeedUsers(AppDbContext context)
        {
            var passwordHash = new PasswordHasher();
            var password = passwordHash.HashPassword("pass123");

            var users = new List<User>
            {
                new User
                {
                    UserName = "admin",
                    Email = "admin@app.com",
                    PasswordHash = password,
                    SecurityStamp = "pass"
                },
                new User
                {
                    UserName = "user",
                    Email = "user@app.com",
                    PasswordHash = password,
                    SecurityStamp = "pass"
                }
            };

            foreach (var user in users)
            {
                context.Users.AddOrUpdate(u => u.UserName, user);
            }

            context.SaveChanges();
        }

        private static void SeedEntities(AppDbContext context)
        {
            var posts = new List<Entity>
            {
                new Entity
                {
                    Title = "Welcome",
                    Content = "Hello world!",
                    User = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                    DateCreated = new DateTime(2015, 09, 20)
                },
                new Entity
                {
                    Title = "Bye",
                    Content = "Goodbye world!",
                    User = context.Users.FirstOrDefault(u => u.UserName == "user"),
                    DateCreated = new DateTime(2015, 09, 25)
                }
            };

            foreach (var post in posts)
            {
                if (!context.Entities.Any(x => x.Title == post.Title))
                {
                    context.Entities.Add(post);
                }
            }
        }
    }
}
