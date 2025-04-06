using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IShortUrlRepository
    {
        Task<string> GetOriginalUrlAsync(string code);
        Task<ShortUrl> CreateAsync(ShortUrl shortUrl);
        Task<ShortUrl> GetByCodeAsync(string code);
        Task UpdateAsync(ShortUrl shortUrl);


        //for testing purposes
        Task<IEnumerable<ShortUrl>> GetAllAsync();
    }
}
