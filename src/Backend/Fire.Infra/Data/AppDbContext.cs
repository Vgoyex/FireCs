using Microsoft.EntityFrameworkCore;
using Fire.Domain.Entities;

namespace Fire.Infra.Data

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        public DbSet<UsersEntity> Users { get; set; }
        public DbSet<PostsEntity> Posts { get; set; }
        //public DbSet<Content> Content { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
