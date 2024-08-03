using Microsoft.EntityFrameworkCore;
using NetApi.Database;
using NetApi.Entities;
using NetApi.Models;

namespace NetApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext dbContext;
        public UserRepository(ApplicationDBContext dbContext)
        {
           this.dbContext = dbContext;
        }

        public async Task<List<User>> GetAllAsync()
        {
           return await dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User> AddUserAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateUserAsync(Guid id, User user)
        {
            var existingUser = await GetByIdAsync(id);
            if (existingUser is null) return null;

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            existingUser.Salary = user.Salary;

            await dbContext.SaveChangesAsync();

            return existingUser;
        }

        public async Task<User?> DeleteUserAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            if (user is null) return null;

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();

            return user;
        }
    }
}
