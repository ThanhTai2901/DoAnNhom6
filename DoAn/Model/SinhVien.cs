namespace DoAn.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SinhVien")]
    public partial class SinhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SinhVien()
        {
            Diems = new HashSet<Diem>();
            DTBs = new HashSet<DTB>();
            HocPhis = new HashSet<HocPhi>();
            SVDangKiMonHocs = new HashSet<SVDangKiMonHoc>();
        }

        [Key]
        [StringLength(10)]
        public string MSSV { get; set; }

        [StringLength(6)]
        public string Ho { get; set; }

        [StringLength(30)]
        public string Ten { get; set; }

        [StringLength(12)]
        public string CCCD { get; set; }

        [StringLength(12)]
        public string DanToc { get; set; }

        [StringLength(4)]
        public string GioiTinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NamSinh { get; set; }

        [StringLength(12)]
        public string SDT { get; set; }

        [StringLength(254)]
        public string Email { get; set; }

        [StringLength(7)]
        public string MaLop { get; set; }

        [StringLength(10)]
        public string TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diem> Diems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTB> DTBs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HocPhi> HocPhis { get; set; }

        public virtual Lop Lop { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SVDangKiMonHoc> SVDangKiMonHocs { get; set; }
    }
}
