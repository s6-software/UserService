using API.Models.User;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        string RegisterUser(RegisterDTO registerDTO);
        string LoginUser(LoginDTO loginDTO);
        string DeleteAll();
    }

    public class UserService: IUserService
    {
        private readonly UserContext _userContext;

        public UserService(UserContext context)
        {
            _userContext = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userContext.users.ToList();
        }

        public string RegisterUser(RegisterDTO registerDTO)
        {
            var newUser = new User
            {
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                Password = registerDTO.Password,
            };
            _userContext.users.Add(newUser);
            _userContext.SaveChanges();
            return $"{newUser.Username} has been created";
        }

        public string LoginUser(LoginDTO loginDTO)
        {
            return "aksnfkjanskfjnakncjkascn";
        }

        public string DeleteAll()
        {
            _userContext.users.ExecuteDelete();
            return "wiped database.";
        }
    }
}
