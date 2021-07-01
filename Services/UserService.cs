using System.Collections.Generic;
using System.Linq;
using IncludeTypeBackend.Models;

namespace IncludeTypeBackend.Services
{
    public class UserService
    {
        private readonly PostgreSqlContext _db;

        public UserService(PostgreSqlContext db) => _db = db;

        public List<User> GetAllUsers() => _db.Users.ToList();

        public void AddUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public User GetUserById(string key) =>
            _db.Users.FirstOrDefault(user => user.UserId == key);

        public User GetUser(string key) =>
            _db.Users.FirstOrDefault(user => (user.Email == key || user.Username == key));

        public void UpdateUser(User existingUser, User updatedUser)
        {
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;
            existingUser.Password = updatedUser.Password;
            _db.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _db.Users.Remove(user);
            _db.SaveChanges();
        }
    }
}