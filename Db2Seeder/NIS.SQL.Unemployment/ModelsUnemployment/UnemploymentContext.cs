using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.NIS.SQL.Unemployment.ModelsUnemployment
{
    public partial class UnemploymentContext : DbContext
    {
        public UnemploymentContext()
        {
        }

        public UnemploymentContext(DbContextOptions<UnemploymentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DeclarationStatus> DeclarationStatus { get; set; }
        public virtual DbSet<RejectionCodes> RejectionCodes { get; set; }
        public virtual DbSet<RequestClaimMapping> RequestClaimMapping { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<TerminationCertificate> TerminationCertificate { get; set; }
        public virtual DbSet<UnempDeclaration> UnempDeclaration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=NISSQLSRV-01;Database=Unemployment;User Id=document;Password=as400_s10479fr;TrustServerCertificate=yes");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeclarationStatus>(entity =>
            {
                entity.HasKey(e => new { e.DeclarationsId, e.StatussesId });

                entity.HasOne(d => d.Declarations)
                    .WithMany(p => p.DeclarationStatus)
                    .HasForeignKey(d => d.DeclarationsId)
                    .HasConstraintName("FK_DeclarationStatus_Declarations_DeclarationsId");

                entity.HasOne(d => d.Statusses)
                    .WithMany(p => p.DeclarationStatus)
                    .HasForeignKey(d => d.StatussesId);
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

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Status)
                    .HasForeignKey<Status>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Status_RejectionCodes");
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
