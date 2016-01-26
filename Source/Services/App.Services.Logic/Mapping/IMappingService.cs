namespace App.Services.Logic.Mapping
{
    using App.Services.Data.Contracts;

    public interface IMappingService : IService
    {
        T Map<T>(object source);
    }
}
