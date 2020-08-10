using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.API.Models;

namespace Auth.API.Data
{
    public interface IUser
    {
        Task<bool> SaveChanges();
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByID(Guid id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}