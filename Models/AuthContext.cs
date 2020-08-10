using Microsoft.EntityFrameworkCore;

namespace Auth.API.Models
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> opt) : base(opt)
        {

        }

        public DbSet<User> User { get; set; }
    }
}