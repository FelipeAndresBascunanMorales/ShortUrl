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
        private readonly IDteDocumentRepository _dteDocumentRepository;

        public CreateShortUrlUseCase(IShortUrlRepository shortUrlRepository, IDteDocumentRepository dteDocumentRepository)
        {
            _shortUrlRepository = shortUrlRepository;
            _dteDocumentRepository = dteDocumentRepository;
        }

        public async Task<ShortUrl> ExecuteAsync(CreateShortUrlRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DteId))
            {
                throw new ArgumentException("DTE ID cannot be null or empty.", nameof(request.DteId));
            }

            if (!await _dteDocumentRepository.ExistAsync(request.DteId))
            {
                throw new ArgumentException($"DTE ID '{request.DteId}' does not exist.");
            }


            // generate code with a service
            string code = Guid.NewGuid().ToString().Substring(0, 8);

            var shortUrl = new ShortUrl(
                originalUrl: $"/dte/{request.DteId}",
                encodedUrl: code,
                dteId: request.DteId,
                expiresAt: request.DurationInMinutes.HasValue ? DateTime.UtcNow.AddMinutes(request.DurationInMinutes.Value) : null,
                maxUses: request.MaxUses.HasValue ? request.MaxUses.Value : null
                );
            
            var savedShortUrl = await _shortUrlRepository.CreateAsync(shortUrl);

            return savedShortUrl;
        }
    }
}
