using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Auth.API.Dtos;
using Auth.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Data
{
    public class AuthRepo : IAuth
    {
        private readonly AuthContext _context;

        public AuthRepo(AuthContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string email, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email.ToLower());

            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PassworSalt))
            {
                return null;
            }

            return user;

        }

        public async Task<User> Register(UserCreateDto userCreateDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            createPasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                ID = Guid.NewGuid(),
                Name = userCreateDto.Name,
                Surname = userCreateDto.Surname,
                Email = userCreateDto.Email.ToLower(),
                Role = userCreateDto.Role,
                PasswordHash = passwordHash,
                PassworSalt = passwordSalt,
                DateCreated = DateTime.Now.ToString(),
                DateUpdated = userCreateDto.DateUpdated
            };

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.User.AnyAsync(u => u.Email == email.ToLower()))
            {
                return true;
            }
            return false;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }

        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public Password UpdateUserPassword(string password)
        {
            byte[] passwordHash, passwordSalt;
            createPasswordHash(password, out passwordHash, out passwordSalt);

            return new Password { PasswordHash = passwordHash, PasswordSalt = passwordSalt };
        }
    }
}