using ParkingInfrastructure.Models;

namespace ParkingInfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


 public class ParkingDbContext : DbContext
    {
        public ParkingDbContext(DbContextOptions<ParkingDbContext> option) : base(option)
        {
        }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        
        public virtual DbSet<ParkingSlot> ParkingSlot { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted });
#endif
        }
    }