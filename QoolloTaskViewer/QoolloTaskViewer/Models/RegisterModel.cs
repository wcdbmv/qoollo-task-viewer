using System.ComponentModel.DataAnnotations;

namespace QoolloTaskViewer.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password required")]
        [Compare("Password", ErrorMessage = "Wrong password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
