﻿using System.ComponentModel.DataAnnotations;

namespace TPSocialMedia.Models
{
    public class UserDetails
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public string? Phone { get; set; }
    }
}
