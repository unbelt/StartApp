namespace App.Server.DataTransferModels.User
{
    using System.ComponentModel.DataAnnotations;

    using App.Server.Common.Mapping;

    public class LoginBindingModel : IMapFrom<RegisterBindingModel>
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
