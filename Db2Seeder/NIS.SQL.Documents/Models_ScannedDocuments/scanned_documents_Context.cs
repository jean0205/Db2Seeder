using System;
using System.Configuration;
using Db2Seeder.API.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.NIS.SQL.Documents.Models_ScannedDocuments
{
    public partial class scanned_documents_Context : DbContext
    {      
        public scanned_documents_Context()
        {           
        }

        public scanned_documents_Context(DbContextOptions<scanned_documents_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<DocumentTypes> DocumentTypes { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<FailCodes> FailCodes { get; set; }
        public virtual DbSet<FailedImports> FailedImports { get; set; }
        public virtual DbSet<ImportLog> ImportLog { get; set; }
        public virtual DbSet<RegistrantTypes> RegistrantTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
             
                optionsBuilder.UseSqlServer($"Server=NISSQLSRV-01;Database={Settings.SQLDocuments()}Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentTypes>(entity =>
            {
                entity.HasKey(e => e.DocTypeId)
                    .HasName("PK__Document__C44DAEE1E70A218A");

                entity.ToTable("Document_Types");

                entity.Property(e => e.DocTypeId)
                    .HasColumnName("DOC_TYPE_ID")
                    .HasMaxLength(10);

                entity.Property(e => e.DocTypeDescription)
                    .HasColumnName("DOC_TYPE_DESCRIPTION")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DocTypeName)
                    .IsRequired()
                    .HasColumnName("DOC_TYPE_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Documents>(entity =>
            {
                entity.HasKey(e => e.FileId)
                    .HasName("PK__Document__49C04C7A2E46473F");

                entity.Property(e => e.FileId).HasColumnName("FILE_ID");

                entity.Property(e => e.ActiveCode)
                    .IsRequired()
                    .HasColumnName("ACTIVE_CODE")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClaimComment)
                    .HasColumnName("CLAIM_COMMENT")
                    .HasColumnType("text");

                entity.Property(e => e.ClaimNumber).HasColumnName("CLAIM_NUMBER");

                entity.Property(e => e.DeletedBy)
                    .HasColumnName("DELETED_BY")
                    .HasMaxLength(255);

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("DELETED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.DocTypeId)
                    .IsRequired()
                    .HasColumnName("DOC_TYPE_ID")
                    .HasMaxLength(10);

                entity.Property(e => e.FlOpn)
                    .HasColumnName("FL_OPN")
                    .HasMaxLength(50);

                entity.Property(e => e.GovClaim)
                    .HasColumnName("GOV_CLAIM")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.ImportId).HasColumnName("IMPORT_ID");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("MODIFIED_BY")
                    .HasMaxLength(255);

                entity.Property(e => e.ModifiedDatetime)
                    .HasColumnName("MODIFIED_DATETIME")
                    .HasColumnType("datetime");

                entity.Property(e => e.NisNumber).HasColumnName("NIS_NUMBER");

                entity.Property(e => e.PdfData).HasColumnName("PDF_DATA");

                entity.Property(e => e.ProFlag)
                    .HasColumnName("PRO_FLAG")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.RegistrantTypeId).HasColumnName("REGISTRANT_TYPE_ID");

                entity.Property(e => e.ScanDatetime)
                    .HasColumnName("SCAN_DATETIME")
                    .HasColumnType("datetime");

                entity.Property(e => e.ScannedBy)
                    .IsRequired()
                    .HasColumnName("SCANNED_BY")
                    .HasMaxLength(255);

                entity.Property(e => e.SubNumber).HasColumnName("SUB_NUMBER");
            });

            modelBuilder.Entity<FailCodes>(entity =>
            {
                entity.HasKey(e => e.FailCode)
                    .HasName("PK__Fail_Cod__06AF440B00BF1976");

                entity.ToTable("Fail_Codes");

                entity.Property(e => e.FailCode)
                    .HasColumnName("FAIL_CODE")
                    .ValueGeneratedNever();

                entity.Property(e => e.CodeDescription)
                    .HasColumnName("CODE_DESCRIPTION")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CodeName)
                    .IsRequired()
                    .HasColumnName("CODE_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FailedImports>(entity =>
            {
                entity.HasKey(e => e.FailedFileId)
                    .HasName("PK__Failed_I__19EDEBD8D35ED8B5");

                entity.ToTable("Failed_Imports");

                entity.Property(e => e.FailedFileId).HasColumnName("FAILED_FILE_ID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("CREATED_BY")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDatetime)
                    .HasColumnName("CREATION_DATETIME")
                    .HasColumnType("datetime");

                entity.Property(e => e.FailCode).HasColumnName("FAIL_CODE");

                entity.Property(e => e.FailedFileName)
                    .IsRequired()
                    .HasColumnName("FAILED_FILE_NAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImportId).HasColumnName("IMPORT_ID");
            });

            modelBuilder.Entity<ImportLog>(entity =>
            {
                entity.HasKey(e => e.ImportId)
                    .HasName("PK__Import_L__2298349CDD2BC721");

                entity.ToTable("Import_Log");

                entity.Property(e => e.ImportId).HasColumnName("IMPORT_ID");

                entity.Property(e => e.ImportDatetime)
                    .HasColumnName("IMPORT_DATETIME")
                    .HasColumnType("datetime");

                entity.Property(e => e.ImportedBy)
                    .IsRequired()
                    .HasColumnName("IMPORTED_BY")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RegistrantTypes>(entity =>
            {
                entity.HasKey(e => e.RegTypeId)
                    .HasName("PK__Registra__930284C52C1DA02C");

                entity.ToTable("Registrant_Types");

                entity.Property(e => e.RegTypeId)
                    .HasColumnName("REG_TYPE_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RegTypeDescription)
                    .HasColumnName("REG_TYPE_DESCRIPTION")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RegTypeName)
                    .IsRequired()
                    .HasColumnName("REG_TYPE_NAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
