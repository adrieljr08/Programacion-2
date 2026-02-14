using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppUsersAPI.DTOs
{
    public class UsersDTOs
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
