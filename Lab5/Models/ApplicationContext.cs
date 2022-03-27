using Microsoft.EntityFrameworkCore;

namespace Lab5.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<HospitalModel> Hospitals { get; set; }
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<LablModel> Labs { get; set; }
        public DbSet<PatientModel> Patients { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
