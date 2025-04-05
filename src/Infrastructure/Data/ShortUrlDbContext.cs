using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ShortUrlDbContext : DbContext
    {
        public ShortUrlDbContext(DbContextOptions<ShortUrlDbContext> options) : base(options)
        {
        }
        public DbSet<ShortUrl> ShortUrls { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortUrl>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<ShortUrl>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<ShortUrl>()
                .Property(s => s.OriginalUrl)
                .IsRequired();
        }
    }
}
