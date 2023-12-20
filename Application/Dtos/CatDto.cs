using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class CatDto
    {
        [Required(ErrorMessage = "UserId is required")]
        public string Name { get; set; } = string.Empty;
        public bool LikesToPlay { get; set; }
        [Required(ErrorMessage = "Breed is required")]
        public required string BreedCat { get; set; }
        [Required(ErrorMessage = "Weight is required")]
        public int WeightCat { get; set; }
    }
}