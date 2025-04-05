using Domain.Entities;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        public Task<ShortUrl> CreateAsync(ShortUrl shortUrl)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetOriginalUrlAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}
