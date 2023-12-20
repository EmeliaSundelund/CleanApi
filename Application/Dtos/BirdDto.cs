using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class BirdDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        public bool CanFly { get; set; }
        [Required(ErrorMessage = "Color is required")]
        public required string Color { get; set; }
    }
}