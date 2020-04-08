using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.Data.EntityFramework
{
    public class BooksConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(r => r.Id).HasColumnName("bo_id");
            builder.Property(r => r.Title).HasColumnName("bo_title");
            builder.Property(r => r.Description).HasColumnName("bo_description");
            builder.Property(r => r.Author).HasColumnName("bo_author");
            builder.Property(r => r.PublicationDate).HasColumnName("bo_publication_date");
            builder.Property(r => r.IsRead).HasColumnName("bo_is_read");

            builder.HasKey(r => r.Id);

            builder.ToTable("book");
        }
    }
}
