using BiblioTechData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioTechData.ModelMappings
{
    public class EmployeeModelMapping : BaseModelMapping<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.ToTable("Employee");
            base.Configure(entity);

            entity
                .Property(c => c.LibraryId)
                .IsRequired()
                .HasColumnName("LibraryId");
            entity
                .HasOne(c => c.Library)
                .WithMany(c => c.Employees)
                .HasForeignKey(c => c.LibraryId);

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
        }
    }
}
