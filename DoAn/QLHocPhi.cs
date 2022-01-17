using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Entity.Validation;
using DoAn.Model;

namespace DoAn
{

    public partial class QLHocPhi : Form
    {
        DbContentSinhVien dbContent = new DbContentSinhVien();
        string flag;
        public QLHocPhi()
        {
            InitializeComponent();
        }

        private void QLHocPhi_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSetQLDiem1.SinhVien' table. You can move, or remove it, as needed.
            this.sinhVienTableAdapter.Fill(this.dataSetQLDiem1.SinhVien);
            // TODO: This line of code loads data into the 'dataSetQLDiem1.Lop' table. You can move, or remove it, as needed.
            this.lopTableAdapter.Fill(this.dataSetQLDiem1.Lop);
            this.khoaTableAdapter.Fill(this.dataSetQLDiem1.Khoa);
            ketnoi = new SqlConnection(chuoiketnoi);

            List<SinhVien> listSinhVien = dbContent.SinhVien.ToList();
            List<Lop> listLop = dbContent.Lop.ToList();
            List<Khoa> listKhoa = dbContent.Khoa.ToList();
            cbbMaKhoa.SelectedIndex = -1;
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            var newlistSV = (from x in dbContent.SinhVien
                             from z in dbContent.Lop
                             from y in dbContent.Khoa
                             from t in dbContent.HocPhi
                             where

                              (cbbMaKhoa.Text.ToString() == "" || y.MaKhoa.CompareTo(cbbMaKhoa.Text.ToString()) == 0)    // lớp
                             && (cbbMaLop.Text.ToString() == "" || z.MaLop.CompareTo(cbbMaLop.Text.ToString()) == 0)    // khoa
                             &&(cbbMSSV.Text.ToString()=="" ||t.MSSV.CompareTo(cbbMSSV.Text.ToString())==0)
                             && x.MaLop == z.MaLop && z.MaKhoa == y.MaKhoa && x.MSSV == t.MSSV
                             select new
                             {
                                 Mã_Sinh_Viên = x.MSSV,
                                 MaHocPhiSV = t.MaHocPhi,
                                 SoTienHp = t.SoTien,
                                 Tên_lớp = x.MaLop,
                                 Khoa = y.TenKhoa

                             }
             ).Distinct().ToList();
            dgvHocPhi.Rows.Clear();
            foreach (var item in newlistSV)
            {
                int newRow = dgvHocPhi.Rows.Add();
                dgvHocPhi.Rows[newRow].Cells[0].Value = item.Mã_Sinh_Viên;
                dgvHocPhi.Rows[newRow].Cells[3].Value = item.MaHocPhiSV;
                dgvHocPhi.Rows[newRow].Cells[4].Value = item.SoTienHp;
                dgvHocPhi.Rows[newRow].Cells[1].Value = item.Tên_lớp;
                dgvHocPhi.Rows[newRow].Cells[2].Value = item.Khoa;
            }
        }
        string chuoiketnoi = @"data source=LAPTOP-DENGDHRK\MSSQLSERVER03;initial catalog=QLSV;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i = 0;
        int j = 0;
        string sql;
        private void cbbMaKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbMaLop.Items.Clear();
            ketnoi.Open();
            sql = @"Select Khoa.MaKhoa,Lop.MaLop
                    From    Khoa Join Lop
                    On Khoa.MaKhoa = Lop.MaKhoa
                    Where (Khoa.MaKhoa = N'" + cbbMaKhoa.Text + @"')";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while (docdulieu.Read())
            {
                cbbMaLop.Items.Add(docdulieu[1].ToString());
                i++;
            }
            ketnoi.Close();
        }

        private void cbbMaLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbMSSV.Items.Clear();
            ketnoi.Open();
            sql = @"Select Lop.MaLop,SinhVien.MSSV
                    From    Lop Join SinhVien
                    On Lop.MaLop = SinhVien.MaLop
                    Where (Lop.MaLop = N'" + cbbMaLop.Text + @"')";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            j = 0;
            while (docdulieu.Read())
            {
                cbbMSSV.Items.Add(docdulieu[1].ToString());
                j++;
            }
            ketnoi.Close();
        }
    }
}
