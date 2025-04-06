using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShortUrl
    {
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; }
        public string EncodedUrl { get; set; } = string.Empty;
        public string DteId { get; private set; }
        public DateTime CreatedAt { get; set; }

        // to control the accesibility of the short URL
        public DateTime? ExpiresAt { get; private set; }
        public int AccessCount { get; private set; }
        public int? MaxUses { get; private set; }
        public bool IsActive { get; private set; }


        public ShortUrl(string originalUrl, string encodedUrl, string dteId, DateTime? expiresAt = null, int? maxUses = null)
        {
            if (string.IsNullOrWhiteSpace(originalUrl))
            {
                throw new ArgumentException("Original URL cannot be null or empty.", nameof(originalUrl));
            }
            if (string.IsNullOrWhiteSpace(dteId))
            {
                throw new ArgumentException("Encoded URL cannot be null or empty.", nameof(dteId));
            }

            Id = Guid.NewGuid();
            OriginalUrl = originalUrl;
            EncodedUrl = encodedUrl;
            DteId=dteId;
            CreatedAt = DateTime.UtcNow;
            ExpiresAt = expiresAt;
            MaxUses = maxUses;
            AccessCount = 0;
            IsActive = true;
        }


        // Domain logic methods
        public bool IsValid()
        {
            if (!IsActive)
                return false;

            if (ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt.Value)
                return false;

            if (MaxUses.HasValue && AccessCount >= MaxUses.Value)
                return false;

            return true;
        }

        public void IncrementAccessCount()
        {
            AccessCount++;

            if (MaxUses.HasValue && AccessCount >= MaxUses.Value)
                IsActive = false;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
