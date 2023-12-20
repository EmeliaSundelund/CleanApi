using Domain.Models.Person;

namespace Infrastructure.DataDbContex.Interfaces
{
    public interface IUserInterface
    {
        Task<UserModel> GetByIdAsync(Guid userId);
        Task AddAsync<T>(T entity) where T : class;
        Task UpdateAsync(UserModel user);
        Task DeleteAsync(Guid userId);
        Task<List<UserModel>> GetAllUsersAsync();
    }
}