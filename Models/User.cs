using System;
using System.ComponentModel.DataAnnotations;

namespace Auth.API.Models
{
    public class User
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int Role { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PassworSalt { get; set; }
    }
}