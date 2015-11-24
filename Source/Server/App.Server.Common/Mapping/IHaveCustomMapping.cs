using AutoMapper;

namespace App.Server.Common.Mapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(IConfiguration configuration);
    }
}
