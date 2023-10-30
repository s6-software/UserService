using System.ComponentModel.DataAnnotations;

namespace API.Models.User
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
