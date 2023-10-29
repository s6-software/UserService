namespace API.Models.User
{
    public class UserDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
    }
}
