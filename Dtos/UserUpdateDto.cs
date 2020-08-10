using System;
using System.ComponentModel.DataAnnotations;

namespace Auth.API.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int Role { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PassworSalt { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
    }
}