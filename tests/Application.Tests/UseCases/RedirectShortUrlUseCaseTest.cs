using Application.UseCases;
using Domain.Entities;
using Domain.Ports;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.UseCases
{
    public class RedirectShortUrlUseCaseTest
    {
        [Fact]
        public async Task ExecuteAsync_Should_ThrowException_When_CodeIsNullOrEmpty()
        {
            // Arrange
            var mockRepo = new Mock<IShortUrlRepository>();
            var useCase = new RedirectShortUrlUseCase(mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(string.Empty));
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(""));
        }

        [Fact]
        public async Task ExecuteAsync_Should_ThrowException_When_ShortUrlNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IShortUrlRepository>();
            var invalidCode = "invalidCode";
            mockRepo.Setup(x => x.GetByCodeAsync(invalidCode)).ReturnsAsync((ShortUrl?) null);

            var useCase = new RedirectShortUrlUseCase(mockRepo.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => useCase.ExecuteAsync(invalidCode));
            Assert.Equal($"Short URL with code '{invalidCode}' not found.", exception.Message);
        }


        [Fact]
        public async Task ExecuteAsync_Should_ReturnExpired_When_ShortUrlIsInvalid()
        {
            // Arrange
            var shortUrl = new ShortUrl("dte/1", "abc123", "1", expiresAt: DateTime.UtcNow.AddMinutes(-1));
            var mockRepo = new Mock<IShortUrlRepository>();
            mockRepo.Setup(x => x.GetByCodeAsync(It.IsAny<string>())).ReturnsAsync(shortUrl);

            var useCase = new RedirectShortUrlUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync("abc123");

            // Assert
            Assert.Equal("dte/1", result.Url);
            Assert.True(result.Expired);
            Assert.False(shortUrl.IsActive);
        }

        [Fact]
        public async Task ExecuteAsync_Should_ReturnOriginalUrl_When_ShortUrlIsValid()
        {
            // Arrange
            var shortUrl = new ShortUrl("dte/1", "abc123", "1", expiresAt: DateTime.UtcNow.AddMinutes(10));
            var mockRepo = new Mock<IShortUrlRepository>();
            mockRepo.Setup(x => x.GetByCodeAsync(It.IsAny<string>())).ReturnsAsync(shortUrl);

            var useCase = new RedirectShortUrlUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync("abc123");

            // Assert
            Assert.Equal("dte/1", result.Url);
            Assert.False(result.Expired);
            Assert.Equal(1, shortUrl.AccessCount);
            mockRepo.Verify(x => x.UpdateAsync(It.IsAny<ShortUrl>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_Should_Deactivate_When_MaxUsesReached()
        {
            // Arrange
            var shortUrl = new ShortUrl("dte/1", "abc123", "1", maxUses: 2);
            shortUrl.IncrementAccessCount();

            var mockRepo = new Mock<IShortUrlRepository>();
            mockRepo.Setup(x => x.GetByCodeAsync(It.IsAny<string>())).ReturnsAsync(shortUrl);

            var useCase = new RedirectShortUrlUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync("abc123");

            // Assert
            Assert.Equal("dte/1", result.Url);
            Assert.False(result.Expired);
            Assert.False(shortUrl.IsActive);
            Assert.Equal(2, shortUrl.AccessCount);
            mockRepo.Verify(x => x.UpdateAsync(It.IsAny<ShortUrl>()), Times.Once);
        }
    }
}
