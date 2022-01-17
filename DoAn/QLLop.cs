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
    public partial class QLLop : Form
    {
        DbContentSinhVien dbContent = new DbContentSinhVien();
        Boolean flag;

        public QLLop()
        {
            InitializeComponent();
        }
        public void LockTxt(bool val)
        {
            cbbMaKhoa.Enabled = !val;
            cbbMaKhoaHoc.Enabled = !val;
            txtMaLop.ReadOnly = val;
            txtTenLop.ReadOnly = val;
            txtSiSo.ReadOnly = val;
        }
        public void SetNull()
        {
            txtMaLop.Text = "";
            txtTenLop.Text = "";
            txtSiSo.Text = "";
        }
        public void SetButton(bool val)
        {
            btnThem.Enabled = val;
            btnXoa.Enabled = val;
            btnSua.Enabled = val;
            btnLuu.Enabled = !val;
            btnHuy.Enabled = !val;
            btnQuayVe.Enabled = val;
        }

        private void QLLop_Load(object sender, EventArgs e)
        {
            cbbMaKhoa.SelectedIndex = -1;
            List<Lop> listLop = dbContent.Lop.ToList();
            List<Khoa> listKhoa = dbContent.Khoa.ToList();
            List<KhoaHoc> listKhoaHoc = dbContent.KhoaHoc.ToList();
            FillDataDGV_Lop(listLop);
            FillDataCBB_KhoaHoc(listKhoaHoc);
            FillDataCBB_Khoa(listKhoa);
            txtMaLop.Focus();
            SetNull();
            SetButton(true);
            LockTxt(true);
        }
        private void loadDGV()              //load data
        {
            List<Lop> newLop = dbContent.Lop.ToList();
            FillDataDGV_Lop(newLop);
        }
        private void loadForm()
        {

            txtMaLop.Clear();
            txtTenLop.Clear();
            txtSiSo.Clear();
        }
        private void FillDataCBB_Khoa(List<Khoa> listKhoa)
        {
            cbbMaKhoa.DataSource = listKhoa;         // cbb ma mon
            cbbMaKhoa.DisplayMember = "MaKhoa";
            cbbMaKhoa.ValueMember = "MaKhoa";
        }


        private void FillDataDGV_Lop(List<Lop> listLop)           //load data vao dgv
        {
            dgvQLLop.Rows.Clear();
            foreach (var item in listLop)
            {
                int newRow = dgvQLLop.Rows.Add();        //them 1 dong moi
                dgvQLLop.Rows[newRow].Cells[0].Value = item.MaKhoa;
                dgvQLLop.Rows[newRow].Cells[1].Value = item.MaLop;
                dgvQLLop.Rows[newRow].Cells[2].Value = item.TenLop;
                dgvQLLop.Rows[newRow].Cells[3].Value = item.SiSo;
                dgvQLLop.Rows[newRow].Cells[4].Value = item.MaKhoaHoc;

            }
        }

        private void FillDataCBB_KhoaHoc(List<KhoaHoc> listKhoaHoc)           //load mã và tên các môn học từ ds môn vào cbb
        {

            cbbMaKhoaHoc.DataSource = listKhoaHoc;         // cbb ma mon
            cbbMaKhoaHoc.DisplayMember = "MaKhoaHoc";
            cbbMaKhoaHoc.ValueMember = "MaKhoaHoc";

        }
        private int CheckMaLop(string idMaLop)        // kt ma lop
        {
            for (int i = 0; i < dgvQLLop.Rows.Count; i++)
            {
                if (dgvQLLop.Rows[i].Cells[1].Value != null)
                {
                    if (dgvQLLop.Rows[i].Cells[1].Value.ToString() == idMaLop)
                    {
                        return i;

                    }
                }
            }
            return -1;  //-1 khi khong tim thay lop co ma so moi
        }
        private bool CheckDataInput()           // kiem tra du lieu dau vao
        {
            if (txtMaLop.Text == "" || txtTenLop.Text == "" || txtSiSo.Text == "")
            {
                MessageBox.Show("Dữ liệu điểm phải là số !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if ((string)cbbMaKhoa.SelectedValue == "" || (string)cbbMaKhoaHoc.SelectedValue == "")
            {
                MessageBox.Show("Thiếu dữ liệu !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (Convert.ToInt32(txtSiSo.Text) < 0 || Convert.ToInt32(txtSiSo.Text) > 40)
            {
                MessageBox.Show("Giá trị sỉ số không hợp lệ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaLop.Focus();
            flag = true;
            LockTxt(false);
            SetButton(false);
            /*if (CheckDataInput() == true)
            {
                if (CheckMaLop(txtMaLop.Text) == -1)
                {
                    Lop newLop = new Lop();
                    newLop.MaKhoa = cbbMaKhoa.SelectedValue.ToString();
                    newLop.MaLop = txtMaLop.Text;
                    newLop.TenLop = txtTenLop.Text;
                    newLop.SiSo = Convert.ToInt32(txtSiSo.Text);
                    newLop.MaKhoaHoc = cbbMaKhoaHoc.SelectedValue.ToString();

                    dbContent.Lop.AddOrUpdate(newLop);
                    dbContent.SaveChanges();

                    loadForm();
                    loadDGV();
                    MessageBox.Show($"Thêm lớp {newLop.MaLop} thành công", "Thông báo !");

                }
                else
                {
                    MessageBox.Show($"Thêm lớp {txtMaLop.Text} thất bại ", "Thông báo !");
                }

            }*/
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Lop XoaLop = dbContent.Lop.Where(p => p.MaLop == txtMaLop.Text).FirstOrDefault();
            if (XoaLop != null)        // neu tra ve -1 thi sv chua co trong ds
            {
                DialogResult XD = MessageBox.Show("Bạn có chắc muốn xóa ? ", "Yes/No", MessageBoxButtons.YesNo);
                if (XD == DialogResult.Yes)
                {

                    dbContent.Lop.Remove(XoaLop);
                    dbContent.SaveChanges();

                    loadForm();
                    loadDGV();

                    MessageBox.Show($"Xóa lop {XoaLop.MaLop} thành công!", "Thông báo");
                }
                else
                {
                    MessageBox.Show($"Xóa lop {txtMaLop } thất bại", "Thông báo");
                }

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (CheckDataInput() == true)
            {
                LockTxt(false);
                flag = false;
                SetButton(false);
            }
            else
            {
                LockTxt(true);
                MessageBox.Show("Vui lòng chọn Khoa để sửa.");
            }
            /*if (CheckDataInput() == true)
            {
                Lop SuaLop = dbContent.Lop.Where(p => p.MaLop == txtMaLop.Text).FirstOrDefault();
                if (SuaLop!=null)
                {
                    SuaLop.MaKhoa = cbbMaKhoa.SelectedValue.ToString();
                    SuaLop.MaLop = txtMaLop.Text;
                    SuaLop.TenLop = txtTenLop.Text;
                    SuaLop.SiSo = Convert.ToInt32(txtSiSo.Text);
                    SuaLop.MaKhoaHoc = cbbMaKhoaHoc.SelectedValue.ToString();

                    dbContent.Lop.AddOrUpdate(SuaLop);
                    dbContent.SaveChanges();

                    loadForm();
                    loadDGV();
                    MessageBox.Show($"Sửa Lop {SuaLop.MaLop} thành công", "Thông báo !");

                }
                else
                {
                    MessageBox.Show($"Sửa lớp {txtMaLop.Text} thất bại ", "Thông báo !");
                }

            }*/
        }

        private void btnQuayVe_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvQLLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {

                //cho phep chon 1 dong
                dgvQLLop.CurrentRow.Selected = true;
                cbbMaKhoa.SelectedIndex = cbbMaKhoa.FindString(dgvQLLop.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
                txtMaLop.Text = dgvQLLop.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                txtTenLop.Text = dgvQLLop.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                txtSiSo.Text = dgvQLLop.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                cbbMaKhoaHoc.SelectedIndex = cbbMaKhoaHoc.FindString(dgvQLLop.Rows[e.RowIndex].Cells[4].FormattedValue.ToString());

        }
        private void cbbMaKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtSiSo.Clear();
            cbbMaKhoaHoc.SelectedIndex = -1;
        }

        private void txtSiSo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // Nếu bạn muốn, bạn có thể cho phép nhập số thực với dấu chấm
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LockTxt(true);
            SetButton(true);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            LockTxt(true);
            string ms = txtMaLop.Text;
            string ht = txtTenLop.Text;
            if (flag == true)
            {
                if (CheckDataInput() == true)
                {
                    if (CheckMaLop(txtMaLop.Text) == -1)
                    {
                        Lop newLop = new Lop();
                        newLop.MaKhoa = cbbMaKhoa.SelectedValue.ToString();
                        newLop.MaLop = txtMaLop.Text;
                        newLop.TenLop = txtTenLop.Text;
                        newLop.SiSo = Convert.ToInt32(txtSiSo.Text);
                        newLop.MaKhoaHoc = cbbMaKhoaHoc.SelectedValue.ToString();

                        dbContent.Lop.AddOrUpdate(newLop);
                        dbContent.SaveChanges();

                        loadForm();
                        loadDGV();
                        MessageBox.Show($"Thêm lớp {newLop.MaLop} thành công", "Thông báo !");

                    }
                    else
                    {
                        MessageBox.Show($"Thêm lớp {txtMaLop.Text} thất bại ", "Thông báo !");
                    }

                }
            }
            else
            {
                if (CheckDataInput() == true)
                {
                    Lop SuaLop = dbContent.Lop.Where(p => p.MaLop == txtMaLop.Text).FirstOrDefault();
                    if (SuaLop != null)
                    {
                        SuaLop.MaKhoa = cbbMaKhoa.SelectedValue.ToString();
                        SuaLop.MaLop = txtMaLop.Text;
                        SuaLop.TenLop = txtTenLop.Text;
                        SuaLop.SiSo = Convert.ToInt32(txtSiSo.Text);
                        SuaLop.MaKhoaHoc = cbbMaKhoaHoc.SelectedValue.ToString();

                        dbContent.Lop.AddOrUpdate(SuaLop);
                        dbContent.SaveChanges();

                        loadForm();
                        loadDGV();
                        MessageBox.Show($"Sửa Lớp {SuaLop.MaLop} thành công", "Thông báo !");

                    }
                    else
                    {
                        MessageBox.Show($"Sửa lớp {txtMaLop.Text} thất bại ", "Thông báo !");
                    }

                }
            }

            SetButton(true);
            SetNull();
        }

    }
}
