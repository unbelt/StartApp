namespace App.Services.Data
{
    using App.Data.Models;
    using App.Data.Repositories;
    using App.Services.Data.Contracts;

    public class UserService : IUserService
    {
        private readonly IRepository<User> users;

        public UserService(IRepository<User> users)
        {
            this.users = users;
        }
    }
}
