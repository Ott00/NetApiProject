using NetApi.Entities;
using NetApi.Models;

namespace NetApi.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>>GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User> AddUserAsync(User user);
        Task<User?> UpdateUserAsync(Guid id, User user);
        Task<User?> DeleteUserAsync(Guid id);
    }
}
