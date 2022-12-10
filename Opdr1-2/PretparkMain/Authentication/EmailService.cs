using System;

namespace ConsoleApplication1.Authentication
{
    public class EmailService : IEmailService
    {
        public bool Email(string text, string address)
        {
            Console.WriteLine("Email send to: " + address);
            Console.WriteLine("Verification Token: " + text);

            return true;
        }
        
    }
}