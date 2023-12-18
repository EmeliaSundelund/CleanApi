using System.ComponentModel.DataAnnotations;
using Domain.Models.AnimalUser;

namespace Domain.Models.Animal
{
    public class AnimalModel
    {
        [Key]
        public Guid AnimalId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<AnimalUserModel> AnimalUsers { get; set; }
    }
}
