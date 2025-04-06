using Domain.Entities;
using Domain.Ports;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly ShortUrlDbContext _context;

        public ShortUrlRepository(ShortUrlDbContext context)
        {
            _context = context;
        }

        public async Task<ShortUrl> CreateAsync(ShortUrl shortUrl)
        {
            _context.ShortUrls.Add(shortUrl);
            await _context.SaveChangesAsync();
            return shortUrl;
        }

        public async Task<ShortUrl> GetByCodeAsync(string code)
        {
            return await _context.ShortUrls.FirstOrDefaultAsync(s => s.EncodedUrl == code);
        }

        public async Task<string> GetOriginalUrlAsync(string code)
        {
            var shortUrl = await GetByCodeAsync(code);
            return shortUrl?.OriginalUrl;
        }

        public async Task<IEnumerable<ShortUrl>> GetAllAsync()
        {
            return await _context.ShortUrls.ToListAsync();
        }

        public async Task UpdateAsync(ShortUrl shortUrl)
        {
            _context.ShortUrls.Update(shortUrl);
            await _context.SaveChangesAsync();
        }
    }
}
