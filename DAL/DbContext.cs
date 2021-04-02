using System;
using System.Data.Entity;
using System.Linq;
using DAL.Entities;
namespace DAL
{
    public class MonopolyDbContext : System.Data.Entity.DbContext
    {
        public MonopolyDbContext()
            : base("name=MonopolyDb")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<MonopolyDbContext>());
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<BranchType> BranchTypes { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<RentSetting> RentSettings { get; set; }
    }
}