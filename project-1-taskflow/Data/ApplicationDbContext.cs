using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project_1_taskflow.Models;

namespace project_1_taskflow.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Công việc", Color = "#3498db" },
                new Category { Id = 2, Name = "Cá nhân", Color = "#2ecc71" },
                new Category { Id = 3, Name = "Học tập", Color = "#9b59b6" },
                new Category { Id = 4, Name = "Mua sắm", Color = "#e74c3c" },
                new Category { Id = 5, Name = "Khác", Color = "#95a5a6" }
            );
        }
    }
}
