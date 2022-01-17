using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DoAn.Model
{
    public partial class DbContentSinhVien : DbContext
    {
        public DbContentSinhVien()
            : base("name=DbContentSinhVien3")
        {
        }

        public virtual DbSet<Diem> Diem { get; set; }
        public virtual DbSet<DTB> DTB { get; set; }
        public virtual DbSet<GiangVien> GiangVien { get; set; }
        public virtual DbSet<HocKy> HocKy { get; set; }
        public virtual DbSet<HocPhi> HocPhi { get; set; }
        public virtual DbSet<Khoa> Khoa { get; set; }
        public virtual DbSet<KhoaHoc> KhoaHoc { get; set; }
        public virtual DbSet<Lop> Lop { get; set; }
        public virtual DbSet<MonHoc> MonHoc { get; set; }
        public virtual DbSet<SinhVien> SinhVien { get; set; }
        public virtual DbSet<SVDangKiMonHoc> SVDangKiMonHoc { get; set; }
        public virtual DbSet<ThoiKhoaBieu> ThoiKhoaBieu { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diem>()
                .Property(e => e.MSSV)
                .IsUnicode(false);

            modelBuilder.Entity<Diem>()
                .Property(e => e.MaMon)
                .IsUnicode(false);

            modelBuilder.Entity<Diem>()
                .Property(e => e.DiemChu)
                .IsUnicode(false);

            modelBuilder.Entity<Diem>()
                .Property(e => e.MaHocKy)
                .IsUnicode(false);

            modelBuilder.Entity<DTB>()
                .Property(e => e.MSSV)
                .IsUnicode(false);

            modelBuilder.Entity<DTB>()
                .Property(e => e.MaHocKy)
                .IsUnicode(false);

            modelBuilder.Entity<GiangVien>()
                .Property(e => e.MaGiangVien)
                .IsUnicode(false);

            modelBuilder.Entity<GiangVien>()
                .Property(e => e.SDT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<GiangVien>()
                .Property(e => e.MaKhoa)
                .IsUnicode(false);

            modelBuilder.Entity<HocKy>()
                .Property(e => e.MaHocKy)
                .IsUnicode(false);

            modelBuilder.Entity<HocPhi>()
                .Property(e => e.MaHocPhi)
                .IsUnicode(false);

            modelBuilder.Entity<HocPhi>()
                .Property(e => e.SoTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<HocPhi>()
                .Property(e => e.MSSV)
                .IsUnicode(false);

            modelBuilder.Entity<Khoa>()
                .Property(e => e.MaKhoa)
                .IsUnicode(false);

            modelBuilder.Entity<KhoaHoc>()
                .Property(e => e.MaKhoaHoc)
                .IsUnicode(false);

            modelBuilder.Entity<Lop>()
                .Property(e => e.MaLop)
                .IsUnicode(false);

            modelBuilder.Entity<Lop>()
                .Property(e => e.MaKhoaHoc)
                .IsUnicode(false);

            modelBuilder.Entity<Lop>()
                .Property(e => e.MaKhoa)
                .IsUnicode(false);

            modelBuilder.Entity<Lop>()
                .HasOptional(e => e.ThoiKhoaBieu)
                .WithRequired(e => e.Lop);

            modelBuilder.Entity<MonHoc>()
                .Property(e => e.MaMon)
                .IsUnicode(false);

            modelBuilder.Entity<MonHoc>()
                .HasMany(e => e.Diems)
                .WithRequired(e => e.MonHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MonHoc>()
                .HasMany(e => e.SVDangKiMonHocs)
                .WithRequired(e => e.MonHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.MSSV)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.CCCD)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.MaLop)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .HasMany(e => e.Diems)
                .WithRequired(e => e.SinhVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SinhVien>()
                .HasMany(e => e.DTBs)
                .WithRequired(e => e.SinhVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SinhVien>()
                .HasMany(e => e.SVDangKiMonHocs)
                .WithRequired(e => e.SinhVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SVDangKiMonHoc>()
                .Property(e => e.MSSV)
                .IsUnicode(false);

            modelBuilder.Entity<SVDangKiMonHoc>()
                .Property(e => e.MaMon)
                .IsUnicode(false);

            modelBuilder.Entity<ThoiKhoaBieu>()
                .Property(e => e.MaLop)
                .IsUnicode(false);

            modelBuilder.Entity<ThoiKhoaBieu>()
                .Property(e => e.MaMon)
                .IsUnicode(false);

            modelBuilder.Entity<ThoiKhoaBieu>()
                .Property(e => e.MaGiangVien)
                .IsUnicode(false);
        }
    }
}
