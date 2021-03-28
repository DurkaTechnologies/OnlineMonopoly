using System;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class MonopolyDbContext : System.Data.Entity.DbContext
    {
       
        public MonopolyDbContext()
            : base("name=MonopolyDb")
        {
            //Database.SetInitializer(new Initializer());
        }

        public virtual DbSet<User> Users { get; set; }
    }

    
}