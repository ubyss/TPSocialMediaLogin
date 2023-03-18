using System.ComponentModel.DataAnnotations;

namespace TPSocialMedia.Models
{
    public class UserProfile
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
    }
}
