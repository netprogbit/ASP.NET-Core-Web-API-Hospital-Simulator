using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DbContexts
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Measurement> Measurements { get; set; }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasIndex(u => u.Email).IsUnique(); // Email must be unique

            // Default database initialization

            modelBuilder.Entity<Patient>().HasData(
                    new Patient[]
                    {
                    new Patient { Id=1, Name="Admin", Email="admin@hospital.com", Password="RNjCdMV8vyDB/dAk79VFT0Vua3HlNly1wFe4xudzPnSDWD6iZY9WdpGBsUpy52UIWZV98VoQbXuchc9Gpw5qyg==", Role="admin"}
                    });
        }        
    }
}
