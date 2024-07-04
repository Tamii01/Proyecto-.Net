using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext() : base() { }

        public static string ConnectionString { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			optionsBuilder.UseSqlServer(ConnectionString);
		}

        public DbSet<Usuarios> Usuarios { get; set; }

    }
}
