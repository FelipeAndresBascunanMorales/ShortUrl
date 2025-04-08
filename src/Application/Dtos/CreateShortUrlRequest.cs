using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    /// <summary>
    /// Represents a request to create a short URL.
    /// </summary>
    public class CreateShortUrlRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier for the DTE (Digital Transaction Entity).
        /// This field is required.
        /// </summary>
        public required string DteId { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of times the short URL can be used.
        /// If null, there is no limit on the number of uses.
        /// </summary>
        public int? MaxUses { get; set; }

        /// <summary>
        /// Gets or sets the duration in minutes for which the short URL will be valid.
        /// If null, the URL will not have an expiration time.
        /// </summary>
        public int? DurationInMinutes { get; set; }
    }
}
