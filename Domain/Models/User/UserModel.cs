using System;
namespace Domain.Models.User
{
	public class UserModel
    {
        public Guid id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } 

    }
}


