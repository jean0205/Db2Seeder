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
            modelBuilder.Entity<TerminationCertificate>(entity =>
            {
                entity.Property(e => e.CertificatePdf).HasColumnName("CertificatePDF");

                entity.Property(e => e.LinkedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SavedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<UnempDeclaration>(entity =>
            {
                entity.Property(e => e.DeclarationJson).IsRequired();

                entity.Property(e => e.SavedTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
