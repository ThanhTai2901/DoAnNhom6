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
    public partial class XemDiemTheoMSSVcs : Form
    {
        DbContentSinhVien dbContent = new DbContentSinhVien();
        string tendangnhap = "", matkhau = "", quyen = "";
        public XemDiemTheoMSSVcs()
        {
            InitializeComponent();
        }
        public XemDiemTheoMSSVcs(string tendangnhap, string matkhau, string quyen)
        {
            InitializeComponent();
            this.tendangnhap = tendangnhap;
            this.matkhau = matkhau;
            this.quyen = quyen;
        }
        private void FillDataCBB_HocKy(List<HocKy> listHocKy)
        {
            cbbMaHocKy.DataSource = listHocKy;
            cbbMaHocKy.DisplayMember = "MaHocKy";
            cbbMaHocKy.ValueMember = "MaHocKy";
        }
        private void btnQuayVe_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void loadForm()
        {
        }
        private void btnXemDiem_Click(object sender, EventArgs e)
        {
            if (quyen == "SV")
            {
                double temp1 = 0;
                var newlistXemTheoMaSV1 = (from m in dbContent.MonHoc
                                          from s in dbContent.SinhVien
                                          from d in dbContent.Diem
                                          where s.MSSV.CompareTo(tendangnhap) == 0
                                          && (s.MSSV == d.MSSV) && (m.MaMon == d.MaMon)
                                          && (d.MaHocKy == cbbMaHocKy.Text || cbbMaHocKy.Text.ToString() == "")

                                           select new
                                          {
                                              Ma_SV = d.MSSV,
                                              Ten_Mon = m.TenMon,
                                              Diem_TK = d.DiemTongKet,
                                              Diem_Chu = d.DiemChu,
                                              Hoc_Ky = d.MaHocKy
                                          }
                                        ).Distinct().ToList();
                dgvXemDiem.Rows.Clear();
                foreach (var item in newlistXemTheoMaSV1)
                {
                    int newRow = dgvXemDiem.Rows.Add();
                    dgvXemDiem.Rows[newRow].Cells[0].Value = item.Ma_SV;
                    dgvXemDiem.Rows[newRow].Cells[1].Value = item.Ten_Mon;
                    dgvXemDiem.Rows[newRow].Cells[2].Value = item.Diem_TK;
                    dgvXemDiem.Rows[newRow].Cells[3].Value = item.Diem_Chu;
                    dgvXemDiem.Rows[newRow].Cells[4].Value = item.Hoc_Ky;
                    temp1 = temp1 + Convert.ToDouble(dgvXemDiem.Rows[newRow].Cells[2].Value);
                    txtDTB.Text = (String.Format("{0:0.00}", temp1 / (dgvXemDiem.Rows.Count - 1))).ToString();
                }
            }
            else
            { 
                double temp = 0;
                var newlistXemTheoMaSV = (from m in dbContent.MonHoc
                                          from s in dbContent.SinhVien
                                          from d in dbContent.Diem
                                          where s.MSSV.Contains(cbbMSSV.Text)
                                          && (s.MSSV == d.MSSV) && (m.MaMon==d.MaMon)
                                          && (d.MaHocKy==cbbMaHocKy.Text || cbbMaHocKy.Text.ToString()=="")
                                          select new
                                          {
                                              Ma_SV = d.MSSV,
                                              Ten_Mon = m.TenMon,
                                              Diem_TK = d.DiemTongKet,
                                              Diem_Chu = d.DiemChu,
                                              Hoc_Ky = d.MaHocKy
                                          }
                                        ).Distinct().ToList();
                dgvXemDiem.Rows.Clear();
                foreach (var item in newlistXemTheoMaSV)
                {
                    int newRow = dgvXemDiem.Rows.Add();
                    dgvXemDiem.Rows[newRow].Cells[0].Value = item.Ma_SV;
                    dgvXemDiem.Rows[newRow].Cells[1].Value = item.Ten_Mon;
                    dgvXemDiem.Rows[newRow].Cells[2].Value = item.Diem_TK;
                    dgvXemDiem.Rows[newRow].Cells[3].Value = item.Diem_Chu;
                    dgvXemDiem.Rows[newRow].Cells[4].Value = item.Hoc_Ky;
                    temp = temp + Convert.ToDouble( item.Diem_TK);
                    txtDTB.Text = (String.Format("{0:0.00}", temp / (dgvXemDiem.Rows.Count - 1))).ToString();
                }
                if(cbbMSSV.Text=="")
                {
                    label2.Visible = false;
                    txtDTB.Visible = false;
                }
                else
                {
                    label2.Visible =true;
                    txtDTB.Visible = true;
                }    
            }

        }

        private void dgvXemDiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvXemDiem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dgvXemDiem.CurrentRow.Selected = true;

                    cbbMSSV.Text = dgvXemDiem.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        string chuoiketnoi = @"data source=LAPTOP-DENGDHRK\MSSQLSERVER03;initial catalog=QLSV;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i = 0;
        int j = 0;
        string sql;
        private void XemDiemTheoMSSVcs_Load(object sender, EventArgs e)
        {
            this.sinhVienTableAdapter.Fill(this.dataSetQLDiem.SinhVien);
            this.lopTableAdapter.Fill(this.dataSetQLDiem.Lop);
            this.khoaTableAdapter.Fill(this.dataSetQLDiem.Khoa);
            ketnoi = new SqlConnection(chuoiketnoi);

            List<SinhVien> listSinhVien = dbContent.SinhVien.ToList();
            List<Lop> listLop = dbContent.Lop.ToList();
            List<Khoa> listKhoa = dbContent.Khoa.ToList();
            List<HocKy> listHocKy = dbContent.HocKy.ToList();
            FillDataCBB_HocKy(listHocKy);
            cbbMaKhoa.SelectedIndex = -1;
            cbbMaHocKy.SelectedIndex=-1;
            cbbMaLop.SelectedIndex = -1;
            txtDTB.Enabled = false;
            if(quyen=="SV")
            {
                label1.Visible = false;
                cbbMSSV.Visible = false;
                cbbMaKhoa.Visible = false;
                cbbMaLop.Visible = false;
                cbbMSSV.SelectedValue = tendangnhap;
                label5.Visible = false;
                label4.Visible = false;
            }

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            XemDiemTheoMSSVcs_Load(sender, e);
        }

        private void cbbMaKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbMaLop.Items.Clear();
            ketnoi.Open();
            sql = @"Select Khoa.MaKhoa,Lop.MaLop
                    From    Khoa Inner  Join  Lop
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
                    From    Lop Inner  Join  SinhVien
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
