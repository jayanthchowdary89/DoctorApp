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
    }
}
