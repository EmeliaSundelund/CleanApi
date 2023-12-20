using System.ComponentModel.DataAnnotations;
using Domain.Models.AnimalUser;

namespace Domain.Models.Person
{
    public class UserModel
    {
        [Key]
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<AnimalUserModel> AnimalUsers { get; set; }
    }
}