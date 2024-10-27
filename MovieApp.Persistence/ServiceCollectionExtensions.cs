using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Persistence.IRepository;
using MovieApp.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            ApplicationConfiguration.MovieAppDBConnection = configuration.GetConnectionString("MovieAppDBConnection");

            // Register DbContext with the connection string
            services.AddDbContext<MovieAppDbContext>(options =>
                options.UseSqlServer(ApplicationConfiguration.MovieAppDBConnection));

            // Register the repositories and Unit of Work
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
