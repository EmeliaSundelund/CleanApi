using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }

    }
}
