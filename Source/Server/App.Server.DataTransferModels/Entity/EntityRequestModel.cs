using System;
using System.ComponentModel.DataAnnotations;

using App.Server.Common.Mapping;

namespace App.Server.DataTransferModels.Entity
{
    public class EntityRequestModel : IMapFrom<Data.Models.Entity>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string UserId { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}
