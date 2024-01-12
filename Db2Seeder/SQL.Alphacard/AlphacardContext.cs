using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.SQL.Alphacard
{
    public partial class AlphacardContext : DbContext
    {
        public AlphacardContext()
        {
        }

        public AlphacardContext(DbContextOptions<AlphacardContext> options)
            : base(options)
        {
        }

        public virtual DbSet<NiXport> NiXport { get; set; }
        public virtual DbSet<Nixport1> Nixport1 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=NISSQLSRV-02;Initial Catalog=Alphacard;User Id=alphacard;Password=alpha_s10479fr;TrustServerCertificate=yes;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NiXport>(entity =>
            {
                entity.Property(e => e.Dob).IsUnicode(false);

                entity.Property(e => e.Fname).IsUnicode(false);

                entity.Property(e => e.Lastmod).IsUnicode(false);

                entity.Property(e => e.Lname).IsUnicode(false);

                entity.Property(e => e.Natl).IsUnicode(false);

                entity.Property(e => e.Nisnum).IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);

                entity.Property(e => e.Plob).IsUnicode(false);

                entity.Property(e => e.Sex).IsUnicode(false);

                entity.Property(e => e.Signature).IsUnicode(false);
            });

            modelBuilder.Entity<Nixport1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("NIXPORT");

                entity.Property(e => e.Dob).IsUnicode(false);

                entity.Property(e => e.Fname).IsUnicode(false);

                entity.Property(e => e.Issued)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Lastmod).IsUnicode(false);

                entity.Property(e => e.Lname).IsUnicode(false);

                entity.Property(e => e.Natl).IsUnicode(false);

                entity.Property(e => e.Nisnum).IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);

                entity.Property(e => e.Plob).IsUnicode(false);

                entity.Property(e => e.Sex).IsUnicode(false);

                entity.Property(e => e.Signature).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
