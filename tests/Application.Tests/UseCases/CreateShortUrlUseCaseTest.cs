using Application.Dtos;
using Application.UseCases;
using Domain.Entities;
using Domain.Ports;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.UseCases
{
    public class CreateShortUrlUseCaseTest
    {
        public CreateShortUrlUseCaseTest() { }

        [Fact]
        public async Task Execute_Should_ThrowException_When_DteDocumentNotFound()
        {
            // Arrange
            var mockDteRepo = new Mock<IDteDocumentRepository>();
            mockDteRepo.Setup(x => x.ExistAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var useCase = new CreateShortUrlUseCase(
                new Mock<IShortUrlRepository>().Object,
                mockDteRepo.Object
                // test with a mock Code generator later, like: new Mock<ICodeGenerator>().Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                useCase.ExecuteAsync(new CreateShortUrlRequest { DteId = "invalid" }));
        }

        [Fact]
        public async Task Execute_Should_ReturnShortUrl_When_DteDocumentExists()
        {
            // Arrange
            var mockDteRepo = new Mock<IDteDocumentRepository>();
            mockDteRepo.Setup(x => x.ExistAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var mockShortUrlRepo = new Mock<IShortUrlRepository>();
            mockShortUrlRepo.Setup(x => x.CreateAsync(It.IsAny<ShortUrl>()))
                .Returns((ShortUrl shortUrl) => Task.FromResult(shortUrl));


            var useCase = new CreateShortUrlUseCase(
                mockShortUrlRepo.Object,
                mockDteRepo.Object
                //mockCodeGenerator.Object
            );

            // Act
            var result = await useCase.ExecuteAsync(new CreateShortUrlRequest { DteId = "1" });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result.EncodedUrl);
            mockShortUrlRepo.Verify(x => x.CreateAsync(It.IsAny<ShortUrl>()), Times.Once);
        }

        [Fact]
        public void ShortUrl_Should_BeInvalid_When_MaxUsesReached()
        {
            // Arrange
            var shortUrl = new ShortUrl("dte/1", "abc123", "1", maxUses: 3);
            shortUrl.IncrementAccessCount();
            shortUrl.IncrementAccessCount();
            shortUrl.IncrementAccessCount();

            // Act
            var isValid = shortUrl.IsValid();

            // Assert
            Assert.False(isValid);
            Assert.False(shortUrl.IsActive);
        }

        [Fact]
        public void ShortUrl_Should_BeInvalid_When_Expired()
        {
            // Arrange
            var shortUrl = new ShortUrl("dte/1", "abc123", "1", expiresAt: DateTime.UtcNow.AddMinutes(-1));

            // Act
            var isValid = shortUrl.IsValid();

            // Assert
            Assert.False(isValid);
            Assert.False(shortUrl.IsActive);
        }

        [Fact]
        public void IncrementAccessCount_Should_Deactivate_When_MaxUsesReached()
        {
            // Arrange
            var shortUrl = new ShortUrl("dte/1", "abc123", "1", maxUses: 2);
            shortUrl.IncrementAccessCount();

            // Act
            shortUrl.IncrementAccessCount();

            // Assert
            Assert.Equal(2, shortUrl.AccessCount);
            Assert.False(shortUrl.IsActive);
        }

        [Fact]
        public void IsValid_Should_ReturnTrue_When_ActiveAndNotExpired()
        {
            // Arrange
            var shortUrl = new ShortUrl("dte/1", "abc123", "1", expiresAt: DateTime.UtcNow.AddMinutes(10));

            // Act
            var isValid = shortUrl.IsValid();

            // Assert
            Assert.True(isValid);
            Assert.True(shortUrl.IsActive);
        }

        [Fact]
        public void ShortUrl_Constructor_Should_ThrowException_When_OriginalUrlOrDteIdIsNullOrEmpty()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new ShortUrl(string.Empty, "abc123", "1"));
            Assert.Throws<ArgumentException>(() => new ShortUrl("dte/1", "abc123", string.Empty));
            Assert.Throws<ArgumentException>(() => new ShortUrl("", "abc123", "1"));
            Assert.Throws<ArgumentException>(() => new ShortUrl("dte/1", "abc123", ""));
        }
    }
}
