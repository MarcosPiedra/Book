using Books.Data.EntityFramework.Config;
using Books.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Books.Data.EntityFramework.Context
{
    public class BooksContext : DbContext
    {
        AppSettings _appSettings;

        public BooksContext(IOptions<AppSettings> config) : base()
        {
            _appSettings = config.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_appSettings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BooksConfig());
        }
    }
}
