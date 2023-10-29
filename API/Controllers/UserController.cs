using API.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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

        [HttpPost("Login")]
        public ActionResult<UserDTO> Login (LoginDTO loginDTO)
        {
            var user = _context.users.FirstOrDefault(u => u.Email == loginDTO.Email);

            if (user == null || user.Password != loginDTO.Password) 
            {
                return Unauthorized();
            }


            

            return Ok(user);
        }
        [HttpDelete("delete_all")]
        public ActionResult DeleteUsers()
        {
            var users = _context.users.ToList();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found to delete.");
            }
            _context.users.RemoveRange(users);
            _context.SaveChanges();
            return Ok("All users have been deleted.");
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
