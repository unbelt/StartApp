namespace App.Server.DataTransferModels.User
{
    using System.ComponentModel.DataAnnotations;

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
