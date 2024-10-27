using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Business.Interfaces;
using MovieApp.Business.Mappings;
using MovieApp.Business.Services;
using MovieApp.Caching.Configurations;
using MovieApp.Caching.Interfaces;
using MovieApp.Caching.Services;
using MovieApp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITMDbService, TMDbService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMapperService, MapperService>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddHttpClient();
            services.RegisterCache(configuration);
            return services;

        }

        private static IServiceCollection RegisterCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));

            services.AddMemoryCache(options =>
            {
                options.SizeLimit = null;
            });
            services.AddScoped<ICacheService, MemoryCacheService>();


            return services;
        }
    }
}
