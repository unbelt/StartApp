namespace App.Server.Infrastructure.Auth
{
    public interface IUserProvider
    {
        string GetUserId();
    }
}
