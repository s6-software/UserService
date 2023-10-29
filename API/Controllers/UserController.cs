using API.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserContext _context;

        public UserController(UserContext userContext)
        {
            _context = userContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = _context.users.ToList();
            var userDTOs = ConvertToDTO(users);

            return Ok(userDTOs);
        }

        [HttpPost]
        public ActionResult<UserDTO> CreateUser(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Username = userDTO.Username,
                    Email = userDTO.Email,
                    Password = userDTO.Password,
                };

                _context.users.Add(user);
                _context.SaveChanges();
                return userDTO;

            }

            return BadRequest(ModelState);
        }
        
        private IEnumerable<UserDTO> ConvertToDTO(IEnumerable<User> users)
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            foreach (var user in users)
            {
                userDTOs.Add(new UserDTO
                {
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email
                });
            }
            return userDTOs;
        }
    }
}
