namespace App.Server.DataTransferModels.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using App.Server.Common.Mapping;

    public class EntityRequestModel : IMapFrom<Data.Models.Entity>
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string UserId { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}
