using ConsoleApplication1.Authentication;

namespace CTest.Authentication
{
    public class EmailServiceMock : IEmailService
    {
        public bool Email(string text, string address)
        {
            return true;
        }
    }
}