using Microsoft.EntityFrameworkCore;
using MyFinance.Application.Interfaces;
using MyFinance.Domain;

namespace MyFinance.Persistence
{
    public class DataDbContext : DbContext, IDataDbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
