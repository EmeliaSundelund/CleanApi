using System.ComponentModel.DataAnnotations;

namespace Domain.Models.AnimalUser
{
    public class AnimalUserModel
    {
        [Key]
        public Guid AnimalUserId { get; set; }
        public Guid AnimalId { get; set; }
        public Guid UserId { get; set; }
        public Animal.AnimalModel Animal { get; set; }
        public Person.UserModel User { get; set; }

    }
}