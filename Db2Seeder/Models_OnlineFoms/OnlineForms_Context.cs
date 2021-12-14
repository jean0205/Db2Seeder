using System;
using System.Configuration;
using Db2Seeder.API.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.Models_OnlineFoms
{
    public partial class OnlineForms_Context : DbContext
    {
        public OnlineForms_Context()
        {
        }

        public OnlineForms_Context(DbContextOptions<OnlineForms_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<ClaimEmpe> ClaimEmpe { get; set; }
        public virtual DbSet<ClaimEmpr> ClaimEmpr { get; set; }
        public virtual DbSet<ComplianceCert> ComplianceCert { get; set; }
        public virtual DbSet<EmailNotification> EmailNotification { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeNis> EmployeeNis { get; set; }
        public virtual DbSet<EmployeeRegist> EmployeeRegist { get; set; }
        public virtual DbSet<Employer> Employer { get; set; }
        public virtual DbSet<EmployerRegist> EmployerRegist { get; set; }
        public virtual DbSet<SelfEmployed> SelfEmployed { get; set; }
        public virtual DbSet<UnempEmployee> UnempEmployee { get; set; }
        public virtual DbSet<UnempEmployer> UnempEmployer { get; set; }
        public virtual DbSet<UnempGoverment> UnempGoverment { get; set; }
        public virtual DbSet<UnempPayroll> UnempPayroll { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {              
                optionsBuilder.UseSqlServer($"Server=NISSQLSRV-01;Database={Settings.OnlineForms()};Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClaimEmpe>(entity =>
            {
                entity.Property(e => e.AppDate).HasColumnType("date");

                entity.Property(e => e.AppState)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateProc).HasColumnType("date");

                entity.Property(e => e.DateUpload).HasColumnType("date");

                entity.Property(e => e.FileUrl)
                    .IsRequired()
                    .HasColumnName("FileURL")
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.NisChanged).IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserUpload)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClaimEmpr>(entity =>
            {
                entity.Property(e => e.AppState)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicationDate).HasColumnType("date");

                entity.Property(e => e.DateUpload).HasColumnType("date");

                entity.Property(e => e.Dateproc).HasColumnType("date");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.EmployerName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.NumberChanged).IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserUpload)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ComplianceCert>(entity =>
            {
                entity.Property(e => e.AppDate).HasColumnType("date");

                entity.Property(e => e.AppState)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessAddress)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.BusinessName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CertificatePath).IsUnicode(false);

                entity.Property(e => e.DateProcess).HasColumnType("date");

                entity.Property(e => e.DateUpload).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.EmployerNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerSub)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("date");

                entity.Property(e => e.Form)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UserProcess)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserUpload)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmailNotification>(entity =>
            {
                entity.Property(e => e.Body)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.DateSent).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UserSent)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.AccountType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicationState)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicationType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bank).IsUnicode(false);

                entity.Property(e => e.BirthCertificate).IsUnicode(false);

                entity.Property(e => e.BusinessAccountNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessAccountType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessAddress)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessBank).IsUnicode(false);

                entity.Property(e => e.BusinessCommenced).HasColumnType("date");

                entity.Property(e => e.BusinessEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessFax)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessMobile)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessParish)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.BusinnessTown)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.DateEmploymentEnd).HasColumnType("date");

                entity.Property(e => e.DateMarriage)
                    .HasColumnName("dateMarriage")
                    .HasColumnType("date");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("dateOfBirth")
                    .HasColumnType("date");

                entity.Property(e => e.DateProcessed).HasColumnType("date");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.FormFile).IsUnicode(false);

                entity.Property(e => e.HomePhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ImportedId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastEmployer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MaidenName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MailingAddress)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MaritalStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MarriageCertificate).IsUnicode(false);

                entity.Property(e => e.Mobile2Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MonthlyIncome).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Nationality)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NatureOfBusiness)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nisnumber)
                    .HasColumnName("NISNumber")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Note).IsUnicode(false);

                entity.Property(e => e.Parish)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);

                entity.Property(e => e.PictureId)
                    .HasColumnName("PictureID")
                    .IsUnicode(false);

                entity.Property(e => e.PreviusIncome).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Town)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmployeeNis>(entity =>
            {
                entity.Property(e => e.DateProc).HasColumnType("date");

                entity.Property(e => e.Email)
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

                entity.Property(e => e.NisNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmployeeRegist>(entity =>
            {
                entity.Property(e => e.EmployedDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ImportedId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MiddelName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.EmployeeRegist)
                    .HasForeignKey(d => d.EmployerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeRegist_EmployerRegist");
            });

            modelBuilder.Entity<Employer>(entity =>
            {
                entity.Property(e => e.BusinesName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateProc).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmployerRegist>(entity =>
            {
                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.AccountType)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.AppDate).HasColumnType("date");

                entity.Property(e => e.AppState)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Bank)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessAddress)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessDate).HasColumnType("date");

                entity.Property(e => e.BusinessName)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessParish)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessTown)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessType)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CertIncorporation).IsUnicode(false);

                entity.Property(e => e.CertRegistration).IsUnicode(false);

                entity.Property(e => e.Comments).IsUnicode(false);

                entity.Property(e => e.DateProcess).HasColumnType("date");

                entity.Property(e => e.DateUpload).HasColumnType("date");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerName)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Form).IsUnicode(false);

                entity.Property(e => e.MailingAddress)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile1)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile2)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NatureObusiness)
                    .IsRequired()
                    .HasColumnName("NatureOBusiness")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NoticeOdirectors)
                    .HasColumnName("NoticeODirectors")
                    .IsUnicode(false);

                entity.Property(e => e.NoticeRegisteredOffice).IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserProcess)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserUpload)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SelfEmployed>(entity =>
            {
                entity.Property(e => e.DateProc).HasColumnType("date");

                entity.Property(e => e.Email)
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

                entity.Property(e => e.NisNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UnempEmployee>(entity =>
            {
                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.AccountType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AppState)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bank)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Benefits).IsUnicode(false);

                entity.Property(e => e.ClaimNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateProc).HasColumnType("date");

                entity.Property(e => e.DateUpload).HasColumnType("date");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.LastName)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MonthApplied).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NisChanged).IsUnicode(false);

                entity.Property(e => e.NisNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserUpload)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.UnempEmployee)
                    .HasForeignKey(d => d.EmployerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnempEmployee_UnempEmployer");
            });

            modelBuilder.Entity<UnempEmployer>(entity =>
            {
                entity.Property(e => e.AppState)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicationDate).HasColumnType("date");

                entity.Property(e => e.DateClosed).HasColumnType("date");

                entity.Property(e => e.DateUpload).HasColumnType("date");

                entity.Property(e => e.Dateproc).HasColumnType("date");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.EmployerName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.EmployerNo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerNumberChanged).IsUnicode(false);

                entity.Property(e => e.EmployerSub)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FormFile)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.GovertmentUser)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.Officer)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserUpload)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UnempGoverment>(entity =>
            {
                entity.Property(e => e.DateProc).HasColumnType("date");

                entity.Property(e => e.EmployerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerSub)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UnempPayroll>(entity =>
            {
                entity.Property(e => e.AccountName).IsUnicode(false);

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Bank).IsUnicode(false);

                entity.Property(e => e.BankAccount)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNo1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNo2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNo3)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateInserted).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GivenNames)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NisNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SurName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VehicleReg)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
