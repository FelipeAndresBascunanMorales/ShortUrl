using Domain.Entities;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{

    public class CreateShortUrlUseCase
    {
        private readonly IShortUrlRepository _shortUrlRepository;

        public CreateShortUrlUseCase(IShortUrlRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public async Task<string> ExecuteAsync(string originalUrl)
        {
            if (string.IsNullOrWhiteSpace(originalUrl))
            {
                throw new ArgumentException("Original URL cannot be null or empty.", nameof(originalUrl));
            }

            string code = Guid.NewGuid().ToString().Substring(0, 8);
            var shortUrl = new ShortUrl(originalUrl, DateTime.UtcNow, code);
            await _shortUrlRepository.CreateAsync(shortUrl);

            // todo: return the saved code or the entire object
            return code;
        }
    }
}
