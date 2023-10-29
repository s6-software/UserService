namespace API.Models.User
{
    public class UserDTO
    {
        public string Username { get; set; } = string.Empty;
        public required string Password { get; set; }
        public required string Email { get; set; }
    }

    public class LoginDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
