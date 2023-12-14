namespace Application.Dtos
{
    public class BirdDto
    {
        public string Name { get; set; } = string.Empty;
        public int Owner { get; set; }
        public bool CanFly { get; set; }
        public string Color { get; set; }
    }
}