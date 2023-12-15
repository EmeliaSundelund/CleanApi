namespace Application.Dtos
{
    public class BirdDto
    {
        public string Name { get; set; } = string.Empty;
        public int Owner { get; set; }
        public bool CanFly { get; set; }
        public required string Color { get; set; }
    }
}