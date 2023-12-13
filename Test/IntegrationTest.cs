using API.Models.User;
using API.Services;
using Microsoft.EntityFrameworkCore;


namespace Test
{
    public class User_Username_testing : BaseTest
    {
        [Fact]
        public void bad_username_empty()
        {
            SetupRegisterDTO();

            _registerDTO.Username = "";

            bool result = _userService.ValidateRegistration(_registerDTO);

            Assert.False(result);

        }

        [Fact]
        public void bad_username_little()
        {
            SetupRegisterDTO();

            _registerDTO.Username = "a";

            bool result = _userService.ValidateRegistration(_registerDTO);

            Assert.False(result);

        }

        [Fact]
        public void bad_username_numerical()
        {
            SetupRegisterDTO();

            _registerDTO.Username = "J0hn D03";

            bool result = _userService.ValidateRegistration(_registerDTO);

            Assert.False(result);

        }

        [Fact]
        public void good_username()
        {
            SetupRegisterDTO();

            _registerDTO.Username = "JohnDoe";

            bool result = _userService.ValidateRegistration(_registerDTO);

            Assert.True(result);

        }
    }

    public class User_Email_testing: BaseTest
    {
        [Fact]
        public void bad_email_empty()
        {
            SetupRegisterDTO();
            _registerDTO.Email = "";
            bool result = _userService.ValidateRegistration(_registerDTO);
            Assert.False(result);

        }

        [Fact]
        public void bad_email_using_at()
        {
            SetupRegisterDTO();
            _registerDTO.Email = "@";
            bool result = _userService.ValidateRegistration(_registerDTO);
            Assert.False(result);

        }

        [Fact]
        public void bad_email_using_dot()
        {
            SetupRegisterDTO();
            _registerDTO.Email = "a.com";
            bool result = _userService.ValidateRegistration(_registerDTO);
            Assert.False(result);

        }

        [Fact]
        public void good_email()
        {
            SetupRegisterDTO();
            _registerDTO.Email = "hello@world.com";
            bool result = _userService.ValidateRegistration(_registerDTO);
            Assert.True(result);

        }

    }
    
    public class User_Password_testing : BaseTest
    {

        [Fact]
        public void bad_password_no_special_capital_numerical_and_little()
        {
            SetupRegisterDTO();

            _registerDTO.Password = "hello";

            bool result = _userService.ValidateRegistration(_registerDTO);

            Assert.False(result);

        }

        [Fact]
        public void bad_password_no_special_capital_numerical()
        {
            SetupRegisterDTO();

            _registerDTO.Password = "helloworld";

            bool result = _userService.ValidateRegistration(_registerDTO);

            Assert.False(result);


        }


        [Fact]
        public void bad_password_no_special_capital()
        {
            SetupRegisterDTO();

            _registerDTO.Password = "helloworld1";

            bool result = _userService.ValidateRegistration(_registerDTO);

            Assert.False(result);

        }

        [Fact]
        public void bad_password_no_special()
        {
            SetupRegisterDTO();

            _registerDTO.Password = "Helloworld1";

            bool result = _userService.ValidateRegistration(_registerDTO);

            Assert.False(result);

        }

        [Fact]
        public void bad_password_empty()
        {
            SetupRegisterDTO();

            _registerDTO.Password = "";
            bool result = _userService.ValidateRegistration(_registerDTO);

            Assert.False(result);

        }


        [Fact]
        public void good_password()
        {
            SetupRegisterDTO();
            _registerDTO.Password = "Super1-Secret";
            bool result = _userService.ValidateRegistration(_registerDTO);
            Assert.True(result);

        }

    }
}