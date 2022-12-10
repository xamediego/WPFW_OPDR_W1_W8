using System;
using ConsoleApplication1.Authentication;
using Xunit;
using Moq;

namespace CTest
{
    public class MoqTest
    {
        
        [Fact]
        public void DateVer_ShouldReturnFalse()
        {

            // Arrange
            User user = new User
            {
                Password = "test",
                Username = "test",
                Email = "test@live.nl",
                Token = new VerificationToken { Token = Guid.NewGuid().ToString(), ExpDate = DateTime.Now.AddDays(-1) }
            };
            
            // Act
            var userService = new UserService(GetContextBase(user).Object , new EmailService());
            
            // Assert
            Assert.False(userService.Verify(user.Email, user.Token.Token));
        }
        
        private Mock<IUserContext> GetContextBase(User user)
        {
            var m = new Mock<IUserContext>();
            
            m.Setup((a) => a.NewUser(user.Password, user.Username, user.Email))
                .Returns(user);
            
            m.Setup((a) => a.FindByEmail(user.Email))
                .Returns(user);

            return m;
        }
        
    }
}