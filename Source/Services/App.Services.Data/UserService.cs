using App.Data.Models;
using App.Data.Repository;
using App.Services.Data.Contracts;

namespace App.Services.Data
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> users;

        public UserService(IRepository<User> users)
        {
            this.users = users;
        }
    }
}
