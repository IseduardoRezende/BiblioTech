using BiblioTechData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioTechData.ModelMappings
{
    public class PermissionModelMapping : BaseModelMapping<Permission>
    {
        public override void Configure(EntityTypeBuilder<Permission> entity)
        {
            entity.ToTable("Permission");
            base.Configure(entity);

            entity
                .Property(c => c.FunctionalityId)
                .IsRequired()
                .HasColumnName("FunctionalityId");
            entity
                .HasOne(c => c.Functionality)
                .WithMany(c => c.Permissions)
                .HasForeignKey(c => c.FunctionalityId);

            entity
                .Property(c => c.TypeId)
                .IsRequired()
                .HasColumnName("TypeId");
            entity
                .HasOne(c => c.Type)
                .WithMany(c => c.Permissions)
                .HasForeignKey(c => c.TypeId);

            entity
                .Property(c => c.Level)
                .IsRequired()                
                .HasColumnName("Level");
        }
    }
}
