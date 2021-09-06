using System.Collections.Generic;
using System.Threading.Tasks;
using IncludeTypeBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace IncludeTypeBackend.Services
{
    public class UserService
    {
        private readonly PostgreSqlContext _db;

        public UserService(PostgreSqlContext db) => _db = db;

        public async Task<List<User>> GetAllUsersAsync() => await _db.Users.ToListAsync();

        public async Task AddUserAsync(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(string key) =>
            await _db.Users.FirstOrDefaultAsync(user => user.UserId == key);

        public async Task<User> GetUserAsync(string key) =>
            await _db.Users.FirstOrDefaultAsync(user => (user.Email == key || user.Username == key));

        public async Task UpdateUserAsync(User existingUser, User updatedUser)
        {
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;
            existingUser.Password = updatedUser.Password;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }
    }
}