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

        public UserController(IUserService userContext)
        {
            _userService = userContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            var userDTOs = ConvertToDTO(users);

            return Ok(userDTOs);
        }

        [HttpPost]
        public string CreateUser(RegisterDTO userDTO)
        {
            return _userService.RegisterUser(userDTO);
        }

        [HttpPost("Login")]
        public ActionResult<UserDTO> Login (LoginDTO loginDTO)
        {
            string token = _userService.LoginUser(loginDTO);

            return Ok(token);
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
