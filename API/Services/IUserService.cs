using API.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        string ValidateRegistration(RegisterDTO registerDTO);
        bool RegisterUser(RegisterDTO registerDTO, string uid);
        string DeleteAll();
    }

    public class UserService : IUserService
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

        public bool RegisterUser(RegisterDTO registerDTO, string uid)
        {
            var newUser = new User
            {
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                Uid = uid,
            };
            _userContext.users.Add(newUser);
            _userContext.SaveChanges();
            return true;
        }

        public string DeleteAll()
        {
            _userContext.users.ExecuteDelete();
            return "wiped database.";
        }

        public string ValidateRegistration(RegisterDTO registerDTO)
        {
            throw new NotImplementedException();
        }
    }
}
