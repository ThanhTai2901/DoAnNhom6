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
    public partial class XemDiemTheoMon : Form
    {
        DbContentSinhVien dbContent = new DbContentSinhVien();
        public XemDiemTheoMon()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnQuayVe_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void XemDiemTheoMon_Load(object sender, EventArgs e)
        {
            List<SinhVien> listSV = dbContent.SinhVien.ToList();
            List<MonHoc> listMon = dbContent.MonHoc.ToList();
            List<Diem> listDiem = dbContent.Diem.ToList();
            FillDataCBBTimKiem(listMon);
            cbbTenMon.SelectedIndex = 0;
        }
        private void FillDataCBBTimKiem(List<MonHoc> listMon)
        {
            cbbTenMon.DataSource = listMon;
            cbbTenMon.DisplayMember = "TenMon";
            cbbTenMon.ValueMember = "TenMon";
        }

        private void btnXemDiem_Click(object sender, EventArgs e)
        {
            var newlistXemTheoMon = (from x in dbContent.MonHoc
                                     from y in dbContent.SinhVien
                                     from z in dbContent.Khoa
                                     from w in dbContent.Diem
                                     from t in dbContent.Lop
                                     orderby w.DiemTongKet        // xem giam dan theo diem
                                     where (cbbTenMon.Text.ToString() == x.TenMon   // chọn mã môn
                                     && w.MSSV == y.MSSV  // diem voi sv
                                     && w.MaMon == x.MaMon // diem voi mon hoc
                                     && t.MaLop == y.MaLop // sv voi lop
                                     && t.MaKhoa == z.MaKhoa // lop voi khoa
                                     )
                                     select new
                                     {
                                         MaSV = y.MSSV,
                                         HoSV = y.Ho,
                                         TenSV = y.Ten,
                                         DiemMon = w.DiemTongKet,
                                         MaKhoaDiem = z.MaKhoa,
                                         MaLopDiem = t.MaLop
                                     }
                         ).ToList();
            int i = 0;
            dgvXemDiem.Rows.Clear();

                foreach (var item in newlistXemTheoMon)
                {
                    int newRow = dgvXemDiem.Rows.Add();
                i++;
                dgvXemDiem.Rows[newRow].Cells[0].Value = i;
                dgvXemDiem.Rows[newRow].Cells[1].Value = item.MaSV;
                    dgvXemDiem.Rows[newRow].Cells[2].Value = item.HoSV;
                    dgvXemDiem.Rows[newRow].Cells[3].Value = item.TenSV;
                    dgvXemDiem.Rows[newRow].Cells[4].Value = item.DiemMon;
                dgvXemDiem.Rows[newRow].Cells[5].Value = item.MaKhoaDiem;
                dgvXemDiem.Rows[newRow].Cells[6].Value = item.MaLopDiem;
            }
            txtTong.Text = (dgvXemDiem.Rows.Count - 1).ToString();
            if (txtTong.Text == "0")
            {
                MessageBox.Show("SV không có điểm");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvXemDiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvXemDiem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dgvXemDiem.CurrentRow.Selected = true;

                    //cbbTenMon.Text = dgvXemDiem.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
