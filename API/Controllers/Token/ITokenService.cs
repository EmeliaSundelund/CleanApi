using Domain.Models.Person;

namespace API.Controllers.Token
{
    public interface ITokenService
    {
        string GenerateToken(UserModel user);
    }
}
