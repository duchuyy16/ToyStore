using System.ComponentModel.DataAnnotations;

namespace ToyStoreAPI.Models
{
    public class ResetPasswordModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "NewPassword is required")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "ConfirmNewPassword is required")]
        public string? ConfirmNewPassword { get; set; }

        public string? Token { get; set; }
    }
}
