using BiblioTechData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioTechData.ModelMappings
{
    public class UserModelMapping : BaseModelMapping<User>
    {
        public override void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("User");
            base.Configure(entity);

            entity
                .Property(c => c.TypeId)
                .IsRequired()
                .HasColumnName("TypeId");
            entity
                .HasOne(c => c.Type)
                .WithMany(c => c.Users)
                .HasForeignKey(c => c.TypeId);

            entity
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnName("Name");

            entity
                .Property(c => c.Password)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("Password");

            entity
                .Property(c => c.Salt)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("Salt");

            entity
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(320)                
                .HasColumnName("Email");

            entity
                .Property(c => c.Phone)
                .IsRequired(false)
                .HasMaxLength(15)
                .HasColumnName("Phone");
        }
    }
}
