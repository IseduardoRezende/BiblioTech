using BiblioTechData.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioTechData.ModelMappings
{
    public abstract class BaseModelMapping<Model> : IEntityTypeConfiguration<Model> 
        where Model : class, IBaseModel 
    {
        public virtual void Configure(EntityTypeBuilder<Model> entity)
        {
            entity
                .Property(c => c.Id)
                .IsRequired()
                .HasColumnName("Id");
            entity
                .HasKey(c => c.Id);

            entity
                .Property(c => c.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .HasColumnName("CreatedAt");

            entity
                .Property(c => c.DeletedAt)
                .IsRequired(false)
                .HasColumnName("DeletedAt");
        }
    }
}
