using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace DoctorApp.Models
{
    public class DoctorAppDbContext : DbContext
    {
        public DoctorAppDbContext(DbContextOptions<DoctorAppDbContext> options) :base(options)
        {
            
        }
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Define relationships
        //    modelBuilder.Entity<Appointment>()
        //        .HasOne(a => a.Patient)
        //        .WithMany(p => p.Appointments)
        //        .HasForeignKey(a => a.PId);

        //    modelBuilder.Entity<Appointment>()
        //        .HasOne(a => a.Doctor)
        //        .WithMany(d => d.Appointments)
        //        .HasForeignKey(a => a.DId);
        //}
    }
}

