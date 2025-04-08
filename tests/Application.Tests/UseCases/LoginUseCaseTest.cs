using Application.Dtos;
using Application.UseCases;
using Domain.Entities;
using Domain.Ports;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.UseCases
{
    public class LoginUseCaseTest
    {
        [Fact]
        public async Task ExecuteAsync_Should_ThrowException_When_UsernameOrPasswordIsNullOrEmpty()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockJwtService = new Mock<IJwtService>();
            var useCase = new LoginUseCase(mockUserService.Object, mockJwtService.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(string.Empty, "password"));
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync("username", string.Empty));
        }

        [Fact]
        public async Task ExecuteAsync_Should_ReturnUnauthenticated_When_CredentialsAreInvalid()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync((User?) null);

            var mockJwtService = new Mock<IJwtService>();
            var useCase = new LoginUseCase(mockUserService.Object, mockJwtService.Object);

            // Act
            var result = await useCase.ExecuteAsync("invalidUser", "invalidPassword");

            // Assert
            Assert.False(result.Autenticated);
            Assert.Equal("", result.Token);
        }


        [Fact]
        public async Task ExecuteAsync_ShouldReturnAuthenticatedResponse_WhenCredentialsAreValid()
        {
            // Arrange
            var username = "testuser";
            var password = "password123";
            var user = new User { Username = username, Password = password };
            var token = "mockToken";

            var _userServiceMock = new Mock<IUserService>();
            var _jwtServiceMock = new Mock<IJwtService>();

            _userServiceMock.Setup(s => s.GetUserByUsernameAsync(username)).ReturnsAsync(user);
            _userServiceMock.Setup(s => s.VerifyPasswordAsync(user, password)).ReturnsAsync(true); // Mock password verification
            _jwtServiceMock.Setup(s => s.GenerateToken(user)).Returns(token);

            var useCase = new LoginUseCase(_userServiceMock.Object, _jwtServiceMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(username, password);

            // Assert
            Assert.True(result.Autenticated);
            Assert.Equal(token, result.Token);
        }
    }
}
