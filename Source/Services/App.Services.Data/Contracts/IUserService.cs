using System.Linq;

using App.Data.Models;

namespace App.Services.Data.Contracts
{
    public interface IUserService : IService
    {
        IQueryable<User> GetAllUsers();

        User GetUser(string id);
    }
}
