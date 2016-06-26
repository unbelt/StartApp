namespace App.Data.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using App.Data.Models;

    using Microsoft.AspNet.Identity;

    public sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            // TODO: Remove in production
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true; 
        }

        protected override void Seed(AppDbContext context)
        {
            SeedUsers(context);
            SeedCategories(context);
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

        private static void SeedCategories(AppDbContext context)
        {
            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Other"
                }
            };

            foreach (var category in categories)
            {
                if (!context.Categories.Any(x => x.Name == category.Name))
                {
                    context.Categories.Add(category);
                }
            }

            context.SaveChanges();
        }

        private static void SeedEntities(AppDbContext context)
        {
            var entities = new List<Entity>
            {
                new Entity
                {
                    Title = "Welcome",
                    Content = "Hello world!",
                    Category = context.Categories.FirstOrDefault(c => c.Name == "Other"),
                    User = context.Users.FirstOrDefault(u => u.UserName == "admin")
                },
                new Entity
                {
                    Title = "Bye",
                    Content = "Goodbye world!",
                    Category = context.Categories.FirstOrDefault(c => c.Name == "Other"),
                    User = context.Users.FirstOrDefault(u => u.UserName == "user")
                }
            };

            foreach (var entity in entities)
            {
                if (!context.Entities.Any(x => x.Title == entity.Title))
                {
                    context.Entities.Add(entity);
                }
            }
        }
    }
}
