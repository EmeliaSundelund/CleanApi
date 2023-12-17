using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
	public class AnimalUserModel
	{
        [Key]
		public Guid AnimalUserId { get; set; }
		public Guid id { get; set; }
		public Guid Id { get; set; }
		public Animal.AnimalModel Animal { get; set; }
		public Person.UserModel User { get; set; }

	}
}

