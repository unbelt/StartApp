using System;

using App.Services.Data.Contracts;

namespace App.Services.Logic
{
    public interface IObjectFactory : IService
    {
        T GetInstance<T>();

        object GetInstance(Type type);

        T TryGetInstance<T>();

        object TryGetInstance(Type type);
    }
}
