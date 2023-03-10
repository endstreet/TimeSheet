using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Trm.MaLogger.MsData.Models;

namespace Trm.MaLogger.MsData.Context
{
    public class MaLoggerDbContext : DbContext
    {
        //private string? _dbConnectionString;

        public MaLoggerDbContext(DbContextOptions<MaLoggerDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new Exception("Database configuration error.");
                //for creating local migrations:
                //optionsBuilder.UseSqlServer(_dbConnectionString);
            }
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });
            modelBuilder.Entity<Client>()
                .HasData(
                    new Client
                    {
                        Id = 1,
                        Name = "TRM Internal"
                    }
                );
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });
            modelBuilder.Entity<Role>()
                .HasData(
                    new Role
                    {
                        Id = 1,
                        Name = "User"
                    },
                    new Role
                    {
                        Id = 2,
                        Name = "Admin"
                    }
                );
        }
    }
}
