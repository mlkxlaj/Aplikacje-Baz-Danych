using Microsoft.EntityFrameworkCore;

namespace Zadanie9.Models
{
    public class s24427Context : DbContext
    {
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        public virtual DbSet<Medicament> Medicaments { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public s24427Context(DbContextOptions<s24427Context> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Hospital");

            modelBuilder.Entity<Prescription_Medicament>(entity =>
            {
                entity.HasKey(e => new { e.IdMedicament, e.IdPrescription }).HasName("Prescription_Medicament_PK");

                entity.ToTable("Prescription_Medicmanet");

                entity.Property(e => e.Dose);
                entity.Property(e => e.Details).HasMaxLength(120);

                entity
                    .HasOne(e => e.Medicament)
                    .WithMany(e => e.PrescriptionMedicaments)
                    .HasForeignKey(e => e.IdMedicament)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Prescription_Medicament_Medicament");

                entity
                    .HasOne(e => e.Prescription)
                    .WithMany(p => p.PrescriptionMedicaments)
                    .HasForeignKey(e => e.IdPrescription)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Prescription_Medicament_Prescription");
            });
            
            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription).HasName("Prescription_PK");

                entity.ToTable("Prescription");

                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.DueDate).IsRequired();

                entity
                    .HasOne(e => e.Patient)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(e => e.IdPatient)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Prescription_Patient");
                
                entity
                    .HasOne(e=>e.Doctor)
                    .WithMany(d=>d.Prescriptions)
                    .HasForeignKey(e => e.IdDoctor)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Prescription_Doctor");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor).HasName("Doctor_PK");
                
                entity.ToTable("Doctor");

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            });
            
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient).HasName("Patient_PK");

                entity.ToTable("Patient");

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.BirthDate).IsRequired();
            });
            
            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament).HasName("Medicament_PK");

                entity.ToTable("Medicament");

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(100);
            });
            
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser).HasName("User_PK");

                entity.ToTable("User");

                entity.Property(e => e.Login).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Salt).IsRequired().HasMaxLength(100);
            });
            
            modelBuilder.Entity<Medicament>().HasData(
                new Medicament
                {
                    IdMedicament = 1,
                    Name = "Tramadol",
                    Description = "Przeciwbolowy",
                    Type = "Tabletki"
                },
                new Medicament
                {
                    IdMedicament = 2,
                    Name = "Mindcards",
                    Description = "Antybiotyk",
                    Type = "Tabletki"
                }
            );
            
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    IdDoctor = 1,
                    FirstName = "Mikolaj",
                    LastName = "Kowaszewicz",
                    Email = "mkowaszewcz@gmail.com"
                },
                new Doctor
                {
                    IdDoctor = 2,
                    FirstName = "Filip",
                    LastName = "Cukierski",
                    Email = "fcukierski@gmail.com"
                }
            );
            
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    IdPatient = 1,
                    FirstName = "Tadeusz",
                    LastName = "Bartkiewicz",
                    BirthDate = new DateTime(2000, 1, 1)
                },
                new Patient
                {
                    IdPatient = 2,
                    FirstName = "Adam",
                    LastName = "Golec",
                    BirthDate = new DateTime(2000, 1, 1)
                }
            );
            
            modelBuilder.Entity<Prescription>().HasData(
                new Prescription
                {
                    IdPrescription = 1,
                    Date = new DateTime(2021, 1, 1),
                    DueDate = new DateTime(2025, 1, 1),
                    IdPatient = 1,
                    IdDoctor = 1
                },
                new Prescription
                {
                    IdPrescription = 2,
                    Date = new DateTime(2022, 1, 1),
                    DueDate = new DateTime(2026, 1, 1),
                    IdPatient = 2,
                    IdDoctor = 2
                }
            );
            
            
            modelBuilder.Entity<Prescription_Medicament>().HasData(
                new Prescription_Medicament
                {
                    IdMedicament = 1,
                    IdPrescription = 1,
                    Dose = 1,
                    Details = "Take daily"
                },
                new Prescription_Medicament
                {
                    IdMedicament = 2,
                    IdPrescription = 2,
                    Dose = 2,
                    Details = "Take once a week"
                }
            );
            
        }
    }
}
