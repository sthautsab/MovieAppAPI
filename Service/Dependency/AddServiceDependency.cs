using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Implementation;
using Service.Interfaces;

namespace Service.Dependency
{
    public static class AddServiceDependency
    {
        public static IServiceCollection AddServiceServices(this IServiceCollection services, IConfiguration configuration)
        {



            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ICommentService, CommentService>();



            return services; // Add this line to fix the error
        }
    }
}
