namespace DoAn.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Diem")]
    public partial class Diem
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MSSV { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6)]
        public string MaMon { get; set; }

        public double? DiemQuaTrinh { get; set; }

        public double? DiemThi { get; set; }

        public double? DiemTongKet { get; set; }

        [StringLength(2)]
        public string DiemChu { get; set; }

        [StringLength(6)]
        public string MaHocKy { get; set; }

        public virtual HocKy HocKy { get; set; }

        public virtual MonHoc MonHoc { get; set; }

        public virtual SinhVien SinhVien { get; set; }
    }
}
