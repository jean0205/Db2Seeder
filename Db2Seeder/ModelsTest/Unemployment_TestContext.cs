using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.ModelsTest
{
    public partial class Unemployment_TestContext : DbContext
    {
        public Unemployment_TestContext()
        {
        }

        public Unemployment_TestContext(DbContextOptions<Unemployment_TestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ComplianceInterview> ComplianceInterview { get; set; }
        public virtual DbSet<ComplianceInterviewUserAccess> ComplianceInterviewUserAccess { get; set; }
        public virtual DbSet<DecObservations> DecObservations { get; set; }
        public virtual DbSet<RejectionCodes> RejectionCodes { get; set; }
        public virtual DbSet<RequestClaimMapping> RequestClaimMapping { get; set; }
        public virtual DbSet<ScannedTermCertificate> ScannedTermCertificate { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<TerminationCertificate> TerminationCertificate { get; set; }
        public virtual DbSet<UnempDeclaration> UnempDeclaration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=NISSQLSRV-01;Database=Unemployment_Test; User Id=document;Password=as400_s10479fr;TrustServerCertificate=yes");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComplianceInterview>(entity =>
            {
                entity.Property(e => e.InterviewTime).HasColumnType("datetime");

                entity.Property(e => e.Interviewer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LockedBy)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Nisnumber).HasColumnName("NISNumber");

                entity.Property(e => e.RequestedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RequestedTime).HasColumnType("datetime");

                entity.Property(e => e.RequesterComments)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Sep).HasColumnName("SEP");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ComplianceInterviewUserAccess>(entity =>
            {
                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DecObservations>(entity =>
            {
                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TimeW).HasColumnType("datetime");

                entity.Property(e => e.UserW)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RejectionCodes>(entity =>
            {
                entity.Property(e => e.Reasson)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RequestClaimMapping>(entity =>
            {
                entity.Property(e => e.RequestId).HasColumnName("RequestID");
            });

            modelBuilder.Entity<ScannedTermCertificate>(entity =>
            {
                entity.Property(e => e.DateOfTermination).HasColumnType("datetime");

                entity.Property(e => e.LastDayPaid).HasColumnType("datetime");

                entity.Property(e => e.LastDayatWork).HasColumnType("datetime");

                entity.Property(e => e.LieuWeeks).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LinkedBy)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LinkedTime).HasColumnType("datetime");

                entity.Property(e => e.SeveranceWeeks).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.RejectionDescription).HasMaxLength(450);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<TerminationCertificate>(entity =>
            {
                entity.Property(e => e.CertificatePdf).HasColumnName("CertificatePDF");

                entity.Property(e => e.LinkedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LinkedTime).HasColumnType("datetime");

                entity.Property(e => e.SavedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<UnempDeclaration>(entity =>
            {
                entity.Property(e => e.DeclarationJson).IsRequired();

                entity.Property(e => e.LockedBy)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessTime).HasColumnType("datetime");

                entity.Property(e => e.ProcessedBy)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ReassonForRejection)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RejectionComment)
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.SavedTime).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
