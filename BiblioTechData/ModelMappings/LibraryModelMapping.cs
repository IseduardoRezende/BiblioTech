using BiblioTechData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioTechData.ModelMappings
{
    public class LibraryModelMapping : BaseModelMapping<Library>
    {
        public override void Configure(EntityTypeBuilder<Library> entity)
        {
            entity.ToTable("Library");
            base.Configure(entity);

            entity
                .Property(c => c.UserId)
                .IsRequired()
                .HasColumnName("UserId");
            entity
                .HasOne(c => c.User)
                .WithMany(c => c.Libraries)
                .HasForeignKey(c => c.UserId);

            entity
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnName("Name");

            entity
                .Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(6)
                .HasColumnName("Code");

            entity
                .Property(c => c.Image)
                .IsRequired(false)
                .HasColumnName("Image");

            entity
                .Property(a => a.Address)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("Address");

            entity
                .Property(a => a.Number)
                .HasMaxLength(8)
                .IsRequired()
                .HasColumnName("Number");

            entity
                .Property(a => a.Complement)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasColumnName("Complement");

            entity
                .Property(a => a.City)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("City");

            entity
                .Property(a => a.UF)
                .HasMaxLength(2)
                .IsRequired()
                .HasColumnName("UF");

            entity
                .Property(a => a.PostalCode)
                .HasMaxLength(8)
                .IsRequired()
                .HasColumnName("PostalCode");

            entity
                .Property(a => a.Phone)
                .HasMaxLength(15)
                .IsRequired()
                .HasColumnName("Phone");
        }
    }
}
