using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.Models.DTOs.Request
{
    public class UserRegistrationRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
