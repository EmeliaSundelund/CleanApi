using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class AnimalUserDto
    {
        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "AnimalId is required")]
        public Guid AnimalId { get; set; }
    }
}
