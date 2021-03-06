﻿namespace App.Server.DataTransferModels.User
{
    using System.ComponentModel.DataAnnotations;

    using App.Server.Common.Mapping;

    public class RegisterBindingModel : IMapFrom<Data.Models.User>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
