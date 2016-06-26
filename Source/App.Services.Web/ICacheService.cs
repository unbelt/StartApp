namespace App.Services.Web
{
    using System;

    using App.Services.Data.Contracts;

    public interface ICacheService : IService
    {
        T Get<T>(string itemName, Func<T> getDataFunc, int durationInSeconds);

        void Remove(string itemName);
    }
}
