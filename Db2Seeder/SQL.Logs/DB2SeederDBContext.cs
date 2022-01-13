using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.SQL.Logs
{
    public partial class DB2SeederDBContext : DbContext
    {
        public DB2SeederDBContext()
        {
        }

        public DB2SeederDBContext(DbContextOptions<DB2SeederDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ComplianceCertRequestLog> ComplianceCertRequestLog { get; set; }
        public virtual DbSet<EmployeeRequestLog> EmployeeRequestLog { get; set; }
        public virtual DbSet<EmployerRequestLog> EmployerRequestLog { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<RemittanceLog> RemittanceLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=NISSQLSRV-01;Database=DB2SeederDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComplianceCertRequestLog>(entity =>
            {
                entity.Property(e => e.BusinessName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompletedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompletedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<EmployeeRequestLog>(entity =>
            {
                entity.Property(e => e.CompletedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompletedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nisnumber).HasColumnName("NISNumber");

                entity.Property(e => e.PostedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<EmployerRequestLog>(entity =>
            {
                entity.Property(e => e.CompletedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompletedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmloyerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.CompletedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ErrorMessage).IsUnicode(false);

                entity.Property(e => e.RequestType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RemittanceLog>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.PostedOn).HasColumnType("datetime");

                entity.Property(e => e.TotalContribution).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalInsuranceEarning).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
