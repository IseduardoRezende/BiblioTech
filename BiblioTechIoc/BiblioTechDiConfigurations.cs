using BiblioTechData.Repositories;
using BiblioTechData.Repositories.IRepository;
using BiblioTechDomain.Profiles;
using BiblioTechDomain.Services;
using BiblioTechDomain.Services.IService;
using Microsoft.Extensions.DependencyInjection;

namespace BiblioTechIoc
{
    public static class BiblioTechDiConfigurations
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IFunctionalityRepository, FunctionalityRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ILibraryRepository, LibraryRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IFunctionalityService, FunctionalityService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ILibraryService, LibraryService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ITypeService, TypeService>();
        }

        public static void AddAutoMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(a =>
            {
                a.AddProfile<FunctionalityProfile>();
                a.AddProfile<PermissionProfile>();
                a.AddProfile<EmployeeProfile>();
                a.AddProfile<LibraryProfile>();
                a.AddProfile<GenreProfile>();
                a.AddProfile<UserProfile>();
                a.AddProfile<BookProfile>();
                a.AddProfile<TypeProfile>();
            });
        }
    }
}
