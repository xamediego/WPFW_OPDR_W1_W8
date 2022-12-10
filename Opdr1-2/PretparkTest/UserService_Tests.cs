using System;
using System.Collections.Generic;
using ConsoleApplication1.Authentication;
using CTest.Authentication;
using Xunit;

namespace CTest
{
    public class Tests
    {
        [Fact]
        [InlineData()]
        public void Register_ShouldReturnUser()
        {
            // Arrange
            var userService = new UserService(new UserContextMock(), new EmailServiceMock());

            // Act
            User user = userService.Register("test@live.nl", "test", "test");
            
            // Assert
            Assert.NotNull(user);
        }

        [Fact]
        public void FindAll_ShouldReturnList()
        {
            // Arrange
            var userService = new UserService(new UserContextMock(), new EmailServiceMock());

            // Act
            List<User> userList = userService.FindAll();

            // Assert
            Assert.NotNull(userList);
        }

        [Fact]
        public void ExistsByEmail_ShouldReturnTrue()
        {
            // Arrange
            var userService = new UserService(new UserContextMock(), new EmailServiceMock());

            // Act
            User user = userService.Register("test@live.nl", "test", "test");

            // Assert
            Assert.True(userService.ExistsByEmail("test@live.nl"));
        }

        [Fact]
        public void Login_ShouldReturnTrue()
        {
            // Arrange
            var userService = new UserService(new UserContextMock(), new EmailServiceMock());

            // Act
            User user = userService.Register("test@live.nl", "test", "test");
            userService.Verify(user.Email, user.Token.Token);

            // Assert
            Assert.True(userService.Login("test@live.nl", "test"));
        }
        
        [Fact]
        public void UnverifiedLogin_ShouldReturnFalse()
        {
            // Arrange
            var userService = new UserService(new UserContextMock(), new EmailServiceMock());

            // Act
            User user = userService.Register("test@live.nl", "test", "test");

            // Assert
            Assert.False(userService.Login(user.Email, user.Password));
        }

        [Fact]
        public void Verify_ShouldReturnTrue()
        {
            // Arrange
            var userService = new UserService(new UserContext(), new EmailServiceMock());

            // Act
            User user = userService.Register("test@live.nl", "test", "test");

            // Assert
            Assert.True(userService.Verify(user.Email, user.Token.Token));
        }
        
        [Fact]
        public void DateVer_ShouldReturnFalse()
        {
            // Arrange
            var userService = new UserService(new UserContMockSecond(), new EmailServiceMock());

            // Act
            User user = userService.Register("test@live.nl", "test", "test");
            

            // Assert
            Assert.False(userService.Verify(user.Email, user.Token.Token));
        }
        
    }
}