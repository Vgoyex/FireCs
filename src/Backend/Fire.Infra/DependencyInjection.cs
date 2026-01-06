using Cassandra;
using Fire.Infra.Configuration;
using Fire.Infra.Data;
using Fire.Infra.Repositories.Posts;
using Fire.Infra.Repositories.PostsLikes;
using Fire.Infra.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Fire.Infra
{
    public static class DependencyInjection
    {
        public static void AddInfrasctructureCR(this IServiceCollection services, IConfiguration configuration)
        {
            AddCassandra(services, configuration);
            AddRedis(services, configuration);
            AddDbContext_Postgres(services, configuration);
            AddBucketConfiguration(services, configuration);
            AddRepositories(services);
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

        private static void AddCassandra(IServiceCollection services, IConfiguration configuration)
        {
            // Cassandra
            //services.AddSingleton<ISession>(_ =>
            //{
            //    var cluster = Cluster.Builder()
            //        .AddContactPoint(configuration["Cassandra:ContactPoint"])
            //        .Build();

            //    return cluster.Connect(configuration["Cassandra:Keyspace"]);
            //});
            Console.WriteLine("Cassandra");
        }
        private static void AddRedis(IServiceCollection services, IConfiguration configuration)
        {
            //Redis
            //services.AddSingleton<IConnectionMultiplexer>(
            //    ConnectionMultiplexer.Connect(
            //        configuration["Redis:Connection"]
            //    )
            //);
            Console.WriteLine("Redis");
        }

        private static void AddBucketConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("r2");
            services.Configure<R2Settings>(section);
        }

        
    }
}
