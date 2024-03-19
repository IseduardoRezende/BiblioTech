using BiblioTechData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioTechData.ModelMappings
{
    public class BookModelMapping : BaseModelMapping<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> entity)
        {
            entity.ToTable("Book");
            base.Configure(entity);
            
            entity
                .Property(c => c.LibraryId)
                .IsRequired()
                .HasColumnName("LibraryId");
            entity
                .HasOne(c => c.Library)
                .WithMany(c => c.Books)
                .HasForeignKey(c => c.LibraryId);

            entity
                .Property(c => c.GenreId)
                .IsRequired()
                .HasColumnName("GenreId");
            entity
                .HasOne(c => c.Genre)
                .WithMany(c => c.Books)
                .HasForeignKey(c => c.GenreId);

            entity
                .Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Title");

            entity
                .Property(c => c.ReleaseDate)
                .IsRequired()                
                .HasColumnName("ReleaseDate");

            entity
                .Property(c => c.Image)
                .IsRequired(false)
                .HasColumnName("Image");

            entity
                .Property(c => c.ISBN)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("ISBN");

            entity
                .Property(c => c.Exemplary)
                .IsRequired()
                .HasColumnName("Exemplary");

            entity
                .Property(c => c.Volume)
                .IsRequired()
                .HasColumnName("Volume");

            entity
                .Property(c => c.Pages)
                .IsRequired(false)
                .HasColumnName("Pages");
        }
    }
}
