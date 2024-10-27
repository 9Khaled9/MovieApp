using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Persistence;
using MovieApp.Business;
using System.Text;
using MovieApp.Api.Models.Jwt;

namespace MovieApp.Api
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            configuration = BuildConfigurations(configuration);
            //var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            services.AddAuthentication(configuration);

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.RegisterBusinessServices(configuration);
            services.AddPersistenceServices(configuration);

            return services;
        }

        private static IConfigurationRoot BuildConfigurations(IConfiguration configuration)
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // JWT Configuration
            var jwtSettings = configuration.GetSection("Authentication:Jwt");
            var secretKey = jwtSettings.GetValue<string>("SecretKey");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.GetValue<string>("Audience"),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ValidateLifetime = true
                };
            })
            .AddGoogle("Google", options =>
            {
                var googleSettings = configuration.GetSection("Authentication:Google");
                options.ClientId = googleSettings["ClientId"];
                options.ClientSecret = googleSettings["ClientSecret"];
                options.CallbackPath = googleSettings["CallbackPath"];
            })
            .AddOpenIdConnect("AzureAdB2C", options =>
            {
                var azureB2CSettings = configuration.GetSection("Authentication:AzureAdB2C");
                options.ClientId = azureB2CSettings["ClientId"];
                options.Authority = $"{azureB2CSettings["Instance"]}/{azureB2CSettings["Domain"]}/{azureB2CSettings["SignUpSignInPolicyId"]}/v2.0";
                options.CallbackPath = azureB2CSettings["CallbackPath"];
                options.ResponseType = "id_token";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidAudience = azureB2CSettings["ClientId"],
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                };
            });
            return services;
        }
    }
}
