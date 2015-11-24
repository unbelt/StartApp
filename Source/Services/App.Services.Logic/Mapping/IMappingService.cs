namespace App.Services.Logic.Mapping
{
    public interface IMappingService
    {
        T Map<T>(object source);
    }
}
