using System.ComponentModel.DataAnnotations;

namespace Aasaan_Admin_Form.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Email ID is required")]
        [Display(Name = "User Email")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
