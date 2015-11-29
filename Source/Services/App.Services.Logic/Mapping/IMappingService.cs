using App.Services.Data.Contracts;

namespace App.Services.Logic.Mapping
{
    public interface IMappingService : IService
    {
        T Map<T>(object source);
    }
}
