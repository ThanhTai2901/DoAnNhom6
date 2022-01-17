namespace DoAn.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DTB")]
    public partial class DTB
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MSSV { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6)]
        public string MaHocKy { get; set; }

        [Column("DTB")]
        public double DTB1 { get; set; }

        public virtual SinhVien SinhVien { get; set; }
    }
}
