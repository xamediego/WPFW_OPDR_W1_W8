using System;
using System.Linq;
using ConsoleApplication1.Authentication;

namespace ConsoleApplication1.View
{
    public static class VerifyView
    {
        
        private static UserService _userService = new UserService(new UserContext(), new EmailService());

        public static void MainView()
        {
            while (true)
            {
                Console.WriteLine("1. All users\n2. Verify User\n3. Exit");
                string input = Console.ReadLine();
                if (input == "3") break;
                if (input == "1")
                    foreach (var user in _userService.FindAll().Where(user => user.Token != null && user.Token.ExpDate > DateTime.Now.AddDays(-3)))
                    {
                        Console.WriteLine("Email: " + user.Email + "\nToken: " + user.Token.Token);
                    }
                
                if (input == "2") VerifyEvent();
            }
        }

        private static void VerifyEvent()
        {
            Console.WriteLine("Enter email: ");
            string email = Console.ReadLine();
            if (_userService.ExistsByEmail(email))
            {
                Console.WriteLine("Enter token: ");
                string token = Console.ReadLine();
                
                if (_userService.Verify(email, token))
                {
                    Console.WriteLine("Account verified");
                }
                else
                {
                    Console.WriteLine("Invalid Token");
                }
            }
            else
            {
                Console.WriteLine("No such user found");
            }
        }
    }
}