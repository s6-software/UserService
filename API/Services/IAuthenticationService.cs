using API.Models.User;
using FirebaseAdmin.Auth;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace API.Services
{
    public interface IAuthenticationService
    {
        Task<string> RegisterAsync(RegisterDTO userDTO);
    }

    public class AuthenticationService : IAuthenticationService
    {
        public async Task<string> RegisterAsync(RegisterDTO userDTO)
        {
            var userArgs = new UserRecordArgs
            {
                Email = userDTO.Email,
                Password = userDTO.Password,
            };

            var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);

            return userRecord.Uid;
        }
    }
}
