using GomiShop.Data.Interface;
using System.Data.Entity;

namespace GomiShop.Data
{
    internal class DatabaseContext : DbContext, IDataContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
            this.Database.CommandTimeout = 300;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public Database GetDbContext()
        {
            return this.Database;
        }
    }
}