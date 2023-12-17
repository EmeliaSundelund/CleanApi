namespace Domain.Models.Animal
{
    public class AnimalModel
    {
        public Guid id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<AnimalUserModel> AnimalUsers { get; set; }
    }
}
