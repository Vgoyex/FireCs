using System.Text;
using Fire.Application.Configuration;
using Fire.Infra.Data;
using Fire.Infra.Repositories.Posts;
using Fire.Infra.Repositories.PostsLikes;
using Fire.Infra.Repositories.Users;
using FireCs.JWT;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Fire.Application.Services
{
    public static class DependencyInjection
    {
        public static void AddInfrasctructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);
            AddServices(services);
            AddDbContext_Postgres(services, configuration);
            AddBucketConfiguration(services, configuration);
            AddJwtConfiguration(services, configuration);
        }

        private static void AddDbContext_Postgres(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSQL");
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString),
                ServiceLifetime.Scoped // reforça que é Scoped
            );
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<IPostsLikesRepository, PostsLikesRepository>();

        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<UsersService>();
            services.AddScoped<PostsService>();
            services.AddScoped<ContentService>();
            services.AddScoped<BucketService>();
            services.AddScoped<JWTService>();
            //services.AddScoped<CommentsService>();
        }

        private static void AddBucketConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("r2");
            services.Configure<R2Settings>(section);
        }

        private static void AddJwtConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("JWT");
            var keyString = section["Key"];

            if (string.IsNullOrWhiteSpace(keyString))
                throw new Exception("Configuração JWT:Key está ausente no appsettings.json");

            var key = Encoding.UTF8.GetBytes(keyString);
            services.AddAuthentication("Bearer")
                        .AddJwtBearer("Bearer", options =>
                        {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(key),
                                ValidateIssuer = false,
                                ValidateAudience = false,
                                ValidateLifetime = true
                            };
                        });
        }


    }
}
