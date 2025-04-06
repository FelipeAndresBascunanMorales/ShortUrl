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
        public string Code { get; set; } = string.Empty;
        public string DteId { get; private set; }
        public DateTime CreatedAt { get; set; }


        public ShortUrl(string originalUrl, string code, string dteId, DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(originalUrl))
            {
                throw new ArgumentException("Original URL cannot be null or empty.", nameof(originalUrl));
            }

            Id = Guid.NewGuid();
            OriginalUrl = originalUrl;
            Code = code;
            DteId=dteId;
            CreatedAt = createdAt;
        }
    }
}
