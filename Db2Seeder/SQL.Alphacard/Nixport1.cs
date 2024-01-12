using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.SQL.Alphacard
{
    public partial class Nixport1
    {
        [Required]
        [Column("NISNUM")]
        [StringLength(9)]
        public string Nisnum { get; set; }
        [Required]
        [Column("FNAME")]
        [StringLength(25)]
        public string Fname { get; set; }
        [Required]
        [Column("LNAME")]
        [StringLength(25)]
        public string Lname { get; set; }
        [Required]
        [Column("SEX")]
        [StringLength(6)]
        public string Sex { get; set; }
        [Required]
        [Column("DOB")]
        [StringLength(10)]
        public string Dob { get; set; }
        [Required]
        [Column("NATL")]
        [StringLength(50)]
        public string Natl { get; set; }
        [Required]
        [Column("PLOB")]
        [StringLength(15)]
        public string Plob { get; set; }
        [Required]
        [Column("ISSUED")]
        [StringLength(10)]
        public string Issued { get; set; }
        [Column("CARDPRINT")]
        public short Cardprint { get; set; }
        [Required]
        [Column("LASTMOD")]
        [StringLength(20)]
        public string Lastmod { get; set; }
        [Required]
        [Column("PHOTO")]
        [StringLength(200)]
        public string Photo { get; set; }
        [Required]
        [Column("SIGNATURE")]
        [StringLength(200)]
        public string Signature { get; set; }
    }
}
