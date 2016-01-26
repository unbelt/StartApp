namespace App.Server.DataTransferModels.Entity
{
    using System;

    using App.Server.Common.Mapping;

    using AutoMapper;

    public class EntityResponseModel : IMapFrom<Data.Models.Entity>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string User { get; set; }

        public DateTime DateCreated { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Data.Models.Entity, EntityResponseModel>()
                .ForMember(u => u.User, opt => opt.MapFrom(u => u.User.UserName));
        }
    }
}
