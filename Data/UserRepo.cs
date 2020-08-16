using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Data
{
    public class UserRepo : IUser
    {
        private readonly AuthContext _context;

        public UserRepo(AuthContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentException(nameof(user));
            }

            user.ID = Guid.NewGuid();
            _context.User.Add(user);
        }

        public void DeleteUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentException(nameof(user));
            }

            _context.User.Remove(user);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetUserByID(Guid id)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.ID == id);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateUser(User user)
        {
            // Does nothing
        }
    }
}