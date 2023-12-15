using System;
namespace Application.Dtos
{
    public class CatDto
    {
        public string Name { get; set; } = string.Empty;
        public int Owner { get; set; }
        public bool LikesToPlay { get; set; }
        public required string BreedCat { get; set; }
        public int WeightCat { get; set; }
    }
}