using BiblioTechData.ModelMappings;
using BiblioTechData.Models;
using Microsoft.EntityFrameworkCore;

namespace BiblioTechData
{
    public class BiblioTechContext : DbContext
    {
        public BiblioTechContext(DbContextOptions<BiblioTechContext> options) : base(options) 
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
            
        public DbSet<Library> Library { get; set; }
        
        public DbSet<User> User { get; set; }
        
        public DbSet<Employee> Employee { get; set; }

        public DbSet<Book> Book { get; set; }

        public DbSet<Genre> Genre { get; set; }        
        
        public DbSet<Type> Type { get; set; }

        public DbSet<Permission> Permission { get; set; }

        public DbSet<Functionality> Functionality { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new LibraryModelMapping().Configure(modelBuilder.Entity<Library>());
            new UserModelMapping().Configure(modelBuilder.Entity<User>());
            new EmployeeModelMapping().Configure(modelBuilder.Entity<Employee>());
            new BookModelMapping().Configure(modelBuilder.Entity<Book>());        
            new GenreModelMapping(descriptionMaxLength: 50).Configure(modelBuilder.Entity<Genre>());        
            new TypeModelMapping(descriptionMaxLength: 30).Configure(modelBuilder.Entity<Type>());
            new PermissionModelMapping().Configure(modelBuilder.Entity<Permission>());
            new FunctionalityModelMapping(descriptionMaxLength: 50).Configure(modelBuilder.Entity<Functionality>());
        }
    }
}
