using System;
using System.ComponentModel.DataAnnotations;

namespace Auth.API.Dtos
{
    public class UserReadDto
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int Role { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
    }
}