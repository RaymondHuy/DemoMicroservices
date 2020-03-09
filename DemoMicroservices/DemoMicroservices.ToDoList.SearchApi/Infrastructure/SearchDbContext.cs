using DemoMicroservices.ToDoList.SearchApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoMicroservices.ToDoList.SearchApi.Infrastructure
{
    public class SearchDbContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public SearchDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Search");

            modelBuilder.Entity<Tag>(t =>
            {
                t.HasIndex(p => p.Keyword);
                t.HasKey(p => p.Id);
            });
        }
    }
}
