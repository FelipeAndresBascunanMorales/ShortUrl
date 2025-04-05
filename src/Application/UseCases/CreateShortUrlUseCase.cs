using Application.Dtos;
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

        public async Task<string> ExecuteAsync(CreateShortUrlRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Url))
            {
                throw new ArgumentException("Original URL cannot be null or empty.", nameof(request.Url));
            }

            // generate code with a service
            string code = Guid.NewGuid().ToString().Substring(0, 8);

            var shortUrl = new ShortUrl(request.Url, code, request.DteId, DateTime.UtcNow);
            await _shortUrlRepository.AddAsync(shortUrl);

            // todo: return the saved code or the entire object
            return code;
        }
    }
}
