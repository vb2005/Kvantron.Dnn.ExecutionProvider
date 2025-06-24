using Microsoft.EntityFrameworkCore;

namespace Kvantron.Dnn.ExecutionProvider.Data
{
    /// <summary>
    /// Локальная база данных
    /// </summary>
    public class Database : DbContext
    {
        public Database()
        {
           //Database.EnsureDeleted();
           //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Private DB.db");
          //  optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=KogerentProd;User id=admin;Password=admin;Connect Timeout=5;TrustServerCertificate=Yes;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}