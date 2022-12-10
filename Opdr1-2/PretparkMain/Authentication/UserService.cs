using System;
using System.Collections.Generic;

namespace ConsoleApplication1.Authentication
{
    public class UserService
    {
        private readonly IUserContext _userContext;
        private readonly IEmailService _emailService;

        public UserService(IUserContext userContext, IEmailService emailService)
        {
            _userContext = userContext;
            _emailService = emailService;
        }

        public User Register(string email, string username, string password)
        {
            User newUser = _userContext.NewUser(password, username , email);
            
            _emailService.Email(newUser.Token.Token, email);
            
            return newUser;
        }

        public List<User> FindAll()
        {
            return _userContext.FindAll();
        }

        public Boolean ExistsByEmail(string email)
        {
            return _userContext.ExistsByEmail(email);
        }

        public Boolean Login(string email, string password)
        {
            User req = _userContext.FindByEmail(email);

            return req.IsVerified() && req.Password == password;
        }

        public Boolean Verify(string email, string token)
        {
            User user = _userContext.FindByEmail(email);

            if (user.Token == null || user.Token.Token != token) return false;
            
            if (user.Token.ExpDate < DateTime.Now)
            {
                Console.WriteLine("Token has expired");
            }
            else
            {
                user.Token = null;
                Console.WriteLine(user.Token = null);
                return true;
            }

            return false;
        }
    }
}