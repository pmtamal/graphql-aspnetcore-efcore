using Microsoft.EntityFrameworkCore;
using GraphQLAgentApp.Models.Entities;
using GraphQLAgentApp.Repository.Configurations;

namespace GraphQLAgentApp.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BookEntityTypeConfiguration());
        }
    }
}