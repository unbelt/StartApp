namespace App.Server.DataTransferModels.Entity
{
    using App.Data.Models;
    using App.Server.Common.Mapping;

    using AutoMapper;
    public class EntityResponseModel : BaseModel<int>, IMapFrom<Data.Models.Entity>, IHaveCustomMapping
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string UserName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Data.Models.Entity, EntityResponseModel>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(u => u.User.UserName));
        }
    }
}
