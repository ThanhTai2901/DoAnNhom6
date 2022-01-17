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
    public partial class QLTimKiem : Form
    {
        DbContentSinhVien dbContent = new DbContentSinhVien();
        public QLTimKiem()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void QLTimKiem_Load(object sender, EventArgs e)
        {
            List<SinhVien> listSinhVien = dbContent.SinhVien.ToList();
            List<Khoa> listKhoa = dbContent.Khoa.ToList();
            List<Lop> listLop = dbContent.Lop.ToList();
            FillDataCBB_Khoa(listKhoa);
            FillDataCBB_Lop(listLop);
            cbbKhoa.SelectedIndex = -1;
            cbbLop.SelectedIndex = -1;
        }
        private void TimKiem_Load(object sender, EventArgs e)
        {
            List<SinhVien> listSinhVien = dbContent.SinhVien.ToList();
            List<Lop> listLop = dbContent.Lop.ToList();
            List<Khoa> listKhoa = dbContent.Khoa.ToList();
            FillDataCBB_Khoa(listKhoa);
            FillDataCBB_Lop(listLop);
            cbbKhoa.SelectedIndex = -1;
        }
        private void FillDataCBB_Khoa(List<Khoa> listKhoa)
        {
            cbbKhoa.DataSource = listKhoa;
            cbbKhoa.DisplayMember = "TenKhoa";
            cbbKhoa.ValueMember = "MaKhoa";
        }
        private void FillDataCBB_Lop(List<Lop> listLop)
        {
            cbbLop.DataSource = listLop;
            cbbLop.DisplayMember = "MaLop";
            cbbLop.ValueMember = "MaLop";
        }
        private void btnQuayVe_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            var newlistSV = (from x in dbContent.SinhVien
                             from z in dbContent.Lop
                             from y in dbContent.Khoa
                             where
                             (txtMa.Text.ToString() == "" || x.MSSV.Contains(txtMa.Text))       // kiem theo ma
                             && (txtTen.Text.ToString() == "" || x.Ten.Contains(txtTen.Text))   // theo tên
                             && (txtHo.Text.ToString() == "" || x.Ho.Contains(txtHo.Text))      // theo họ
                             && (cbbKhoa.Text.ToString() == "" || y.TenKhoa.CompareTo(cbbKhoa.Text.ToString()) == 0)    // lớp
                             && (cbbLop.Text.ToString() == "" || z.MaLop.CompareTo(cbbLop.Text.ToString()) == 0)    // khoa
                             && x.MaLop == z.MaLop && z.MaKhoa ==y.MaKhoa
                             select new
                             {
                                 Mã_Sinh_Viên = x.MSSV,
                                 Họ = x.Ho,
                                 Tên = x.Ten,
                                 Ngày_Sinh = x.NamSinh,
                                 Giới_Tính = x.GioiTinh,
                                 Tên_lớp = x.MaLop,
                                 Khoa = y.TenKhoa
                                 
                             }
                         ).Distinct().ToList();
            dgvTimKiem.Rows.Clear();
            foreach (var item in newlistSV)
            {
                int newRow = dgvTimKiem.Rows.Add();
                dgvTimKiem.Rows[newRow].Cells[0].Value = item.Mã_Sinh_Viên;
                dgvTimKiem.Rows[newRow].Cells[1].Value = item.Họ;
                dgvTimKiem.Rows[newRow].Cells[2].Value = item.Tên;
                dgvTimKiem.Rows[newRow].Cells[3].Value = item.Ngày_Sinh;
                dgvTimKiem.Rows[newRow].Cells[4].Value = item.Giới_Tính;
                dgvTimKiem.Rows[newRow].Cells[5].Value = item.Tên_lớp;
                dgvTimKiem.Rows[newRow].Cells[6].Value = item.Khoa;
            }
            txtKetQuaTimKiem.Text = (dgvTimKiem.Rows.Count-1).ToString();
            if (txtKetQuaTimKiem.Text == "0")
            {
                MessageBox.Show("Không có sinh viên bạn cần tìm");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMa.Clear();
            txtHo.Clear();
            txtTen.Clear();
            
            txtKetQuaTimKiem.Clear();
            dgvTimKiem.Rows.Clear();
            cbbKhoa.SelectedIndex = -1;
            cbbLop.SelectedIndex = -1;
        }


        private void dgvTimKiem_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvTimKiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            SinhVien XoaSV = dbContent.SinhVien.Where(p => p.MSSV == txtMa.Text).FirstOrDefault();
            if (XoaSV != null)        // neu tra ve -1 thi sv chua co trong ds
            {
                DialogResult XD = MessageBox.Show("Bạn có chắc muốn xóa sinh viên ? ", "Yes/No", MessageBoxButtons.YesNo);
                if (XD == DialogResult.Yes)
                {

                    dbContent.SinhVien.Remove(XoaSV);
                    dbContent.SaveChanges();
                    MessageBox.Show($"Xóa sinh viên {XoaSV.MSSV} thành công!", "Thông báo");
                }
                else
                {
                    MessageBox.Show($"Xóa sinh viên {txtMa.Text} thất bại", "Thông báo");
                }

            }
        }

        private void dgvTimKiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                if (dgvTimKiem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dgvTimKiem.CurrentRow.Selected = true;
                    txtMa.Text = dgvTimKiem.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                    txtHo.Text = dgvTimKiem.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                    txtTen.Text = dgvTimKiem.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                    cbbLop.Text = dgvTimKiem.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
                    cbbKhoa.Text = dgvTimKiem.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
                }
        }
    }
}
