using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.NIS.SQL.Webportal
{
    public partial class WebPortalContext : DbContext
    {
        public WebPortalContext()
        {
        }

        public WebPortalContext(DbContextOptions<WebPortalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CashHistory> CashHistory { get; set; }
        public virtual DbSet<Ceilings> Ceilings { get; set; }
        public virtual DbSet<Gndbank> Gndbank { get; set; }
        public virtual DbSet<NiCnte> NiCnte { get; set; }
        public virtual DbSet<NiCnteStaging> NiCnteStaging { get; set; }
        public virtual DbSet<NiCons> NiCons { get; set; }
        public virtual DbSet<NiEmpe> NiEmpe { get; set; }
        public virtual DbSet<NiEmpeStaging> NiEmpeStaging { get; set; }
        public virtual DbSet<NiEmpr> NiEmpr { get; set; }
        public virtual DbSet<NiEmprStaging> NiEmprStaging { get; set; }
        public virtual DbSet<NiInspt> NiInspt { get; set; }
        public virtual DbSet<NiXport> NiXport { get; set; }
        public virtual DbSet<QVend> QVend { get; set; }
        public virtual DbSet<Rates> Rates { get; set; }
        public virtual DbSet<RatesDetails> RatesDetails { get; set; }
        public virtual DbSet<TtAritm> TtAritm { get; set; }
        public virtual DbSet<TtArop> TtArop { get; set; }
        public virtual DbSet<TtArpmt> TtArpmt { get; set; }
        public virtual DbSet<TtArsum> TtArsum { get; set; }
        public virtual DbSet<TtCash> TtCash { get; set; }
        public virtual DbSet<TtChst> TtChst { get; set; }
        public virtual DbSet<TtCnte> TtCnte { get; set; }
        public virtual DbSet<TtCntr> TtCntr { get; set; }
        public virtual DbSet<TtCnts> TtCnts { get; set; }
        public virtual DbSet<TtConh> TtConh { get; set; }
        public virtual DbSet<TtEcwe> TtEcwe { get; set; }
        public virtual DbSet<TtEcxe> TtEcxe { get; set; }
        public virtual DbSet<TtEmpe> TtEmpe { get; set; }
        public virtual DbSet<TtRcwe> TtRcwe { get; set; }
        public virtual DbSet<TtRcxe> TtRcxe { get; set; }
        public virtual DbSet<TtStat> TtStat { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=NISSQLSRV-02;Database=WebPortal;User Id=Webportal;Password=Welcome1;TrustServerCertificate=yes");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CashHistory>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Actv24)
                    .IsRequired()
                    .HasColumnName("ACTV24")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Cheq24)
                    .HasColumnName("CHEQ24")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Chng24)
                    .HasColumnName("CHNG24")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Code24)
                    .HasColumnName("CODE24")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Crf124)
                    .IsRequired()
                    .HasColumnName("CRF124")
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Crf224)
                    .IsRequired()
                    .HasColumnName("CRF224")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Curr24)
                    .IsRequired()
                    .HasColumnName("CURR24")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Date24)
                    .HasColumnName("DATE24")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Dsct24)
                    .HasColumnName("DSCT24")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Fcyr24)
                    .HasColumnName("FCYR24")
                    .HasColumnType("numeric(4, 0)");

                entity.Property(e => e.Fmon24)
                    .HasColumnName("FMON24")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Init24)
                    .IsRequired()
                    .HasColumnName("INIT24")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Paid24)
                    .HasColumnName("PAID24")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Rcpt24)
                    .HasColumnName("RCPT24")
                    .HasColumnType("decimal(9, 0)");

                entity.Property(e => e.Rreg24)
                    .HasColumnName("RREG24")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rrsf24)
                    .HasColumnName("RRSF24")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Rtyp24)
                    .HasColumnName("RTYP24")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Time24)
                    .HasColumnName("TIME24")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Totl24)
                    .HasColumnName("TOTL24")
                    .HasColumnType("decimal(13, 2)");
            });

            modelBuilder.Entity<Ceilings>(entity =>
            {
                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.MaxFortnightly).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MaxMonthly).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MaxWeekly).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinFortnightly).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinMonthly).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinWeekly).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ToDate).HasColumnType("date");
            });

            modelBuilder.Entity<Gndbank>(entity =>
            {
                entity.ToTable("GNDBank");

                entity.Property(e => e.Address1).IsUnicode(false);

                entity.Property(e => e.Address2).IsUnicode(false);

                entity.Property(e => e.FinancialInstitution)
                    .IsRequired()
                    .HasColumnName("Financial_Institution")
                    .IsUnicode(false);

                entity.Property(e => e.Parish)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VendorN)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NiCnte>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"NI.CNTE\"", "WEBPORTAL");

                entity.HasIndex(e => new { e.ContributionYearMonth, e.Id })
                    .HasName("PK_NI_CNTE")
                    .IsClustered();

                entity.HasIndex(e => new { e.Ereg06, e.Ccen06, e.Cony06, e.Cper06, e.Egie06, e.Ecnb06, e.Ct106, e.Ct206, e.Ct306, e.Ct406, e.Ct506, e.Ct606, e.Egrs06, e.Rcnb06, e.Page06, e.Freq06, e.Ern106, e.Ern206, e.Ern306, e.Ern406, e.Ern506, e.Ern606, e.Wksw06, e.Crie06, e.ContributionYearMonth, e.Actv06, e.Rreg06, e.Rrsf06 })
                    .HasName("IDX_WEBPORTAL_NI.CNTE_PagedActiveEmployer");

                entity.HasIndex(e => new { e.Rreg06, e.Rrsf06, e.Ccen06, e.Cony06, e.Cper06, e.Egie06, e.Ecnb06, e.Ct106, e.Ct206, e.Ct306, e.Ct406, e.Ct506, e.Ct606, e.Egrs06, e.Rcnb06, e.Page06, e.Freq06, e.Ern106, e.Ern206, e.Ern306, e.Ern406, e.Ern506, e.Ern606, e.Wksw06, e.Crie06, e.ContributionYearMonth, e.Actv06, e.Ereg06 })
                    .HasName("IDX_WEBPORTAL_NI.CNTE_ActiveEmployee");

                entity.Property(e => e.Actv06)
                    .IsRequired()
                    .HasColumnName("ACTV06")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ccen06)
                    .HasColumnName("CCEN06")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cony06)
                    .HasColumnName("CONY06")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cper06)
                    .HasColumnName("CPER06")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Crd106)
                    .HasColumnName("CRD106")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crd206)
                    .HasColumnName("CRD206")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crd306)
                    .HasColumnName("CRD306")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crd406)
                    .HasColumnName("CRD406")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crd506)
                    .HasColumnName("CRD506")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crd606)
                    .HasColumnName("CRD606")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crie06)
                    .HasColumnName("CRIE06")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Ct106)
                    .IsRequired()
                    .HasColumnName("CT#106")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ct206)
                    .IsRequired()
                    .HasColumnName("CT#206")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ct306)
                    .IsRequired()
                    .HasColumnName("CT#306")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ct406)
                    .IsRequired()
                    .HasColumnName("CT#406")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ct506)
                    .IsRequired()
                    .HasColumnName("CT#506")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ct606)
                    .IsRequired()
                    .HasColumnName("CT#606")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ecnb06)
                    .HasColumnName("ECNB06")
                    .HasColumnType("numeric(7, 2)");

                entity.Property(e => e.Egie06)
                    .HasColumnName("EGIE06")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Egrs06)
                    .HasColumnName("EGRS06")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Ereg06)
                    .HasColumnName("EREG06")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Ern106)
                    .HasColumnName("ERN106")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern206)
                    .HasColumnName("ERN206")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern306)
                    .HasColumnName("ERN306")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern406)
                    .HasColumnName("ERN406")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern506)
                    .HasColumnName("ERN506")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern606)
                    .HasColumnName("ERN606")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Fill06)
                    .IsRequired()
                    .HasColumnName("FILL06")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Freq06)
                    .IsRequired()
                    .HasColumnName("FREQ06")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Page06)
                    .IsRequired()
                    .HasColumnName("PAGE06")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Rcnb06)
                    .HasColumnName("RCNB06")
                    .HasColumnType("numeric(7, 2)");

                entity.Property(e => e.Rreg06)
                    .HasColumnName("RREG06")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rrsf06)
                    .HasColumnName("RRSF06")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Wksw06)
                    .HasColumnName("WKSW06")
                    .HasColumnType("numeric(2, 0)");
            });

            modelBuilder.Entity<NiCnteStaging>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"NI.CNTE.STAGING\"", "WEBPORTAL");

                entity.HasIndex(e => new { e.ContributionYearMonth, e.Id })
                    .HasName("PK_NI_CNTE_STAGING")
                    .IsClustered();

                entity.HasIndex(e => new { e.Ereg06, e.Ccen06, e.Cony06, e.Cper06, e.Egie06, e.Ecnb06, e.Ct106, e.Ct206, e.Ct306, e.Ct406, e.Ct506, e.Ct606, e.Egrs06, e.Rcnb06, e.Page06, e.Freq06, e.Ern106, e.Ern206, e.Ern306, e.Ern406, e.Ern506, e.Ern606, e.Wksw06, e.Crie06, e.ContributionYearMonth, e.Actv06, e.Rreg06, e.Rrsf06 })
                    .HasName("IDX_WEBPORTAL_NI.CNTE.STAGING_PagedActiveEmployer");

                entity.HasIndex(e => new { e.Rreg06, e.Rrsf06, e.Ccen06, e.Cony06, e.Cper06, e.Egie06, e.Ecnb06, e.Ct106, e.Ct206, e.Ct306, e.Ct406, e.Ct506, e.Ct606, e.Egrs06, e.Rcnb06, e.Page06, e.Freq06, e.Ern106, e.Ern206, e.Ern306, e.Ern406, e.Ern506, e.Ern606, e.Wksw06, e.Crie06, e.ContributionYearMonth, e.Actv06, e.Ereg06 })
                    .HasName("IDX_WEBPORTAL_NI.CNTE.STAGING_ActiveEmployee");

                entity.Property(e => e.Actv06)
                    .IsRequired()
                    .HasColumnName("ACTV06")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ccen06)
                    .HasColumnName("CCEN06")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cony06)
                    .HasColumnName("CONY06")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cper06)
                    .HasColumnName("CPER06")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Crd106)
                    .HasColumnName("CRD106")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crd206)
                    .HasColumnName("CRD206")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crd306)
                    .HasColumnName("CRD306")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crd406)
                    .HasColumnName("CRD406")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crd506)
                    .HasColumnName("CRD506")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crd606)
                    .HasColumnName("CRD606")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crie06)
                    .HasColumnName("CRIE06")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Ct106)
                    .IsRequired()
                    .HasColumnName("CT#106")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ct206)
                    .IsRequired()
                    .HasColumnName("CT#206")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ct306)
                    .IsRequired()
                    .HasColumnName("CT#306")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ct406)
                    .IsRequired()
                    .HasColumnName("CT#406")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ct506)
                    .IsRequired()
                    .HasColumnName("CT#506")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ct606)
                    .IsRequired()
                    .HasColumnName("CT#606")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ecnb06)
                    .HasColumnName("ECNB06")
                    .HasColumnType("numeric(7, 2)");

                entity.Property(e => e.Egie06)
                    .HasColumnName("EGIE06")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Egrs06)
                    .HasColumnName("EGRS06")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Ereg06)
                    .HasColumnName("EREG06")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Ern106)
                    .HasColumnName("ERN106")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern206)
                    .HasColumnName("ERN206")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern306)
                    .HasColumnName("ERN306")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern406)
                    .HasColumnName("ERN406")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern506)
                    .HasColumnName("ERN506")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern606)
                    .HasColumnName("ERN606")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Fill06)
                    .IsRequired()
                    .HasColumnName("FILL06")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Freq06)
                    .IsRequired()
                    .HasColumnName("FREQ06")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Page06)
                    .IsRequired()
                    .HasColumnName("PAGE06")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Rcnb06)
                    .HasColumnName("RCNB06")
                    .HasColumnType("numeric(7, 2)");

                entity.Property(e => e.Rreg06)
                    .HasColumnName("RREG06")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rrsf06)
                    .HasColumnName("RRSF06")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Wksw06)
                    .HasColumnName("WKSW06")
                    .HasColumnType("numeric(2, 0)");
            });

            modelBuilder.Entity<NiCons>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"NI.CONS\"", "WEBPORTAL");

                entity.Property(e => e.Actv01)
                    .IsRequired()
                    .HasColumnName("ACTV01")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Arel01)
                    .HasColumnName("AREL01")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Ckey01)
                    .IsRequired()
                    .HasColumnName("CKEY01")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Data01)
                    .IsRequired()
                    .HasColumnName("DATA01")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Datn01)
                    .HasColumnName("DATN01")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Decm01)
                    .HasColumnName("DECM01")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Flen01)
                    .HasColumnName("FLEN01")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.User01)
                    .IsRequired()
                    .HasColumnName("USER01")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Usrm01)
                    .IsRequired()
                    .HasColumnName("USRM01")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NiEmpe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"NI.EMPE\"", "WEBPORTAL");

                entity.HasIndex(e => new { e.Actv03, e.Ereg03 })
                    .HasName("IDX_WEBPORTAL_NI.EMPE_ActiveEmployee");

                entity.Property(e => e.Actv03)
                    .IsRequired()
                    .HasColumnName("ACTV03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Adcr03)
                    .HasColumnName("ADCR03")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Add103)
                    .IsRequired()
                    .HasColumnName("ADD103")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Add203)
                    .IsRequired()
                    .HasColumnName("ADD203")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Add303)
                    .IsRequired()
                    .HasColumnName("ADD303")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Add403)
                    .IsRequired()
                    .HasColumnName("ADD403")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Add503)
                    .IsRequired()
                    .HasColumnName("ADD503")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Aftd03)
                    .HasColumnName("AFTD03")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Aftn03)
                    .HasColumnName("AFTN03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Aftr03)
                    .IsRequired()
                    .HasColumnName("AFTR03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Btou03)
                    .IsRequired()
                    .HasColumnName("BTOU03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Cenb03)
                    .HasColumnName("CENB03")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cend03)
                    .HasColumnName("CEND03")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Ceni03)
                    .HasColumnName("CENI03")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Crtb03)
                    .IsRequired()
                    .HasColumnName("CRTB03")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Crtd03)
                    .HasColumnName("CRTD03")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Datb03)
                    .HasColumnName("DATB03")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Datd03)
                    .HasColumnName("DATD03")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Dati03)
                    .HasColumnName("DATI03")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Eadd03)
                    .IsRequired()
                    .HasColumnName("EADD03")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Eml103)
                    .IsRequired()
                    .HasColumnName("EML103")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Eml203)
                    .IsRequired()
                    .HasColumnName("EML203")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Epcd03)
                    .IsRequired()
                    .HasColumnName("EPCD03")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ereg03)
                    .HasColumnName("EREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Esex03)
                    .IsRequired()
                    .HasColumnName("ESEX03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Etwn03)
                    .IsRequired()
                    .HasColumnName("ETWN03")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Fill03)
                    .IsRequired()
                    .HasColumnName("FILL03")
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.Fill3a)
                    .IsRequired()
                    .HasColumnName("FILL3A")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Fnam03)
                    .IsRequired()
                    .HasColumnName("FNAM03")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Freg03)
                    .HasColumnName("FREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Incr03)
                    .HasColumnName("INCR03")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Lnam03)
                    .IsRequired()
                    .HasColumnName("LNAM03")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Mnam03)
                    .IsRequired()
                    .HasColumnName("MNAM03")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Mntd03)
                    .HasColumnName("MNTD03")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Mntn03)
                    .IsRequired()
                    .HasColumnName("MNTN03")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Mreg03)
                    .HasColumnName("MREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Mrtl03)
                    .IsRequired()
                    .HasColumnName("MRTL03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Natl03)
                    .IsRequired()
                    .HasColumnName("NATL03")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Nreg03)
                    .HasColumnName("NREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Pds103)
                    .IsRequired()
                    .HasColumnName("PDS103")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Pds203)
                    .IsRequired()
                    .HasColumnName("PDS203")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Phn103)
                    .HasColumnName("PHN103")
                    .HasColumnType("numeric(14, 0)");

                entity.Property(e => e.Phn203)
                    .HasColumnName("PHN203")
                    .HasColumnType("numeric(14, 0)");

                entity.Property(e => e.Plob03)
                    .IsRequired()
                    .HasColumnName("PLOB03")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Prov03)
                    .IsRequired()
                    .HasColumnName("PROV03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Rben03)
                    .IsRequired()
                    .HasColumnName("RBEN03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Regn03)
                    .IsRequired()
                    .HasColumnName("REGN03")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Rrgs03)
                    .HasColumnName("RRGS03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rsfs03)
                    .HasColumnName("RSFS03")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Sefg03)
                    .IsRequired()
                    .HasColumnName("SEFG03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Sreg03)
                    .HasColumnName("SREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Totc03)
                    .HasColumnName("TOTC03")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Treg03)
                    .HasColumnName("TREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Verb03)
                    .IsRequired()
                    .HasColumnName("VERB03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Verm03)
                    .IsRequired()
                    .HasColumnName("VERM03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Vndr03)
                    .IsRequired()
                    .HasColumnName("VNDR03")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NiEmpeStaging>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"NI.EMPE.STAGING\"", "WEBPORTAL");

                entity.HasIndex(e => new { e.Actv03, e.Ereg03 })
                    .HasName("IDX_WEBPORTAL_NI.EMPE_ActiveEmployee");

                entity.Property(e => e.Actv03)
                    .IsRequired()
                    .HasColumnName("ACTV03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Adcr03)
                    .HasColumnName("ADCR03")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Add103)
                    .IsRequired()
                    .HasColumnName("ADD103")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Add203)
                    .IsRequired()
                    .HasColumnName("ADD203")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Add303)
                    .IsRequired()
                    .HasColumnName("ADD303")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Add403)
                    .IsRequired()
                    .HasColumnName("ADD403")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Add503)
                    .IsRequired()
                    .HasColumnName("ADD503")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Aftd03)
                    .HasColumnName("AFTD03")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Aftn03)
                    .HasColumnName("AFTN03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Aftr03)
                    .IsRequired()
                    .HasColumnName("AFTR03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Btou03)
                    .IsRequired()
                    .HasColumnName("BTOU03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Cenb03)
                    .HasColumnName("CENB03")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cend03)
                    .HasColumnName("CEND03")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Ceni03)
                    .HasColumnName("CENI03")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Crtb03)
                    .IsRequired()
                    .HasColumnName("CRTB03")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Crtd03)
                    .HasColumnName("CRTD03")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Datb03)
                    .HasColumnName("DATB03")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Datd03)
                    .HasColumnName("DATD03")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Dati03)
                    .HasColumnName("DATI03")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Eadd03)
                    .IsRequired()
                    .HasColumnName("EADD03")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Eml103)
                    .IsRequired()
                    .HasColumnName("EML103")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Eml203)
                    .IsRequired()
                    .HasColumnName("EML203")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Epcd03)
                    .IsRequired()
                    .HasColumnName("EPCD03")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ereg03)
                    .HasColumnName("EREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Esex03)
                    .IsRequired()
                    .HasColumnName("ESEX03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Etwn03)
                    .IsRequired()
                    .HasColumnName("ETWN03")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Fill03)
                    .IsRequired()
                    .HasColumnName("FILL03")
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.Fill3a)
                    .IsRequired()
                    .HasColumnName("FILL3A")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Fnam03)
                    .IsRequired()
                    .HasColumnName("FNAM03")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Freg03)
                    .HasColumnName("FREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Incr03)
                    .HasColumnName("INCR03")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Lnam03)
                    .IsRequired()
                    .HasColumnName("LNAM03")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Mnam03)
                    .IsRequired()
                    .HasColumnName("MNAM03")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Mntd03)
                    .HasColumnName("MNTD03")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Mntn03)
                    .IsRequired()
                    .HasColumnName("MNTN03")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Mreg03)
                    .HasColumnName("MREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Mrtl03)
                    .IsRequired()
                    .HasColumnName("MRTL03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Natl03)
                    .IsRequired()
                    .HasColumnName("NATL03")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Nreg03)
                    .HasColumnName("NREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Pds103)
                    .IsRequired()
                    .HasColumnName("PDS103")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Pds203)
                    .IsRequired()
                    .HasColumnName("PDS203")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Phn103)
                    .HasColumnName("PHN103")
                    .HasColumnType("numeric(14, 0)");

                entity.Property(e => e.Phn203)
                    .HasColumnName("PHN203")
                    .HasColumnType("numeric(14, 0)");

                entity.Property(e => e.Plob03)
                    .IsRequired()
                    .HasColumnName("PLOB03")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Prov03)
                    .IsRequired()
                    .HasColumnName("PROV03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Rben03)
                    .IsRequired()
                    .HasColumnName("RBEN03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Regn03)
                    .IsRequired()
                    .HasColumnName("REGN03")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Rrgs03)
                    .HasColumnName("RRGS03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rsfs03)
                    .HasColumnName("RSFS03")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Sefg03)
                    .IsRequired()
                    .HasColumnName("SEFG03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Sreg03)
                    .HasColumnName("SREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Totc03)
                    .HasColumnName("TOTC03")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Treg03)
                    .HasColumnName("TREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Verb03)
                    .IsRequired()
                    .HasColumnName("VERB03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Verm03)
                    .IsRequired()
                    .HasColumnName("VERM03")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Vndr03)
                    .IsRequired()
                    .HasColumnName("VNDR03")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NiEmpr>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"NI.EMPR\"", "WEBPORTAL");

                entity.Property(e => e.Actv02)
                    .IsRequired()
                    .HasColumnName("ACTV02")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Age102)
                    .HasColumnName("AGE102")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Age202)
                    .HasColumnName("AGE202")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Age302)
                    .HasColumnName("AGE302")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Age402)
                    .HasColumnName("AGE402")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Age502)
                    .HasColumnName("AGE502")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Badd02)
                    .IsRequired()
                    .HasColumnName("BADD02")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Bnam02)
                    .IsRequired()
                    .HasColumnName("BNAM02")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Bpcd02)
                    .IsRequired()
                    .HasColumnName("BPCD02")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Brgn02)
                    .IsRequired()
                    .HasColumnName("BRGN02")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Btno02)
                    .HasColumnName("BTNO02")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Btwn02)
                    .IsRequired()
                    .HasColumnName("BTWN02")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Btyp02)
                    .HasColumnName("BTYP02")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cadj02)
                    .HasColumnName("CADJ02")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Cchr02)
                    .HasColumnName("CCHR02")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Ceni02)
                    .HasColumnName("CENI02")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cex102)
                    .HasColumnName("CEX102")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Cex202)
                    .HasColumnName("CEX202")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Cint02)
                    .HasColumnName("CINT02")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Cml102)
                    .IsRequired()
                    .HasColumnName("CML102")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Cmm202)
                    .IsRequired()
                    .HasColumnName("CMM202")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Cnm102)
                    .IsRequired()
                    .HasColumnName("CNM102")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Cnm202)
                    .IsRequired()
                    .HasColumnName("CNM202")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Cpay02)
                    .HasColumnName("CPAY02")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Cph102)
                    .HasColumnName("CPH102")
                    .HasColumnType("numeric(14, 0)");

                entity.Property(e => e.Cph202)
                    .HasColumnName("CPH202")
                    .HasColumnType("numeric(14, 0)");

                entity.Property(e => e.Cpnl02)
                    .HasColumnName("CPNL02")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Cpt102)
                    .IsRequired()
                    .HasColumnName("CPT102")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Cpt202)
                    .IsRequired()
                    .HasColumnName("CPT202")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Crdt02)
                    .HasColumnName("CRDT02")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Dati02)
                    .HasColumnName("DATI02")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Dord02)
                    .HasColumnName("DORD02")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Dorm02)
                    .IsRequired()
                    .HasColumnName("DORM02")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Eml102)
                    .IsRequired()
                    .HasColumnName("EML102")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Eml202)
                    .IsRequired()
                    .HasColumnName("EML202")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Faxn02)
                    .HasColumnName("FAXN02")
                    .HasColumnType("numeric(14, 0)");

                entity.Property(e => e.Fill02)
                    .IsRequired()
                    .HasColumnName("FILL02")
                    .HasMaxLength(21)
                    .IsUnicode(false);

                entity.Property(e => e.Fill2a)
                    .IsRequired()
                    .HasColumnName("FILL2A")
                    .HasMaxLength(204)
                    .IsUnicode(false);

                entity.Property(e => e.Fine02)
                    .HasColumnName("FINE02")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Fpay02)
                    .HasColumnName("FPAY02")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Grad02)
                    .IsRequired()
                    .HasColumnName("GRAD02")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Indc02)
                    .HasColumnName("INDC02")
                    .HasColumnType("numeric(5, 0)");

                entity.Property(e => e.Jgdt02)
                    .HasColumnName("JGDT02")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Jgmt02)
                    .IsRequired()
                    .HasColumnName("JGMT02")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Main02)
                    .IsRequired()
                    .HasColumnName("MAIN02")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Marr02)
                    .HasColumnName("MARR02")
                    .HasColumnType("decimal(3, 0)");

                entity.Property(e => e.Mndt02)
                    .HasColumnName("MNDT02")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Nofe02)
                    .HasColumnName("NOFE02")
                    .HasColumnType("numeric(4, 0)");

                entity.Property(e => e.Nome02)
                    .HasColumnName("NOME02")
                    .HasColumnType("numeric(4, 0)");

                entity.Property(e => e.Offr02)
                    .IsRequired()
                    .HasColumnName("OFFR02")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Pds102)
                    .IsRequired()
                    .HasColumnName("PDS102")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Pds202)
                    .IsRequired()
                    .HasColumnName("PDS202")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Phn102)
                    .HasColumnName("PHN102")
                    .HasColumnType("numeric(14, 0)");

                entity.Property(e => e.Phn202)
                    .HasColumnName("PHN202")
                    .HasColumnType("numeric(14, 0)");

                entity.Property(e => e.Radd02)
                    .IsRequired()
                    .HasColumnName("RADD02")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Rnam02)
                    .IsRequired()
                    .HasColumnName("RNAM02")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Rpcd02)
                    .IsRequired()
                    .HasColumnName("RPCD02")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Rreg02)
                    .HasColumnName("RREG02")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rrgn02)
                    .IsRequired()
                    .HasColumnName("RRGN02")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Rrsf02)
                    .HasColumnName("RRSF02")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Rtno02)
                    .HasColumnName("RTNO02")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Rtwn02)
                    .IsRequired()
                    .HasColumnName("RTWN02")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Sect02)
                    .HasColumnName("SECT02")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Stdt02)
                    .HasColumnName("STDT02")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Stmd02)
                    .HasColumnName("STMD02")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Stmt02)
                    .IsRequired()
                    .HasColumnName("STMT02")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Strp02)
                    .IsRequired()
                    .HasColumnName("STRP02")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Tcen02)
                    .HasColumnName("TCEN02")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Tdat02)
                    .HasColumnName("TDAT02")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Totl02)
                    .HasColumnName("TOTL02")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.User02)
                    .IsRequired()
                    .HasColumnName("USER02")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Zone02)
                    .HasColumnName("ZONE02")
                    .HasColumnType("numeric(3, 0)");
            });

            modelBuilder.Entity<NiEmprStaging>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"NI.EMPR.STAGING\"", "WEBPORTAL");

                entity.Property(e => e.Actv02)
                    .IsRequired()
                    .HasColumnName("ACTV02")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Age102)
                    .HasColumnName("AGE102")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Age202)
                    .HasColumnName("AGE202")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Age302)
                    .HasColumnName("AGE302")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Age402)
                    .HasColumnName("AGE402")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Age502)
                    .HasColumnName("AGE502")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Badd02)
                    .IsRequired()
                    .HasColumnName("BADD02")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Bnam02)
                    .IsRequired()
                    .HasColumnName("BNAM02")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Bpcd02)
                    .IsRequired()
                    .HasColumnName("BPCD02")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Brgn02)
                    .IsRequired()
                    .HasColumnName("BRGN02")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Btno02)
                    .HasColumnName("BTNO02")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Btwn02)
                    .IsRequired()
                    .HasColumnName("BTWN02")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Btyp02)
                    .HasColumnName("BTYP02")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cadj02)
                    .HasColumnName("CADJ02")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Cchr02)
                    .HasColumnName("CCHR02")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Ceni02)
                    .HasColumnName("CENI02")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cex102)
                    .HasColumnName("CEX102")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Cex202)
                    .HasColumnName("CEX202")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Cint02)
                    .HasColumnName("CINT02")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Cml102)
                    .IsRequired()
                    .HasColumnName("CML102")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Cmm202)
                    .IsRequired()
                    .HasColumnName("CMM202")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Cnm102)
                    .IsRequired()
                    .HasColumnName("CNM102")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Cnm202)
                    .IsRequired()
                    .HasColumnName("CNM202")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Cpay02)
                    .HasColumnName("CPAY02")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Cph102)
                    .HasColumnName("CPH102")
                    .HasColumnType("numeric(14, 0)");

                entity.Property(e => e.Cph202)
                    .HasColumnName("CPH202")
                    .HasColumnType("numeric(14, 0)");

                entity.Property(e => e.Cpnl02)
                    .HasColumnName("CPNL02")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Cpt102)
                    .IsRequired()
                    .HasColumnName("CPT102")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Cpt202)
                    .IsRequired()
                    .HasColumnName("CPT202")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Crdt02)
                    .HasColumnName("CRDT02")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Dati02)
                    .HasColumnName("DATI02")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Dord02)
                    .HasColumnName("DORD02")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Dorm02)
                    .IsRequired()
                    .HasColumnName("DORM02")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Eml102)
                    .IsRequired()
                    .HasColumnName("EML102")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Eml202)
                    .IsRequired()
                    .HasColumnName("EML202")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Faxn02)
                    .HasColumnName("FAXN02")
                    .HasColumnType("numeric(14, 0)");

                entity.Property(e => e.Fill02)
                    .IsRequired()
                    .HasColumnName("FILL02")
                    .HasMaxLength(21)
                    .IsUnicode(false);

                entity.Property(e => e.Fill2a)
                    .IsRequired()
                    .HasColumnName("FILL2A")
                    .HasMaxLength(204)
                    .IsUnicode(false);

                entity.Property(e => e.Fine02)
                    .HasColumnName("FINE02")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Fpay02)
                    .HasColumnName("FPAY02")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Grad02)
                    .IsRequired()
                    .HasColumnName("GRAD02")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Indc02)
                    .HasColumnName("INDC02")
                    .HasColumnType("numeric(5, 0)");

                entity.Property(e => e.Jgdt02)
                    .HasColumnName("JGDT02")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Jgmt02)
                    .IsRequired()
                    .HasColumnName("JGMT02")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Main02)
                    .IsRequired()
                    .HasColumnName("MAIN02")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Marr02)
                    .HasColumnName("MARR02")
                    .HasColumnType("decimal(3, 0)");

                entity.Property(e => e.Mndt02)
                    .HasColumnName("MNDT02")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Nofe02)
                    .HasColumnName("NOFE02")
                    .HasColumnType("numeric(4, 0)");

                entity.Property(e => e.Nome02)
                    .HasColumnName("NOME02")
                    .HasColumnType("numeric(4, 0)");

                entity.Property(e => e.Offr02)
                    .IsRequired()
                    .HasColumnName("OFFR02")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Pds102)
                    .IsRequired()
                    .HasColumnName("PDS102")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Pds202)
                    .IsRequired()
                    .HasColumnName("PDS202")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Phn102)
                    .HasColumnName("PHN102")
                    .HasColumnType("numeric(14, 0)");

                entity.Property(e => e.Phn202)
                    .HasColumnName("PHN202")
                    .HasColumnType("numeric(14, 0)");

                entity.Property(e => e.Radd02)
                    .IsRequired()
                    .HasColumnName("RADD02")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Rnam02)
                    .IsRequired()
                    .HasColumnName("RNAM02")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Rpcd02)
                    .IsRequired()
                    .HasColumnName("RPCD02")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Rreg02)
                    .HasColumnName("RREG02")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rrgn02)
                    .IsRequired()
                    .HasColumnName("RRGN02")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Rrsf02)
                    .HasColumnName("RRSF02")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Rtno02)
                    .HasColumnName("RTNO02")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Rtwn02)
                    .IsRequired()
                    .HasColumnName("RTWN02")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Sect02)
                    .HasColumnName("SECT02")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Stdt02)
                    .HasColumnName("STDT02")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Stmd02)
                    .HasColumnName("STMD02")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Stmt02)
                    .IsRequired()
                    .HasColumnName("STMT02")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Strp02)
                    .IsRequired()
                    .HasColumnName("STRP02")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Tcen02)
                    .HasColumnName("TCEN02")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Tdat02)
                    .HasColumnName("TDAT02")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Totl02)
                    .HasColumnName("TOTL02")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.User02)
                    .IsRequired()
                    .HasColumnName("USER02")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Zone02)
                    .HasColumnName("ZONE02")
                    .HasColumnType("numeric(3, 0)");
            });

            modelBuilder.Entity<NiInspt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"NI.INSPT\"", "WEBPORTAL");

                entity.Property(e => e.Actvcd)
                    .IsRequired()
                    .HasColumnName("ACTVCD")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Idalog)
                    .IsRequired()
                    .HasColumnName("IDALOG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Idolog)
                    .IsRequired()
                    .HasColumnName("IDOLOG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Imalog)
                    .IsRequired()
                    .HasColumnName("IMALOG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Imolog)
                    .IsRequired()
                    .HasColumnName("IMOLOG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Inname)
                    .IsRequired()
                    .HasColumnName("INNAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Inspas)
                    .IsRequired()
                    .HasColumnName("INSPAS")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Inspno)
                    .IsRequired()
                    .HasColumnName("INSPNO")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Inssec)
                    .IsRequired()
                    .HasColumnName("INSSEC")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Ipalog)
                    .IsRequired()
                    .HasColumnName("IPALOG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ipolog)
                    .IsRequired()
                    .HasColumnName("IPOLOG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ixalog)
                    .IsRequired()
                    .HasColumnName("IXALOG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ixolog)
                    .IsRequired()
                    .HasColumnName("IXOLOG")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NiXport>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"NI.XPORT\"", "WEBPORTAL");

                entity.Property(e => e.Cardprint).HasColumnName("CARDPRINT");

                entity.Property(e => e.Dob)
                    .IsRequired()
                    .HasColumnName("DOB")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("FNAME")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Issued)
                    .IsRequired()
                    .HasColumnName("ISSUED")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Lastmod)
                    .IsRequired()
                    .HasColumnName("LASTMOD")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasColumnName("LNAME")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Natl)
                    .IsRequired()
                    .HasColumnName("NATL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nisnum)
                    .IsRequired()
                    .HasColumnName("NISNUM")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasColumnName("PHOTO")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Plob)
                    .IsRequired()
                    .HasColumnName("PLOB")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasColumnName("SEX")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Signature)
                    .IsRequired()
                    .HasColumnName("SIGNATURE")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QVend>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"Q.VEND\"", "WEBPORTAL");

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasColumnName("CLASS")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Fill)
                    .IsRequired()
                    .HasColumnName("FILL")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Fill1)
                    .IsRequired()
                    .HasColumnName("FILL1")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Fill2)
                    .IsRequired()
                    .HasColumnName("FILL2")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Fill3)
                    .IsRequired()
                    .HasColumnName("FILL3")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Vadr1)
                    .IsRequired()
                    .HasColumnName("VADR1")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Vadr2)
                    .IsRequired()
                    .HasColumnName("VADR2")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Vadr3)
                    .IsRequired()
                    .HasColumnName("VADR3")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Vcurr)
                    .IsRequired()
                    .HasColumnName("VCURR")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Vdsda)
                    .HasColumnName("VDSDA")
                    .HasColumnType("decimal(3, 0)");

                entity.Property(e => e.Vdspc)
                    .HasColumnName("VDSPC")
                    .HasColumnType("decimal(3, 1)");

                entity.Property(e => e.Vend)
                    .IsRequired()
                    .HasColumnName("VEND")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Vendx)
                    .IsRequired()
                    .HasColumnName("VENDX")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Vlcdt)
                    .HasColumnName("VLCDT")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Vname)
                    .IsRequired()
                    .HasColumnName("VNAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Voytd)
                    .HasColumnName("VOYTD")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Vpost)
                    .IsRequired()
                    .HasColumnName("VPOST")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Vprt)
                    .IsRequired()
                    .HasColumnName("VPRT")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Vpytd)
                    .HasColumnName("VPYTD")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Vspos)
                    .HasColumnName("VSPOS")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Vtel)
                    .HasColumnName("VTEL")
                    .HasColumnType("decimal(10, 0)");
            });

            modelBuilder.Entity<Rates>(entity =>
            {
                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.Sector)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.Property(e => e.Value).HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<RatesDetails>(entity =>
            {
                entity.Property(e => e.Employee).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Employer).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.Sector)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.Property(e => e.Value).HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<TtAritm>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"TT.ARITM\"", "WEBPORTAL");

                entity.Property(e => e.Adj103)
                    .HasColumnName("ADJ103")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Cnp103)
                    .HasColumnName("CNP103")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Cnt103)
                    .HasColumnName("CNT103")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Ern103)
                    .HasColumnName("ERN103")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Est103)
                    .IsRequired()
                    .HasColumnName("EST103")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Fin103)
                    .HasColumnName("FIN103")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Int103)
                    .HasColumnName("INT103")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Pen103)
                    .HasColumnName("PEN103")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Pmt103)
                    .HasColumnName("PMT103")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Prd103)
                    .HasColumnName("PRD103")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Reg103)
                    .HasColumnName("REG103")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rsf103)
                    .HasColumnName("RSF103")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Seq103)
                    .HasColumnName("SEQ103")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Sur103)
                    .HasColumnName("SUR103")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Txd103)
                    .HasColumnName("TXD103")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Typ103)
                    .IsRequired()
                    .HasColumnName("TYP103")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");
            });

            modelBuilder.Entity<TtArop>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"TT.AROP\"", "WEBPORTAL");

                entity.Property(e => e.Actv12)
                    .IsRequired()
                    .HasColumnName("ACTV12")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Adjm12)
                    .HasColumnName("ADJM12")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Ccen12)
                    .HasColumnName("CCEN12")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cntr12)
                    .HasColumnName("CNTR12")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Cnyr12)
                    .HasColumnName("CNYR12")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cper12)
                    .HasColumnName("CPER12")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Fill12)
                    .IsRequired()
                    .HasColumnName("FILL12")
                    .HasMaxLength(66)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Fine12)
                    .HasColumnName("FINE12")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Intr12)
                    .HasColumnName("INTR12")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Mtot12)
                    .HasColumnName("MTOT12")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Paym12)
                    .HasColumnName("PAYM12")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Penl12)
                    .HasColumnName("PENL12")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Rreg12)
                    .HasColumnName("RREG12")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rrsf12)
                    .HasColumnName("RRSF12")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Subc12)
                    .HasColumnName("SUBC12")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Subd12)
                    .HasColumnName("SUBD12")
                    .HasColumnType("numeric(6, 0)");
            });

            modelBuilder.Entity<TtArpmt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"TT.ARPMT\"", "WEBPORTAL");

                entity.Property(e => e.Adj101)
                    .HasColumnName("ADJ101")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Aut101)
                    .IsRequired()
                    .HasColumnName("AUT101")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Dsb101)
                    .IsRequired()
                    .HasColumnName("DSB101")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Dsd101)
                    .HasColumnName("DSD101")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Dst101)
                    .IsRequired()
                    .HasColumnName("DST101")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Fin101)
                    .HasColumnName("FIN101")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Int101)
                    .HasColumnName("INT101")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Orb101)
                    .IsRequired()
                    .HasColumnName("ORB101")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ord101)
                    .HasColumnName("ORD101")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Oro101)
                    .HasColumnName("ORO101")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Ort101)
                    .HasColumnName("ORT101")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Pen101)
                    .HasColumnName("PEN101")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Pmt101)
                    .HasColumnName("PMT101")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Prd101)
                    .HasColumnName("PRD101")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Psb101)
                    .IsRequired()
                    .HasColumnName("PSB101")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Psd101)
                    .HasColumnName("PSD101")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Pst101)
                    .IsRequired()
                    .HasColumnName("PST101")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Rct101)
                    .HasColumnName("RCT101")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Reg101)
                    .HasColumnName("REG101")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rsf101)
                    .HasColumnName("RSF101")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Seq101)
                    .HasColumnName("SEQ101")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Sur101)
                    .HasColumnName("SUR101")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Txd101)
                    .HasColumnName("TXD101")
                    .HasColumnType("numeric(8, 0)");
            });

            modelBuilder.Entity<TtArsum>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"TT.ARSUM\"", "WEBPORTAL");

                entity.Property(e => e.Aci104)
                    .HasColumnName("ACI104")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Adj104)
                    .HasColumnName("ADJ104")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Bal104)
                    .HasColumnName("BAL104")
                    .HasColumnType("numeric(11, 2)");

                entity.Property(e => e.Cnt104)
                    .HasColumnName("CNT104")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Ern104)
                    .HasColumnName("ERN104")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Ese104)
                    .HasColumnName("ESE104")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Esp104)
                    .HasColumnName("ESP104")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Fin104)
                    .HasColumnName("FIN104")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Fpd104)
                    .HasColumnName("FPD104")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Int104)
                    .HasColumnName("INT104")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Lpd104)
                    .HasColumnName("LPD104")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Pen104)
                    .HasColumnName("PEN104")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Pit104)
                    .HasColumnName("PIT104")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Pmt104)
                    .HasColumnName("PMT104")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Prd104)
                    .HasColumnName("PRD104")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Reg104)
                    .HasColumnName("REG104")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rsf104)
                    .HasColumnName("RSF104")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Sur104)
                    .HasColumnName("SUR104")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Tot104)
                    .HasColumnName("TOT104")
                    .HasColumnType("numeric(11, 2)");
            });

            modelBuilder.Entity<TtCash>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"TT.CASH\"", "WEBPORTAL");

                entity.Property(e => e.Actv16)
                    .IsRequired()
                    .HasColumnName("ACTV16")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Cheq16)
                    .HasColumnName("CHEQ16")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Chng16)
                    .HasColumnName("CHNG16")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Code16)
                    .HasColumnName("CODE16")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Crf116)
                    .IsRequired()
                    .HasColumnName("CRF116")
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Crf216)
                    .IsRequired()
                    .HasColumnName("CRF216")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Curr16)
                    .IsRequired()
                    .HasColumnName("CURR16")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Date16)
                    .HasColumnName("DATE16")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Dsct16)
                    .HasColumnName("DSCT16")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Fcyr16)
                    .HasColumnName("FCYR16")
                    .HasColumnType("numeric(4, 0)");

                entity.Property(e => e.Fmon16)
                    .HasColumnName("FMON16")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Init16)
                    .IsRequired()
                    .HasColumnName("INIT16")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Paid16)
                    .HasColumnName("PAID16")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Rcpt16)
                    .HasColumnName("RCPT16")
                    .HasColumnType("decimal(9, 0)");

                entity.Property(e => e.Rreg16)
                    .HasColumnName("RREG16")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rrsf16)
                    .HasColumnName("RRSF16")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Rtyp16)
                    .HasColumnName("RTYP16")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Time16)
                    .HasColumnName("TIME16")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Totl16)
                    .HasColumnName("TOTL16")
                    .HasColumnType("decimal(13, 2)");
            });

            modelBuilder.Entity<TtChst>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"TT.CHST\"", "WEBPORTAL");

                entity.Property(e => e.Actv24)
                    .IsRequired()
                    .HasColumnName("ACTV24")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Cheq24)
                    .HasColumnName("CHEQ24")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Chng24)
                    .HasColumnName("CHNG24")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Code24)
                    .HasColumnName("CODE24")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Crf124)
                    .IsRequired()
                    .HasColumnName("CRF124")
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Crf224)
                    .IsRequired()
                    .HasColumnName("CRF224")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Curr24)
                    .IsRequired()
                    .HasColumnName("CURR24")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Date24)
                    .HasColumnName("DATE24")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Dsct24)
                    .HasColumnName("DSCT24")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Fcyr24)
                    .HasColumnName("FCYR24")
                    .HasColumnType("numeric(4, 0)");

                entity.Property(e => e.Fmon24)
                    .HasColumnName("FMON24")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Init24)
                    .IsRequired()
                    .HasColumnName("INIT24")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Paid24)
                    .HasColumnName("PAID24")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Rcpt24)
                    .HasColumnName("RCPT24")
                    .HasColumnType("decimal(9, 0)");

                entity.Property(e => e.Rreg24)
                    .HasColumnName("RREG24")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rrsf24)
                    .HasColumnName("RRSF24")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Rtyp24)
                    .HasColumnName("RTYP24")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Time24)
                    .HasColumnName("TIME24")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Totl24)
                    .HasColumnName("TOTL24")
                    .HasColumnType("decimal(13, 2)");
            });

            modelBuilder.Entity<TtCnte>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"TT.CNTE\"", "WEBPORTAL");

                entity.Property(e => e.Actv06)
                    .IsRequired()
                    .HasColumnName("ACTV06")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ccen06)
                    .HasColumnName("CCEN06")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cony06)
                    .HasColumnName("CONY06")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cper06)
                    .HasColumnName("CPER06")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Crd106)
                    .HasColumnName("CRD106")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crd206)
                    .HasColumnName("CRD206")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crd306)
                    .HasColumnName("CRD306")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crd406)
                    .HasColumnName("CRD406")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crd506)
                    .HasColumnName("CRD506")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crd606)
                    .HasColumnName("CRD606")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Crie06)
                    .HasColumnName("CRIE06")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Ct106)
                    .IsRequired()
                    .HasColumnName("CT#106")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ct206)
                    .IsRequired()
                    .HasColumnName("CT#206")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ct306)
                    .IsRequired()
                    .HasColumnName("CT#306")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ct406)
                    .IsRequired()
                    .HasColumnName("CT#406")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ct506)
                    .IsRequired()
                    .HasColumnName("CT#506")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ct606)
                    .IsRequired()
                    .HasColumnName("CT#606")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ecnb06)
                    .HasColumnName("ECNB06")
                    .HasColumnType("numeric(7, 2)");

                entity.Property(e => e.Egie06)
                    .HasColumnName("EGIE06")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Egrs06)
                    .HasColumnName("EGRS06")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Ereg06)
                    .HasColumnName("EREG06")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Ern106)
                    .HasColumnName("ERN106")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern206)
                    .HasColumnName("ERN206")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern306)
                    .HasColumnName("ERN306")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern406)
                    .HasColumnName("ERN406")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern506)
                    .HasColumnName("ERN506")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern606)
                    .HasColumnName("ERN606")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Fill06)
                    .IsRequired()
                    .HasColumnName("FILL06")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Freq06)
                    .IsRequired()
                    .HasColumnName("FREQ06")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Page06)
                    .IsRequired()
                    .HasColumnName("PAGE06")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Rcnb06)
                    .HasColumnName("RCNB06")
                    .HasColumnType("numeric(7, 2)");

                entity.Property(e => e.Rreg06)
                    .HasColumnName("RREG06")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rrsf06)
                    .HasColumnName("RRSF06")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Wksw06)
                    .HasColumnName("WKSW06")
                    .HasColumnType("numeric(2, 0)");
            });

            modelBuilder.Entity<TtCntr>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"TT.CNTR\"", "WEBPORTAL");

                entity.Property(e => e.Actv07)
                    .IsRequired()
                    .HasColumnName("ACTV07")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ampd07)
                    .HasColumnName("AMPD07")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Ccen07)
                    .HasColumnName("CCEN07")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Conm07)
                    .HasColumnName("CONM07")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cony07)
                    .HasColumnName("CONY07")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Ecnt07)
                    .HasColumnName("ECNT07")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Fill07)
                    .IsRequired()
                    .HasColumnName("FILL07")
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Fine07)
                    .HasColumnName("FINE07")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Iwk107)
                    .HasColumnName("IWK107")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Iwk207)
                    .HasColumnName("IWK207")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Iwk307)
                    .HasColumnName("IWK307")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Iwk407)
                    .HasColumnName("IWK407")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Iwk507)
                    .HasColumnName("IWK507")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Iwk607)
                    .HasColumnName("IWK607")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Payc07)
                    .HasColumnName("PAYC07")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Payd07)
                    .HasColumnName("PAYD07")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Rbal07)
                    .HasColumnName("RBAL07")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Rcnb07)
                    .HasColumnName("RCNB07")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Recn07)
                    .HasColumnName("RECN07")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Rgie07)
                    .HasColumnName("RGIE07")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Rgrs07)
                    .HasColumnName("RGRS07")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Rint07)
                    .HasColumnName("RINT07")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Rreg07)
                    .HasColumnName("RREG07")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rrsf07)
                    .HasColumnName("RRSF07")
                    .HasColumnType("numeric(3, 0)");
            });

            modelBuilder.Entity<TtCnts>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"TT.CNTS\"", "WEBPORTAL");

                entity.Property(e => e.Actv08)
                    .IsRequired()
                    .HasColumnName("ACTV08")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ccen08)
                    .HasColumnName("CCEN08")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cony08)
                    .HasColumnName("CONY08")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Ereg08)
                    .HasColumnName("EREG08")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Etcc08)
                    .HasColumnName("ETCC08")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Etpc08)
                    .HasColumnName("ETPC08")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Fila08)
                    .IsRequired()
                    .HasColumnName("FILA08")
                    .HasMaxLength(29)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Fill08)
                    .IsRequired()
                    .HasColumnName("FILL08")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Gtpc08)
                    .HasColumnName("GTPC08")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Pcon08)
                    .HasColumnName("PCON08")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Rcon08)
                    .HasColumnName("RCON08")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.We0108)
                    .HasColumnName("WE0108")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We0208)
                    .HasColumnName("WE0208")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We0308)
                    .HasColumnName("WE0308")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We0408)
                    .HasColumnName("WE0408")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We0508)
                    .HasColumnName("WE0508")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We0608)
                    .HasColumnName("WE0608")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We0708)
                    .HasColumnName("WE0708")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We0808)
                    .HasColumnName("WE0808")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We0908)
                    .HasColumnName("WE0908")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We1008)
                    .HasColumnName("WE1008")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We1108)
                    .HasColumnName("WE1108")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We1208)
                    .HasColumnName("WE1208")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We1308)
                    .HasColumnName("WE1308")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We1408)
                    .HasColumnName("WE1408")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We1508)
                    .HasColumnName("WE1508")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We1608)
                    .HasColumnName("WE1608")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We1708)
                    .HasColumnName("WE1708")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We1808)
                    .HasColumnName("WE1808")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We1908)
                    .HasColumnName("WE1908")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We2008)
                    .HasColumnName("WE2008")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We2108)
                    .HasColumnName("WE2108")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We2208)
                    .HasColumnName("WE2208")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We2308)
                    .HasColumnName("WE2308")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We2408)
                    .HasColumnName("WE2408")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We2508)
                    .HasColumnName("WE2508")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We2608)
                    .HasColumnName("WE2608")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We2708)
                    .HasColumnName("WE2708")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We2808)
                    .HasColumnName("WE2808")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We2908)
                    .HasColumnName("WE2908")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We3008)
                    .HasColumnName("WE3008")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We3108)
                    .HasColumnName("WE3108")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We3208)
                    .HasColumnName("WE3208")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We3308)
                    .HasColumnName("WE3308")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We3408)
                    .HasColumnName("WE3408")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We3508)
                    .HasColumnName("WE3508")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We3608)
                    .HasColumnName("WE3608")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We3708)
                    .HasColumnName("WE3708")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We3808)
                    .HasColumnName("WE3808")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We3908)
                    .HasColumnName("WE3908")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We4008)
                    .HasColumnName("WE4008")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We4108)
                    .HasColumnName("WE4108")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We4208)
                    .HasColumnName("WE4208")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We4308)
                    .HasColumnName("WE4308")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We4408)
                    .HasColumnName("WE4408")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We4508)
                    .HasColumnName("WE4508")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We4608)
                    .HasColumnName("WE4608")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We4708)
                    .HasColumnName("WE4708")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We4808)
                    .HasColumnName("WE4808")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We4908)
                    .HasColumnName("WE4908")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We5008)
                    .HasColumnName("WE5008")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We5108)
                    .HasColumnName("WE5108")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We5208)
                    .HasColumnName("WE5208")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.We5508)
                    .HasColumnName("WE5508")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Wfcc08)
                    .HasColumnName("WFCC08")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Wfpc08)
                    .HasColumnName("WFPC08")
                    .HasColumnType("numeric(2, 0)");
            });

            modelBuilder.Entity<TtConh>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"TT.CONH\"", "WEBPORTAL");

                entity.Property(e => e.Actv27)
                    .IsRequired()
                    .HasColumnName("ACTV27")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ccen27)
                    .HasColumnName("CCEN27")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cony27)
                    .HasColumnName("CONY27")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Em0127)
                    .HasColumnName("EM0127")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Em0227)
                    .HasColumnName("EM0227")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Em0327)
                    .HasColumnName("EM0327")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Em0427)
                    .HasColumnName("EM0427")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Em0527)
                    .HasColumnName("EM0527")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Em0627)
                    .HasColumnName("EM0627")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Em0727)
                    .HasColumnName("EM0727")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Em0827)
                    .HasColumnName("EM0827")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Em0927)
                    .HasColumnName("EM0927")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Em1027)
                    .HasColumnName("EM1027")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Em1127)
                    .HasColumnName("EM1127")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Em1227)
                    .HasColumnName("EM1227")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Fill27)
                    .IsRequired()
                    .HasColumnName("FILL27")
                    .HasMaxLength(26)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Freq27)
                    .IsRequired()
                    .HasColumnName("FREQ27")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Lmo27)
                    .HasColumnName("LMO#27")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Lwk27)
                    .HasColumnName("LWK#27")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Rreg27)
                    .HasColumnName("RREG27")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rrsf27)
                    .HasColumnName("RRSF27")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Wk0127)
                    .HasColumnName("WK0127")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Wk0227)
                    .HasColumnName("WK0227")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Wk0327)
                    .HasColumnName("WK0327")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Wk0427)
                    .HasColumnName("WK0427")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Wk0527)
                    .HasColumnName("WK0527")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Wk0627)
                    .HasColumnName("WK0627")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Wk0727)
                    .HasColumnName("WK0727")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Wk0827)
                    .HasColumnName("WK0827")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Wk0927)
                    .HasColumnName("WK0927")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Wk1027)
                    .HasColumnName("WK1027")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Wk1127)
                    .HasColumnName("WK1127")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Wk1227)
                    .HasColumnName("WK1227")
                    .HasColumnType("numeric(1, 0)");
            });

            modelBuilder.Entity<TtEcwe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"TT.ECWE\"", "WEBPORTAL");

                entity.Property(e => e.Actv09)
                    .IsRequired()
                    .HasColumnName("ACTV09")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ccen09)
                    .HasColumnName("CCEN09")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Conm09)
                    .HasColumnName("CONM09")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cony09)
                    .HasColumnName("CONY09")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Ecnb09)
                    .HasColumnName("ECNB09")
                    .HasColumnType("numeric(7, 2)");

                entity.Property(e => e.Egie09)
                    .HasColumnName("EGIE09")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Ereg09)
                    .HasColumnName("EREG09")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Ern109)
                    .HasColumnName("ERN109")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern209)
                    .HasColumnName("ERN209")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern309)
                    .HasColumnName("ERN309")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern409)
                    .HasColumnName("ERN409")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern509)
                    .HasColumnName("ERN509")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern609)
                    .HasColumnName("ERN609")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Fill09)
                    .IsRequired()
                    .HasColumnName("FILL09")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Freq09)
                    .IsRequired()
                    .HasColumnName("FREQ09")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Lin09)
                    .HasColumnName("LIN#09")
                    .HasColumnType("numeric(5, 0)");

                entity.Property(e => e.Page09)
                    .IsRequired()
                    .HasColumnName("PAGE09")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Post09)
                    .IsRequired()
                    .HasColumnName("POST09")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Pwk109)
                    .IsRequired()
                    .HasColumnName("PWK109")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Pwk209)
                    .IsRequired()
                    .HasColumnName("PWK209")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Pwk309)
                    .IsRequired()
                    .HasColumnName("PWK309")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Pwk409)
                    .IsRequired()
                    .HasColumnName("PWK409")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Pwk509)
                    .IsRequired()
                    .HasColumnName("PWK509")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Pwk609)
                    .IsRequired()
                    .HasColumnName("PWK609")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Rreg09)
                    .HasColumnName("RREG09")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rrsf09)
                    .HasColumnName("RRSF09")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.User09)
                    .IsRequired()
                    .HasColumnName("USER09")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Wksw09)
                    .HasColumnName("WKSW09")
                    .HasColumnType("numeric(2, 0)");
            });

            modelBuilder.Entity<TtEcxe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"TT.ECXE\"", "WEBPORTAL");

                entity.Property(e => e.Actv09)
                    .IsRequired()
                    .HasColumnName("ACTV09")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ccen09)
                    .HasColumnName("CCEN09")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Conm09)
                    .HasColumnName("CONM09")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cony09)
                    .HasColumnName("CONY09")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Ecnb09)
                    .HasColumnName("ECNB09")
                    .HasColumnType("numeric(7, 2)");

                entity.Property(e => e.Egie09)
                    .HasColumnName("EGIE09")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Ereg09)
                    .HasColumnName("EREG09")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Ern109)
                    .HasColumnName("ERN109")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern209)
                    .HasColumnName("ERN209")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern309)
                    .HasColumnName("ERN309")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern409)
                    .HasColumnName("ERN409")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern509)
                    .HasColumnName("ERN509")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Ern609)
                    .HasColumnName("ERN609")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Fill09)
                    .IsRequired()
                    .HasColumnName("FILL09")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Freq09)
                    .IsRequired()
                    .HasColumnName("FREQ09")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Lin09)
                    .HasColumnName("LIN#09")
                    .HasColumnType("numeric(5, 0)");

                entity.Property(e => e.Page09)
                    .IsRequired()
                    .HasColumnName("PAGE09")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Post09)
                    .IsRequired()
                    .HasColumnName("POST09")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Pwk109)
                    .IsRequired()
                    .HasColumnName("PWK109")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Pwk209)
                    .IsRequired()
                    .HasColumnName("PWK209")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Pwk309)
                    .IsRequired()
                    .HasColumnName("PWK309")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Pwk409)
                    .IsRequired()
                    .HasColumnName("PWK409")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Pwk509)
                    .IsRequired()
                    .HasColumnName("PWK509")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Pwk609)
                    .IsRequired()
                    .HasColumnName("PWK609")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Rreg09)
                    .HasColumnName("RREG09")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rrsf09)
                    .HasColumnName("RRSF09")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.User09)
                    .IsRequired()
                    .HasColumnName("USER09")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Wksw09)
                    .HasColumnName("WKSW09")
                    .HasColumnType("numeric(2, 0)");
            });

            modelBuilder.Entity<TtEmpe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"TT.EMPE\"", "WEBPORTAL");

                entity.Property(e => e.Actv03)
                    .IsRequired()
                    .HasColumnName("ACTV03")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Adcr03)
                    .HasColumnName("ADCR03")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Add103)
                    .IsRequired()
                    .HasColumnName("ADD103")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Add203)
                    .IsRequired()
                    .HasColumnName("ADD203")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Add303)
                    .IsRequired()
                    .HasColumnName("ADD303")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Add403)
                    .IsRequired()
                    .HasColumnName("ADD403")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Add503)
                    .IsRequired()
                    .HasColumnName("ADD503")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Aftd03)
                    .HasColumnName("AFTD03")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Aftn03)
                    .HasColumnName("AFTN03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Aftr03)
                    .IsRequired()
                    .HasColumnName("AFTR03")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Btou03)
                    .IsRequired()
                    .HasColumnName("BTOU03")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Cenb03)
                    .HasColumnName("CENB03")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cend03)
                    .HasColumnName("CEND03")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Ceni03)
                    .HasColumnName("CENI03")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Crtb03)
                    .IsRequired()
                    .HasColumnName("CRTB03")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Crtd03)
                    .HasColumnName("CRTD03")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Datb03)
                    .HasColumnName("DATB03")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Datd03)
                    .HasColumnName("DATD03")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Dati03)
                    .HasColumnName("DATI03")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Eadd03)
                    .IsRequired()
                    .HasColumnName("EADD03")
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Eml103)
                    .IsRequired()
                    .HasColumnName("EML103")
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Eml203)
                    .IsRequired()
                    .HasColumnName("EML203")
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Epcd03)
                    .IsRequired()
                    .HasColumnName("EPCD03")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ereg03)
                    .HasColumnName("EREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Esex03)
                    .IsRequired()
                    .HasColumnName("ESEX03")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Etwn03)
                    .IsRequired()
                    .HasColumnName("ETWN03")
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Fill03)
                    .IsRequired()
                    .HasColumnName("FILL03")
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Fill3a)
                    .IsRequired()
                    .HasColumnName("FILL3A")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Fnam03)
                    .IsRequired()
                    .HasColumnName("FNAM03")
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Freg03)
                    .HasColumnName("FREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Incr03)
                    .HasColumnName("INCR03")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Lnam03)
                    .IsRequired()
                    .HasColumnName("LNAM03")
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Mnam03)
                    .IsRequired()
                    .HasColumnName("MNAM03")
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Mntd03)
                    .HasColumnName("MNTD03")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Mntn03)
                    .IsRequired()
                    .HasColumnName("MNTN03")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Mreg03)
                    .HasColumnName("MREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Mrtl03)
                    .IsRequired()
                    .HasColumnName("MRTL03")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Natl03)
                    .IsRequired()
                    .HasColumnName("NATL03")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Nreg03)
                    .HasColumnName("NREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Pds103)
                    .IsRequired()
                    .HasColumnName("PDS103")
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Pds203)
                    .IsRequired()
                    .HasColumnName("PDS203")
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Phn103)
                    .HasColumnName("PHN103")
                    .HasColumnType("numeric(14, 0)");

                entity.Property(e => e.Phn203)
                    .HasColumnName("PHN203")
                    .HasColumnType("numeric(14, 0)");

                entity.Property(e => e.Plob03)
                    .IsRequired()
                    .HasColumnName("PLOB03")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Prov03)
                    .IsRequired()
                    .HasColumnName("PROV03")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Rben03)
                    .IsRequired()
                    .HasColumnName("RBEN03")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Regn03)
                    .IsRequired()
                    .HasColumnName("REGN03")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Rrgs03)
                    .HasColumnName("RRGS03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rsfs03)
                    .HasColumnName("RSFS03")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Sefg03)
                    .IsRequired()
                    .HasColumnName("SEFG03")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Sreg03)
                    .HasColumnName("SREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Totc03)
                    .HasColumnName("TOTC03")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Treg03)
                    .HasColumnName("TREG03")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Verb03)
                    .IsRequired()
                    .HasColumnName("VERB03")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Verm03)
                    .IsRequired()
                    .HasColumnName("VERM03")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Vndr03)
                    .IsRequired()
                    .HasColumnName("VNDR03")
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");
            });

            modelBuilder.Entity<TtRcwe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"TT.RCWE\"", "WEBPORTAL");

                entity.Property(e => e.Actv10)
                    .IsRequired()
                    .HasColumnName("ACTV10")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ampd10)
                    .HasColumnName("AMPD10")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Bchc10)
                    .HasColumnName("BCHC10")
                    .HasColumnType("numeric(11, 2)");

                entity.Property(e => e.Bchi10)
                    .HasColumnName("BCHI10")
                    .HasColumnType("numeric(11, 2)");

                entity.Property(e => e.Ccen10)
                    .HasColumnName("CCEN10")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cenr10)
                    .HasColumnName("CENR10")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Conm10)
                    .HasColumnName("CONM10")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cony10)
                    .HasColumnName("CONY10")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Date10)
                    .HasColumnName("DATE10")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Datr10)
                    .HasColumnName("DATR10")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Edit10)
                    .IsRequired()
                    .HasColumnName("EDIT10")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Fill10)
                    .IsRequired()
                    .HasColumnName("FILL10")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Finp10)
                    .HasColumnName("FINP10")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Freq10)
                    .IsRequired()
                    .HasColumnName("FREQ10")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Intp10)
                    .HasColumnName("INTP10")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Iwk110)
                    .HasColumnName("IWK110")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Iwk210)
                    .HasColumnName("IWK210")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Iwk310)
                    .HasColumnName("IWK310")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Iwk410)
                    .HasColumnName("IWK410")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Iwk510)
                    .HasColumnName("IWK510")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Iwk610)
                    .HasColumnName("IWK610")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Lock10)
                    .IsRequired()
                    .HasColumnName("LOCK10")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Rreg10)
                    .HasColumnName("RREG10")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rrsf10)
                    .HasColumnName("RRSF10")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Tcon10)
                    .HasColumnName("TCON10")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.User10)
                    .IsRequired()
                    .HasColumnName("USER10")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Wsid10)
                    .IsRequired()
                    .HasColumnName("WSID10")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");
            });

            modelBuilder.Entity<TtRcxe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"TT.RCXE\"", "WEBPORTAL");

                entity.Property(e => e.Actv10)
                    .IsRequired()
                    .HasColumnName("ACTV10")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ampd10)
                    .HasColumnName("AMPD10")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Bchc10)
                    .HasColumnName("BCHC10")
                    .HasColumnType("numeric(11, 2)");

                entity.Property(e => e.Bchi10)
                    .HasColumnName("BCHI10")
                    .HasColumnType("numeric(11, 2)");

                entity.Property(e => e.Ccen10)
                    .HasColumnName("CCEN10")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cenr10)
                    .HasColumnName("CENR10")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Conm10)
                    .HasColumnName("CONM10")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Cony10)
                    .HasColumnName("CONY10")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Date10)
                    .HasColumnName("DATE10")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Datr10)
                    .HasColumnName("DATR10")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Edit10)
                    .IsRequired()
                    .HasColumnName("EDIT10")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Fill10)
                    .IsRequired()
                    .HasColumnName("FILL10")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Finp10)
                    .HasColumnName("FINP10")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Freq10)
                    .IsRequired()
                    .HasColumnName("FREQ10")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Intp10)
                    .HasColumnName("INTP10")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Iwk110)
                    .HasColumnName("IWK110")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Iwk210)
                    .HasColumnName("IWK210")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Iwk310)
                    .HasColumnName("IWK310")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Iwk410)
                    .HasColumnName("IWK410")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Iwk510)
                    .HasColumnName("IWK510")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Iwk610)
                    .HasColumnName("IWK610")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Lock10)
                    .IsRequired()
                    .HasColumnName("LOCK10")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Rreg10)
                    .HasColumnName("RREG10")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Rrsf10)
                    .HasColumnName("RRSF10")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Tcon10)
                    .HasColumnName("TCON10")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.User10)
                    .IsRequired()
                    .HasColumnName("USER10")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Wsid10)
                    .IsRequired()
                    .HasColumnName("WSID10")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");
            });

            modelBuilder.Entity<TtStat>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("\"TT.STAT\"", "WEBPORTAL");

                entity.Property(e => e.Actv34)
                    .HasColumnName("ACTV34")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ccen34)
                    .HasColumnName("CCEN34")
                    .HasColumnType("numeric(2, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Comm34)
                    .HasColumnName("COMM34")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Conm34)
                    .HasColumnName("CONM34")
                    .HasColumnType("numeric(2, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Cony34)
                    .HasColumnName("CONY34")
                    .HasColumnType("numeric(2, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Cors34)
                    .HasColumnName("CORS34")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Fill34)
                    .HasColumnName("FILL34")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Keyc34)
                    .HasColumnName("KEYC34")
                    .HasColumnType("numeric(2, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Keyd34)
                    .HasColumnName("KEYD34")
                    .HasColumnType("numeric(6, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Lin34)
                    .HasColumnName("LIN#34")
                    .HasColumnType("numeric(5, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Orgs34)
                    .HasColumnName("ORGS34")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Rreg34)
                    .HasColumnName("RREG34")
                    .HasColumnType("numeric(9, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Rrsf34)
                    .HasColumnName("RRSF34")
                    .HasColumnType("numeric(3, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Subc34)
                    .HasColumnName("SUBC34")
                    .HasColumnType("numeric(2, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Subd34)
                    .HasColumnName("SUBD34")
                    .HasColumnType("numeric(6, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.User34)
                    .HasColumnName("USER34")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("(' ')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
