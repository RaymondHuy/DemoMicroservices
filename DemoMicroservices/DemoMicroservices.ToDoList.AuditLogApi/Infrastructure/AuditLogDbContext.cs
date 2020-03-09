using DemoMicroservices.ToDoList.AuditLogApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoMicroservices.ToDoList.AuditLogApi.Infrastructure
{
    public class AuditLogDbContext : DbContext
    {
        public DbSet<ToDoLog> ToDoLogs { get; set; }

        public AuditLogDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("AuditLog");

            modelBuilder.Entity<ToDoLog>(e =>
            {
                e.HasKey(p => p.Id);
            });
        }
    }
}
