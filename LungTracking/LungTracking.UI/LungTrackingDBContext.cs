using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LungTracking.UI
{
    public partial class LungTrackingDBContext : IdentityDbContext
    {
        public LungTrackingDBContext()
        {
        }

        public LungTrackingDBContext(DbContextOptions<LungTrackingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAppointment> TblAppointments { get; set; }
        public virtual DbSet<TblBloodPressure> TblBloodPressures { get; set; }
        public virtual DbSet<TblBloodSugar> TblBloodSugars { get; set; }
        public virtual DbSet<TblCaregiver> TblCaregivers { get; set; }
        public virtual DbSet<TblFev1> TblFev1s { get; set; }
        public virtual DbSet<TblMedicationDetail> TblMedicationDetails { get; set; }
        public virtual DbSet<TblMedicationTime> TblMedicationTimes { get; set; }
        public virtual DbSet<TblMedicationTracking> TblMedicationTrackings { get; set; }
        public virtual DbSet<TblPatient> TblPatients { get; set; }
        public virtual DbSet<TblPatientCaregiverAccess> TblPatientCaregiverAccesses { get; set; }
        public virtual DbSet<TblPatientProviderAccess> TblPatientProviderAccesses { get; set; }
        public virtual DbSet<TblPef> TblPefs { get; set; }
        public virtual DbSet<TblProvider> TblProviders { get; set; }
        public virtual DbSet<TblPulse> TblPulses { get; set; }
        public virtual DbSet<TblSleepWake> TblSleepWakes { get; set; }
        public virtual DbSet<TblTemperature> TblTemperatures { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }
        public virtual DbSet<TblWeight> TblWeights { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=LungTracking.DB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblAppointment>(entity =>
            {
                entity.ToTable("tblAppointment");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TblAppointments)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblAppointment_PatientId");
            });

            modelBuilder.Entity<TblBloodPressure>(entity =>
            {
                entity.ToTable("tblBloodPressure");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Bpdiastolic).HasColumnName("BPdiastolic");

                entity.Property(e => e.Bpsystolic).HasColumnName("BPsystolic");

                entity.Property(e => e.Map)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("MAP");

                entity.Property(e => e.TimeOfDay).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TblBloodPressures)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblBloodPressure_PatientId");
            });

            modelBuilder.Entity<TblBloodSugar>(entity =>
            {
                entity.ToTable("tblBloodSugar");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.TimeOfDay).HasColumnType("datetime");

                entity.Property(e => e.TypeOfInsulinGiven)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TblBloodSugars)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblBloodSugar_PatientId");
            });

            modelBuilder.Entity<TblCaregiver>(entity =>
            {
                entity.ToTable("tblCaregiver");

                entity.HasIndex(e => e.UserId, "UQ__tblCareg__1788CC4DDE26ABDD")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.TblCaregiver)
                    .HasForeignKey<TblCaregiver>(d => d.UserId)
                    .HasConstraintName("tblCaregiver_UserId");
            });

            modelBuilder.Entity<TblFev1>(entity =>
            {
                entity.ToTable("tblFEV1");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Fev1number)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("FEV1Number");

                entity.Property(e => e.TimeOfDay).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TblFev1s)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblFEV1_PatientId");
            });

            modelBuilder.Entity<TblMedicationDetail>(entity =>
            {
                entity.ToTable("tblMedicationDetail");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateFilled).HasColumnType("date");

                entity.Property(e => e.MedicationDosagePerPill)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MedicationDosageTotal)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MedicationInstructions)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.MedicationName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.RefillDate).HasColumnType("date");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TblMedicationDetails)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblMedicationDetail_PatientId");
            });

            modelBuilder.Entity<TblMedicationTime>(entity =>
            {
                entity.ToTable("tblMedicationTime");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.PillTime).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TblMedicationTimes)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblMedicationTime_PatientId");
            });

            modelBuilder.Entity<TblMedicationTracking>(entity =>
            {
                entity.ToTable("tblMedicationTracking");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.PillTakenTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblPatient>(entity =>
            {
                entity.ToTable("tblPatient");

                entity.HasIndex(e => e.UserId, "UQ__tblPatie__1788CC4DA6753C0D")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.TblPatient)
                    .HasForeignKey<TblPatient>(d => d.UserId)
                    .HasConstraintName("tblPatient_UserId");
            });

            modelBuilder.Entity<TblPatientCaregiverAccess>(entity =>
            {
                entity.ToTable("tblPatientCaregiverAccess");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblPatientProviderAccess>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.ProviderId })
                    .HasName("PK__tblPatie__5C5A05E1E186CD3B");

                entity.ToTable("tblPatientProviderAccess");
            });

            modelBuilder.Entity<TblPef>(entity =>
            {
                entity.ToTable("tblPEF");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Pefnumber)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PEFNumber");

                entity.Property(e => e.TimeOfDay).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TblPefs)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblPEF_PatientId");
            });

            modelBuilder.Entity<TblProvider>(entity =>
            {
                entity.ToTable("tblProvider");

                entity.HasIndex(e => e.UserId, "UQ__tblProvi__1788CC4D9240DC62")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ImagePath).IsUnicode(false);

                entity.Property(e => e.JobDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.TblProvider)
                    .HasForeignKey<TblProvider>(d => d.UserId)
                    .HasConstraintName("tblProvider_UserId");
            });

            modelBuilder.Entity<TblPulse>(entity =>
            {
                entity.ToTable("tblPulse");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TimeOfDay).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TblPulses)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblPulse_PatientId");
            });

            modelBuilder.Entity<TblSleepWake>(entity =>
            {
                entity.ToTable("tblSleepWake");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TimeOfDay).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TblSleepWakes)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblSleepWake_PatientId");
            });

            modelBuilder.Entity<TblTemperature>(entity =>
            {
                entity.ToTable("tblTemperature");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TempNumber).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TimeOfDay).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TblTemperatures)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblTemperature_PatientId");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.ToTable("tblUser");

                entity.HasIndex(e => e.Username, "UQ__tblUser__536C85E4BD25323C")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__tblUser__A9D10534B155C261")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblWeight>(entity =>
            {
                entity.ToTable("tblWeight");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TimeOfDay).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TblWeights)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblWeight_PatientId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
