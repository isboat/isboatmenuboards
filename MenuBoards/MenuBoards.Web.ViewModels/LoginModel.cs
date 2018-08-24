using System.ComponentModel.DataAnnotations;

namespace MenuBoards.Web.ViewModels
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Message { get; set; }
    }
}