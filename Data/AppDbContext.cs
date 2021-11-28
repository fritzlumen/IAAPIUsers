using IAAPIUsers.Models;
using Microsoft.EntityFrameworkCore;

namespace IAAPIUsers.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>()
                .Property(u => u.Username).HasMaxLength(150);
            mb.Entity<User>()
                .Property(u => u.Password).HasMaxLength(150);
            mb.Entity<User>()
                .Property(u => u.Role).HasMaxLength(150);
            mb.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "administrador",
                    Age = 0,
                    Password = "senha123",
                    Role = "manager"
                }
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder
                .UseSqlServer("Server=fritzlumen\\SQLEXPRESS;" +
                "Database=dbIAAPIUsers;" +
                "User=sa;" +
                "Password=123456;");
    }
}
