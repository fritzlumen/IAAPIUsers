using IAAPIUsers.Models;
using Microsoft.EntityFrameworkCore;

namespace IAAPIUsers.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder
                .UseSqlServer("Server=fritzlumen\\SQLEXPRESS;" +
                "Database=dbIAAPIUsers;" +
                "User=sa;" +
                "Password=123456;");
    }
}
