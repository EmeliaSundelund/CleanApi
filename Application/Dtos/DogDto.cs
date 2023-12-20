using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class DogDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Breed is required")]
        public string BreedDog { get; set; }
        [Required(ErrorMessage = "Weight is required")]
        public int WeightDog { get; set; }
    }
}
