using System.ComponentModel.DataAnnotations;

namespace ToyStoreAPI.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "NewPassword is required")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "ConfirmNewPassword is required")]
        public string? ConfirmNewPassword { get; set; }

        public string? Token { get; set; }
    }
}
