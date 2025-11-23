using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Fire.Infra.Data;
using Fire.Infra.Repositories.Users;
using Fire.Infra.Repositories.Posts;
using Microsoft.EntityFrameworkCore;
using Fire.Application.Configuration;

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

        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<UsersService>();
            services.AddScoped<PostsService>();
            services.AddScoped<ContentService>();
            services.AddScoped<BucketService>();
            //services.AddScoped<CommentsService>();
            //services.AddScoped<>();
        }

        private static void AddBucketConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("r2");
            services.Configure<R2Settings>(section);
        }
    }
}
