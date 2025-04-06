using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class RedirectShortUrlUseCase
    {

        private readonly IShortUrlRepository _shortUrlRepository;
        public RedirectShortUrlUseCase(IShortUrlRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }
        public async Task<(string Url, bool Expired)> ExecuteAsync(string code)
        {
            // Validate the code
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Code cannot be null or empty.", nameof(code));
            }

            // Retrieve the original URL from the repository
            var shortUrl = await _shortUrlRepository.GetByCodeAsync(code);

            if (shortUrl == null)
            {
                throw new KeyNotFoundException($"Short URL with code '{code}' not found.");
            }
            // Redirect to the original URL
            return (shortUrl.OriginalUrl, false);
        }
    }
}
