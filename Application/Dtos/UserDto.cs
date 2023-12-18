namespace Application.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public required string Password { get; set; }

    }
}
