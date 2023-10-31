using API.Models.User;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authentication;
        private readonly IJwtProvider _jwtProvider;

        public UserController(IUserService userContext, IAuthenticationService authentication, IJwtProvider jwtProvider)
        {
            _userService = userContext;
            _authentication = authentication;
            _jwtProvider = jwtProvider;
        }
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            var userDTOs = ConvertToDTO(users);

            return Ok(userDTOs);
        }

        [HttpPost("register")]
        public async Task<string> RegisterUser(RegisterDTO userDTO)
        {
            string Uid = await _authentication.RegisterAsync(userDTO);

            _userService.RegisterUser(userDTO, Uid);

            return $"successfully registered {userDTO.Email}";
        }

        [HttpPost("login")]
        public async Task<string> Login (LoginDTO loginDTO)
        {
            string token =  await _jwtProvider.Login(loginDTO);

            return  token;
        }
        [HttpDelete("delete_all")]
        public ActionResult DeleteUsers()
        {
            string response = _userService.DeleteAll();
            return Ok(response);
        }
        private IEnumerable<UserDTO> ConvertToDTO(IEnumerable<User> users)
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            foreach (var user in users)
            {
                userDTOs.Add(new UserDTO
                {
                    Email = user.Email
                });
            }
            return userDTOs;
        }
    }
}
