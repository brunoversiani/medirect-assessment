using MeDirectTest.Data.Maps;
using MeDirectTest.Models;

namespace MeDirectTest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options) { }

        public DbSet<TransactionModel> TransactionContext { get; set; }
        public DbSet<UserModel> UserContext { get; set; }
        //public DbSet<RateModel> RateContext { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionMap());
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
