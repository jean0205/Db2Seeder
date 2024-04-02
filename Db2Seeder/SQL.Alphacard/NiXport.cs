using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.SQL.Alphacard
{
    [Table("NI_XPORT")]
    public partial class NiXport
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NISNUM")]
        [StringLength(9)]
        public string Nisnum { get; set; }
        [Column("FNAME")]
        [StringLength(50)]
        public string Fname { get; set; }
        [Column("LNAME")]
        [StringLength(50)]
        public string Lname { get; set; }
        [Column("SEX")]
        [StringLength(10)]
        public string Sex { get; set; }
        [Column("DOB")]
        [StringLength(15)]
        public string Dob { get; set; }
        [Column("NATL")]
        [StringLength(100)]
        public string Natl { get; set; }
        [Column("PLOB")]
        [StringLength(100)]
        public string Plob { get; set; }
        [Column("ISSUED", TypeName = "date")]
        public DateTime? Issued { get; set; }
        [Column("CARDPRINT")]
        public int? Cardprint { get; set; }
        [Column("LASTMOD")]
        [StringLength(15)]
        public string Lastmod { get; set; }
        [Column("PHOTO")]
        [StringLength(500)]
        public string Photo { get; set; }
        [Column("SIGNATURE")]
        [StringLength(500)]
        public string Signature { get; set; }
        [Column("PIC")]
        public byte[] Pic { get; set; }
        [Column("SIG")]
        public byte[] Sig { get; set; }
        public bool? PicUpdated { get; set; }
        public bool? SigUpdated { get; set; }
    }
}
