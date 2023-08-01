using DAL.Repository.Implementation;
using DAL.Repository.Interface;
using DAL.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Dependency
{
    public static class DALDependency
    {
        public static IServiceCollection AddDALServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MovieConnect");

            services.AddDbContext<MovieContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            options.SignIn.RequireConfirmedAccount = true)
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<MovieContext>();

            services.AddScoped<ISeedRole, SeedRole>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();



            return services; // Add this line to fix the error
        }
    }
}
