using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LungTracking.PL
{
    public partial class LungTrackingEntities : DbContext
    {
        public LungTrackingEntities()
        {
        }

        public LungTrackingEntities(DbContextOptions<LungTrackingEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<tblAppointment> tblAppointments { get; set; }
        public virtual DbSet<tblBloodPressure> tblBloodPressures { get; set; }
        public virtual DbSet<tblBloodSugar> tblBloodSugars { get; set; }
        public virtual DbSet<tblCaregiver> tblCaregivers { get; set; }
        public virtual DbSet<tblFev1> tblFev1s { get; set; }
        public virtual DbSet<tblMedicationDetail> tblMedicationDetails { get; set; }
        public virtual DbSet<tblMedicationTime> tblMedicationTimes { get; set; }
        public virtual DbSet<tblMedicationTracking> tblMedicationTrackings { get; set; }
        public virtual DbSet<tblPatient> tblPatients { get; set; }
        public virtual DbSet<tblPatientCaregiverAccess> tblPatientCaregiverAccesses { get; set; }
        public virtual DbSet<tblPatientProviderAccess> tblPatientProviderAccesses { get; set; }
        public virtual DbSet<tblPef> tblPefs { get; set; }
        public virtual DbSet<tblProvider> tblProviders { get; set; }
        public virtual DbSet<tblPulse> tblPulses { get; set; }
        public virtual DbSet<tblSleepWake> tblSleepWakes { get; set; }
        public virtual DbSet<tblTemperature> tblTemperatures { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblWeight> tblWeights { get; set; }

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

            modelBuilder.Entity<tblAppointment>(entity =>
            {
                entity.ToTable("tblAppointment");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.tblAppointments)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblAppointment_PatientId");
            });

            modelBuilder.Entity<tblBloodPressure>(entity =>
            {
                entity.ToTable("tblBloodPressure");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Bpdiastolic).HasColumnName("BPdiastolic");

                entity.Property(e => e.Bpsystolic).HasColumnName("BPsystolic");

                entity.Property(e => e.TimeOfDay).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.tblBloodPressures)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblBloodPressure_PatientId");
            });

            modelBuilder.Entity<tblBloodSugar>(entity =>
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
                    .WithMany(p => p.tblBloodSugars)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblBloodSugar_PatientId");
            });

            modelBuilder.Entity<tblCaregiver>(entity =>
            {
                entity.ToTable("tblCaregiver");

                entity.HasIndex(e => e.UserId, "UQ__tblCareg__1788CC4D28B7EC83")
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
                    .WithOne(p => p.tblCaregiver)
                    .HasForeignKey<tblCaregiver>(d => d.UserId)
                    .HasConstraintName("tblCaregiver_UserId");
            });

            modelBuilder.Entity<tblFev1>(entity =>
            {
                entity.ToTable("tblFEV1");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Fev1number)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("FEV1Number");

                entity.Property(e => e.TimeOfDay).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.tblFev1s)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblFEV1_PatientId");
            });

            modelBuilder.Entity<tblMedicationDetail>(entity =>
            {
                entity.ToTable("tblMedicationDetails");

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
                    .WithMany(p => p.tblMedicationDetails)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblMedicationDetails_PatientId");
            });

            modelBuilder.Entity<tblMedicationTime>(entity =>
            {
                entity.ToTable("tblMedicationTime");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.tblMedicationTimes)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblMedicationTime_PatientId");
            });

            modelBuilder.Entity<tblMedicationTracking>(entity =>
            {
                entity.ToTable("tblMedicationTracking");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.PillTakenTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<tblPatient>(entity =>
            {
                entity.ToTable("tblPatient");

                entity.HasIndex(e => e.UserId, "UQ__tblPatie__1788CC4DAC55FE73")
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
                    .WithOne(p => p.tblPatient)
                    .HasForeignKey<tblPatient>(d => d.UserId)
                    .HasConstraintName("tblPatient_UserId");
            });

            modelBuilder.Entity<tblPatientCaregiverAccess>(entity =>
            {
                entity.ToTable("tblPatientCaregiverAccess");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<tblPatientProviderAccess>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.ProviderId })
                    .HasName("PK__tblPatie__5C5A05E1181ADD4F");

                entity.ToTable("tblPatientProviderAccess");
            });

            modelBuilder.Entity<tblPef>(entity =>
            {
                entity.ToTable("tblPEF");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Pefnumber)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PEFNumber");

                entity.Property(e => e.TimeOfDay).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.tblPefs)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblPEF_PatientId");
            });

            modelBuilder.Entity<tblProvider>(entity =>
            {
                entity.ToTable("tblProvider");

                entity.HasIndex(e => e.UserId, "UQ__tblProvi__1788CC4D0D25A474")
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
                    .WithOne(p => p.tblProvider)
                    .HasForeignKey<tblProvider>(d => d.UserId)
                    .HasConstraintName("tblProvider_UserId");
            });

            modelBuilder.Entity<tblPulse>(entity =>
            {
                entity.ToTable("tblPulse");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TimeOfDay).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.tblPulses)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblPulse_PatientId");
            });

            modelBuilder.Entity<tblSleepWake>(entity =>
            {
                entity.ToTable("tblSleepWake");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TimeOfDay).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.tblSleepWakes)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblSleepWake_PatientId");
            });

            modelBuilder.Entity<tblTemperature>(entity =>
            {
                entity.ToTable("tblTemperature");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TempNumber).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TimeOfDay).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.tblTemperatures)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblTemperature_PatientId");
            });

            modelBuilder.Entity<tblUser>(entity =>
            {
                entity.ToTable("tblUser");

                entity.HasIndex(e => e.Username, "UQ__tblUser__536C85E4FDB5D24D")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__tblUser__A9D1053424F383AC")
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

            modelBuilder.Entity<tblWeight>(entity =>
            {
                entity.ToTable("tblWeight");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TimeOfDay).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.tblWeights)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("tblWeight_PatientId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
