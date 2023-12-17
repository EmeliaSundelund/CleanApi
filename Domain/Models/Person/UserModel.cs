namespace Domain.Models.Person
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; }
        public ICollection<AnimalUserModel> AnimalUsers { get; set; }
    }
}