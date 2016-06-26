namespace App.Server.DataTransferModels.Entity
{
    using App.Data.Models;
    using App.Server.Common.Mapping;

    using AutoMapper;

    public class EntityResponseModel : BaseModel<int>, IMapFrom<Entity>, IHaveCustomMapping
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public string UserName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Entity, EntityResponseModel>()
                .ForMember(c => c.Category, opt => opt.MapFrom(c => c.Category.Name))
                .ForMember(u => u.UserName, opt => opt.MapFrom(u => u.User.UserName));
        }
    }
}
