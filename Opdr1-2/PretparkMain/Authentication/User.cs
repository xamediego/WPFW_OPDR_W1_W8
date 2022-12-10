using System;

namespace ConsoleApplication1.Authentication
{
    public class User
    {

        public VerificationToken Token;
        
        public string Password { get; set;}
        public string Username { get; set;}
        public string Email { get; set;}

        public User()
        {
           
        }

        public Boolean IsVerified()
        {
            if (Token == null)
            {
                return true;
            }
            else
            {
                Console.WriteLine("User not verified");
                return false;
            }
        }
        
    }
}