using DCare.Data.Entites;
using DCare.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Data.Context
{
    public class DCareDbContext : DbContext
    {
        public DCareDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                        .HasOne(a => a.Doctor)
                        .WithMany(d => d.Appointments)
                        .HasForeignKey(a => a.DoctorId)
                        .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Appointment>()
                        .HasOne(a => a.Patient)
                        .WithMany(p => p.Appointments)
                        .HasForeignKey(a => a.PatientId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Availability)
                .WithMany()
                .HasForeignKey(a => a.AvailabilityId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialty)
                .WithMany()
                .HasForeignKey(d => d.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Appointment>()
        .Property(a => a.Status)
        .HasConversion<string>()
        .HasDefaultValue(AppointmentStatus.Pending);
           

        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
