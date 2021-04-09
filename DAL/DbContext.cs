using System;
using System.Linq;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class MonopolyDbContext : DbContext
    {

		public MonopolyDbContext() : base()
        {

		}

        public MonopolyDbContext(DbContextOptions<MonopolyDbContext> options) : base(options)
        {

		}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile("appsettings.json", optional: false);

                var configuration = builder.Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity => {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Login).IsUnique();
            });
        }


        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<BranchType> BranchTypes { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<RentSetting> RentSettings { get; set; }
    }


	class ConnectionGetter
	{
        public static string GetConnection()
		{
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            return configuration.GetConnectionString("DefaultConnection");
        }
	}

	public class DbContextOptions 
	{
        public static DbContextOptions<MonopolyDbContext> GetOptions()
		{
            var optionsBuilder = new DbContextOptionsBuilder<MonopolyDbContext>();
            return optionsBuilder
                .UseSqlServer(ConnectionGetter.GetConnection())
                .Options;
        }
	}
}