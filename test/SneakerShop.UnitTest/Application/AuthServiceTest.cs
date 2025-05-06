using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Moq;
using SneakerShop.Application.Common.Services;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;

namespace SneakerShop.UnitTest.Application
{
    public class AuthServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly AuthService _authService;

        public AuthServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _authService = new AuthService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task RegisterUser_ReturnsTrue_WhenRepositoryCreatesUser()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.CreateUser(It.IsAny<User>()))
                .ReturnsAsync(new User());

            // Act
            var result = await _authService.RegisterUser("test@example.com", "password", "Test");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RegisterUser_ReturnsFalse_WhenRepositoryReturnsNull()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.CreateUser(It.IsAny<User>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _authService.RegisterUser("test@example.com", "password", "Test");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task LoginUser_ReturnsUser_WhenCredentialsAreValid()
        {
            // Arrange
            var user = new User { Email = "test@example.com", Password = "password" };
            _userRepositoryMock.Setup(r => r.GetUserByEmail(user.Email))
                .ReturnsAsync(user);

            // Act
            var result = await _authService.LoginUser(user.Email, "password");

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task LoginUser_ReturnsNull_WhenCredentialsAreInvalid()
        {
            // Arrange
            var user = new User { Email = "test@example.com", Password = "password" };
            _userRepositoryMock.Setup(r => r.GetUserByEmail(user.Email))
                .ReturnsAsync(user);

            // Act
            var result = await _authService.LoginUser(user.Email, "wrongpassword");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task LogoutUser_AlwaysReturnsTrue()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.GetUserById(1))
                .ReturnsAsync(new User());

            // Act
            var result = await _authService.LogoutUser(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GenerateJwtToken_ReturnsValidJwtWithCorrectClaims()
        {
            // Arrange
            var user = new User { Email = "test@example.com", Role = Domain.Enums.Role.Customer };

            // Act
            var tokenString = _authService.GenerateJwtToken(user);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenString);

            // Assert
            Assert.Equal(user.Email, token.Subject);
            Assert.Contains(token.Claims, c => c.Type == ClaimTypes.Role && c.Value == user.Role.ToString());
            Assert.True(token.ValidTo > DateTime.UtcNow);
        }

        [Fact]
        public async Task CheckEmailExists_ReturnsTrue_WhenUserExists()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.GetUserByEmail("exists@example.com"))
                .ReturnsAsync(new User());

            // Act
            var result = await _authService.CheckEmailExists("exists@example.com");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CheckEmailExists_ReturnsFalse_WhenUserDoesNotExist()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.GetUserByEmail("notfound@example.com"))
                .ReturnsAsync((User)null);

            // Act
            var result = await _authService.CheckEmailExists("notfound@example.com");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsUserList()
        {
            // Arrange
            var users = new List<User> { new User(), new User() };
            _userRepositoryMock.Setup(r => r.GetAllUsers())
                .ReturnsAsync(users);

            // Act
            var result = await _authService.GetAllUsers();

            // Assert
            Assert.Equal(users, result);
        }
    }
}
