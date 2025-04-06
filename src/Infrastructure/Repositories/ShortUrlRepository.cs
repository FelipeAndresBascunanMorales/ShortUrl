using Domain.Entities;
using Domain.Ports;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly ShortUrlDbContext _dbContext;
        public ShortUrlRepository(ShortUrlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //get all for testing only

        public async Task<IEnumerable<ShortUrl>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.ShortUrls.ToList());
        }

        public async Task<ShortUrl> AddAsync(ShortUrl shortUrl)
        {
            _dbContext.ShortUrls.Add(shortUrl);
            await _dbContext.SaveChangesAsync();

            return shortUrl;
        }

        public async Task<ShortUrl?> GetByCodeAsync(string code)
        {
            return await Task.FromResult(_dbContext.ShortUrls.FirstOrDefault(x => x.Code == code));
        }

        public Task<string> GetOriginalUrlAsync(string code)
        {
            throw new NotImplementedException();
        }

    }
}
