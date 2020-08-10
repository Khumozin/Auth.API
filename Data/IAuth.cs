using System.Threading.Tasks;
using Auth.API.Dtos;
using Auth.API.Models;

namespace Auth.API.Data
{
    public interface IAuth
    {
        Task<User> Register(UserCreateDto user, string password);
        Task<User> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}