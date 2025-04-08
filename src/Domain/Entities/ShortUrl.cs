using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{

    /// <summary>
    /// Represents a shortened URL entity with properties and methods to manage its lifecycle.
    /// </summary>
    public class ShortUrl
    {
        /// <summary>
        /// Gets or sets the unique identifier for the shortened URL.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the original URL that is being shortened.
        /// </summary>
        public string OriginalUrl { get; set; }

        /// <summary>
        /// Gets or sets the encoded version of the shortened URL.
        /// </summary>
        public string EncodedUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets the unique identifier for the associated DTE (Digital Transaction Entity).
        /// </summary>
        public string DteId { get; private set; }

        /// <summary>
        /// Gets or sets the date and time when the shortened URL was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets the expiration date and time for the shortened URL, if applicable.
        /// </summary>
        public DateTime? ExpiresAt { get; private set; }

        /// <summary>
        /// Gets the number of times the shortened URL has been accessed.
        /// </summary>
        public int AccessCount { get; private set; }

        /// <summary>
        /// Gets the maximum number of times the shortened URL can be accessed, if applicable.
        /// </summary>
        public int? MaxUses { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the shortened URL is currently active.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortUrl"/> class.
        /// </summary>
        /// <param name="originalUrl">The original URL to be shortened.</param>
        /// <param name="encodedUrl">The encoded version of the shortened URL.</param>
        /// <param name="dteId">The unique identifier for the associated DTE.</param>
        /// <param name="expiresAt">The optional expiration date and time for the shortened URL.</param>
        /// <param name="maxUses">The optional maximum number of times the shortened URL can be accessed.</param>
        /// <exception cref="ArgumentException">Thrown when the original URL or DTE ID is null or empty.</exception>
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
            DteId = dteId;
            CreatedAt = DateTime.UtcNow;
            ExpiresAt = expiresAt;
            MaxUses = maxUses;
            AccessCount = 0;
            IsActive = true;
        }

        /// <summary>
        /// Determines whether the shortened URL is valid based on its active status, expiration, and usage limits.
        /// </summary>
        /// <returns>True if the shortened URL is valid; otherwise, false.</returns>
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

        /// <summary>
        /// Increments the access count for the shortened URL and deactivates it if the maximum usage limit is reached.
        /// </summary>
        public void IncrementAccessCount()
        {
            AccessCount++;

            if (MaxUses.HasValue && AccessCount >= MaxUses.Value)
                IsActive = false;
        }

        /// <summary>
        /// Deactivates the shortened URL, making it inactive.
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
