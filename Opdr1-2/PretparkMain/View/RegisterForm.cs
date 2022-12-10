using System;
using System.ComponentModel.DataAnnotations;
using ConsoleApplication1.Authentication;

namespace ConsoleApplication1.View
{
    public static class RegisterForm
    {
        private static string _email;
        private static string _password;
        private static string _username;
        
        private static UserService _userService = new UserService(new UserContext(), new EmailService());

        private static Boolean _regProcess;

        public static void MainForm()
        {
            _regProcess = true;
            
            while (_regProcess)
            {
                Console.WriteLine("1. Email\n" +
                                  "2. Password\n" +
                                  "3. Username\n" +
                                  "4. Register\n" +
                                  "5. Exit");
                string input = Console.ReadLine();

                if (input == "5") break;
                Options(input);
            }
        }

        private static void Options(string input)
        {
            if (input == "1") ChangeEmail();
            if (input == "2") ChangePassword();
            if (input == "3") ChangeName();
            if (input == "4") RegisterEvent();
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
            }
            else if (_userService.ExistsByEmail(input))
            {
                Console.WriteLine("Email address already in use");
            }
            else
            {
                _email = input;
            }
        }

        private static void ChangePassword()
        {
            Console.Write("Enter Password: ");
            string input = Console.ReadLine();
            if (input == null || input == "")
            {
                Console.WriteLine("Please enter a valid password");
            }
            else
            {
                _password = input;
            }
        }

        private static void ChangeName()
        {
            if (_username != null || _username != "") Console.WriteLine("Current Username: " + _username);
            Console.Write("Enter Username: ");
            _username = Console.ReadLine();
        }

        private static void RegisterEvent()
        {
            if (_email == null || _password == null || _username == null)
            {
                Console.WriteLine("Not all fields filled out");
            }
            else
            {
                _userService.Register(_email, _username, _password);
                Console.WriteLine(
                    "Register successful\nActivation link has been send to the corresponding email address");
                _regProcess = false;
            }
        }
    }
}