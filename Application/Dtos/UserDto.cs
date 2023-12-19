namespace Application.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; } = string.Empty;
        public required string Password { get; set; }

    }
}
