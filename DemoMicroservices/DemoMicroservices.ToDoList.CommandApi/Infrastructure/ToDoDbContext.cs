using DemoMicroservices.ToDoList.CommandApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoMicroservices.ToDoList.CommandApi.Infrastructure
{
    public class ToDoDbContext : DbContext
    {
        public DbSet<ToDo> ToDos { get; set; }

        public ToDoDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Core");

            modelBuilder.Entity<ToDo>(e =>
            {
                e.HasKey(p => p.Id);
            });
        }
    }
}
