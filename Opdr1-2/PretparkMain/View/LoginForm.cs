using System;
using System.ComponentModel.DataAnnotations;
using ConsoleApplication1.Authentication;

namespace ConsoleApplication1.View
{
    public static class LoginForm
    {
        private static string _email;
        private static string _password;
        
        private static UserService _userService = new UserService(new UserContext(), new EmailService());

        public static void MainForm()
        {
            while (true)
            {
                Console.WriteLine("1. Email\n" +
                                  "2. Password\n" +
                                  "3. Confirm Login\n" +
                                  "4. Exit");
                string input = Console.ReadLine();

                if (input == "4") break;
                Options(input);
            }
        }

        private static void Options(string input)
        {
            if (input == "1") ChangeEmail();
            if (input == "2") ChangePassword();
            if (input == "3") LoginEvent();
        }

        private static void ChangeEmail()
        {
            var email = new EmailAddressAttribute();

            if (_email != null || _email != "") Console.WriteLine("Current Email address: " + _email);
            
            Console.Write("Enter/Change Email: ");
            string input = Console.ReadLine();
            
            if (!email.IsValid(input))
            {
                Console.WriteLine("Invalid address");
            }else {
                _email = input;
            }
        }

        private static void ChangePassword()
        {
            Console.Write("Enter Password: ");
            _password = Console.ReadLine();
        }

        private static void LoginEvent()
        {
            if (_email == null || _password == null)
            {
                Console.WriteLine("Not all fields filled out");
            }
            else if (_userService.Login(_email, _password))
            {
                Console.WriteLine("Login successful");
            }
        }
    }
}