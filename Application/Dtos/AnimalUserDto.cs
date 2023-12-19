using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Application.Dtos
{
    public class AnimalUserDto
    {
        public Guid UserId { get; set; }
        public Guid AnimalId { get; set; }
    }
}
