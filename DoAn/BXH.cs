using System;
using System.Collections.Generic;
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
using DoAn.Model;

namespace DoAn
{
    public partial class BXH : Form
    {
        DbContentSinhVien dbContent = new DbContentSinhVien();
        public BXH()
        {
            InitializeComponent();
        }

        private void BXH_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSetQLDiem1.Lop' table. You can move, or remove it, as needed.
            this.lopTableAdapter.Fill(this.dataSetQLDiem1.Lop);
            // TODO: This line of code loads data into the 'dataSetQLDiem1.Khoa' table. You can move, or remove it, as needed.
            this.khoaTableAdapter.Fill(this.dataSetQLDiem1.Khoa);


            ketnoi = new SqlConnection(chuoiketnoi);
            List<SinhVien> listSinhVien = dbContent.SinhVien.ToList();
            List<Khoa> listKhoa = dbContent.Khoa.ToList();
            List<Lop> listLop = dbContent.Lop.ToList();
            List<HocKy> listHocKy = dbContent.HocKy.ToList();
            FillDataCBB_HocKy(listHocKy);
            cbbKhoa.SelectedIndex = -1;
        }
        string chuoiketnoi = @"data source=LAPTOP-DENGDHRK\MSSQLSERVER03;initial catalog=QLSV;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i = 0;
        string sql;
        private void FillDataCBB_HocKy(List<HocKy> listHocKy)
        {
            cbbHocKi.DataSource = listHocKy;
            cbbHocKi.DisplayMember = "MaHocKy";
            cbbHocKi.ValueMember = "MaHocKy";
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            var newlistSV =  (from x in dbContent.SinhVien
                             from z in dbContent.Lop
                             from y in dbContent.Khoa
                             from t in dbContent.DTB
                             orderby t.DTB1
                             where
                             (cbbKhoa.Text.ToString() == "" || y.TenKhoa.Contains(cbbKhoa.Text))       // kiem theo ma
                             && (cbbLop.Text.ToString() == "" || z.MaLop.Contains(cbbLop.Text))   // theo tên
                             && (cbbHocKi.Text.ToString() == "" || t.MaHocKy.Contains(cbbHocKi.Text))      // theo họ    // khoa
                             && x.MaLop == z.MaLop && z.MaKhoa == y.MaKhoa && x.MSSV==t.MSSV
                             select new
                             {
                                 Mã_Sinh_Viên = x.MSSV,
                                 Tên_lớp = x.MaLop,
                                 Khoa = y.TenKhoa,
                                 DiemTB = t.DTB1,
                                 HocKi = t.MaHocKy

                             }).Distinct().ToList();
            dgvBXH.Rows.Clear();
            int i = 0;
            foreach (var item in newlistSV)
            {
                int newRow = dgvBXH.Rows.Add();
                i++;
                dgvBXH.Rows[newRow].Cells[0].Value = i;
                dgvBXH.Rows[newRow].Cells[1].Value = item.Mã_Sinh_Viên;
                dgvBXH.Rows[newRow].Cells[2].Value = item.Tên_lớp;
                dgvBXH.Rows[newRow].Cells[3].Value = item.Khoa;
                dgvBXH.Rows[newRow].Cells[4].Value = item.DiemTB;
                dgvBXH.Rows[newRow].Cells[5].Value = item.HocKi;
            }
        }

        private void cbbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbLop.Items.Clear();
            ketnoi.Open();
            sql = @"Select Khoa.MaKhoa,Lop.MaLop
                    From    Khoa Join Lop
                    On Khoa.MaKhoa = Lop.MaKhoa
                    Where (Khoa.MaKhoa = N'" + cbbKhoa.Text + @"')";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while (docdulieu.Read())
            {
                cbbLop.Items.Add(docdulieu[1].ToString());
                i++;
            }
            ketnoi.Close();
        }

        private void cbbLop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cbbKhoa.SelectedIndex = -1;
            cbbLop.SelectedIndex = -1;
            cbbHocKi.SelectedIndex = -1;
            dgvBXH.ClearSelection();
        }
    }
}
