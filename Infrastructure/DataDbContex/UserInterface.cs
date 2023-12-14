using Domain.Models;
using Domain.Models.Person;

namespace Infrastructure.DataDbContex
{
	public interface UserInterface
	{
        Task<UserModel> GetByIdAsync(Guid userId);
        Task AddAsync<T>(T entity) where T : class;
        Task UpdateAsync(UserModel user);
        Task DeleteAsync(Guid userId);
        Task<List<UserS>> GetAllUsersAsync();
    }
}