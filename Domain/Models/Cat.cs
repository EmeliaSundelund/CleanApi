using Domain.Models.Animal;

namespace Domain.Models
{
    public class Cat : AnimalModel
    {
        public bool LikesToPlay { get; set; }
        public string BreedCat { get; set; }
        public int WeightCat { get; set; }

    }
}