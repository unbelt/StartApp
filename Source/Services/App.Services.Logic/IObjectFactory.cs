namespace App.Services.Logic
{
    using System;

    using App.Services.Data.Contracts;

    public interface IObjectFactory : IService
    {
        T GetInstance<T>();

        object GetInstance(Type type);

        T TryGetInstance<T>();

        object TryGetInstance(Type type);
    }
}
