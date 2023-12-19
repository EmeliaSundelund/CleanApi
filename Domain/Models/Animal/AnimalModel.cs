using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.AnimalUser;

namespace Domain.Models.Animal
{
    public class AnimalModel
    {
        [Key]
        public Guid AnimalId { get; set; }
        public string Name { get; set; } = string.Empty;

        [InverseProperty("Animal")]
        public List<AnimalUserModel> AnimalUsers { get; set; }
    }
}
