using System.ComponentModel.DataAnnotations;

namespace App.Server.DataTransferModels.User
{
    public class LoginBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
