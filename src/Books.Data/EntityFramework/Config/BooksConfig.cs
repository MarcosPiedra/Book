using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Books.Model.Entities;

namespace Books.Data.EntityFramework.Config
{
    public class BooksConfig : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
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
