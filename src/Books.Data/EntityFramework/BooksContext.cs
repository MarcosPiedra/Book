using Microsoft.EntityFrameworkCore;

namespace Books.Data.EntityFramework
{
    public class BooksContext : DbContext
    {
        public BooksContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BooksConfig());
        }
    }
}
