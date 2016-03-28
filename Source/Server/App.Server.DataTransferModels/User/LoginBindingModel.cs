namespace App.Server.DataTransferModels.User
{
    using System.ComponentModel.DataAnnotations;

    using App.Server.Common.Mapping;

    public class LoginBindingModel : IMapFrom<RegisterBindingModel>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
