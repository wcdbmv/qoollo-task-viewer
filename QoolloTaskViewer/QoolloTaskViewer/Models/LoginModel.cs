using System.ComponentModel.DataAnnotations;

namespace QoolloTaskViewer.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
