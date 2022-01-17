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
    public partial class FormQLSinhViencs : Form
    {
        DbContentSinhVien dbContent = new DbContentSinhVien();      //Gọi d
        string tendangnhap = "", matkhau = "", quyen = "";
        string flag;
        public FormQLSinhViencs()
        {
            InitializeComponent();

        }
        public FormQLSinhViencs(string tendangnhap, string matkhau, string quyen)
        {
            InitializeComponent();
            this.tendangnhap = tendangnhap;
            this.matkhau = matkhau;
            this.quyen = quyen;
        }
        public void LockTxt(bool val)       // Khóa hết Textbox và ComboBox trước khi chọn nút chức năng
        {
            txtMa.ReadOnly = val;
            txtHo.ReadOnly = val;
            txtTen.ReadOnly = val;
            txtCCCD.ReadOnly = val;
            txtDanToc.ReadOnly = val;
            rdoNam.Enabled = !val;
            rdoNu.Enabled = !val;
            dtpNgaySinh.Enabled = !val;
            txtSDT.ReadOnly = val;
            txtEmail.ReadOnly = val;
            txtTrangThai.ReadOnly = val;
            cbbMaKhoa.Enabled = !val;
            cbbMaLop.Enabled = !val;
            if(quyen=="SV")         // khi dang nhap bang acc SV , chọn các nút chức năng thì k đổi được Mã lớp , Mã khoa
            {
                cbbMaKhoa.Enabled = val;
                cbbMaLop.Enabled = val;
            }
            else
            {
                cbbMaKhoa.Enabled = !val;
                cbbMaLop.Enabled = !val;
            }
        }
        public void SetNull()
        {
            txtMa.Text = "";
            txtTen.Text = "";
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
        private void FormQLSinhViencs_Load(object sender, EventArgs e)          //load form
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            this.lopTableAdapter.Fill(this.dataSetQLDiem.Lop);
            this.khoaTableAdapter.Fill(this.dataSetQLDiem.Khoa);
            List<SinhVien> listSinhVien = dbContent.SinhVien.ToList();
            List<Khoa> listKhoa = dbContent.Khoa.ToList();;
            FillDataDGV(listSinhVien);
            txtMa.Focus();
            SetNull();
            SetButton(true);
            LockTxt(true);
            labelShow.Text = "";
            if (quyen=="SV")
            { 
                btnThem.Visible = false;
                btnXoa.Visible = false;
                btnQuayVe.Visible = false;

                txtMa.Enabled = false;
                txtTrangThai.Enabled = false;
                cbbMaKhoa.Enabled = false;  
                cbbMaLop.Enabled = false;   
            }    
        }
        // Cac ham load va check data
        private void loadDGV()              //load data
        {
            List<SinhVien> newSinhVien = dbContent.SinhVien.ToList();
            FillDataDGV(newSinhVien);
        }
        private void loadForm()
        {

            txtMa.Clear();
            txtHo.Clear();
            txtTen.Clear();
            txtCCCD.Clear();
            txtDanToc.Clear();
            txtEmail.Clear();

            txtSDT.Clear();
            txtTong.Clear();
            txtTrangThai.Clear();
            

        }
        private void FillDataDGV(List<SinhVien> listSinhVien)           //load data vao dgv
        {
            dgvSinhVien.Rows.Clear();
            if(quyen=="SV")
            {
                var newlistSV = (from x in dbContent.SinhVien
                                 where
                                 x.MSSV.CompareTo(tendangnhap)==0
                                 select new
                                 {
                                     MSSV = x.MSSV,
                                     Ho = x.Ho,
                                     Ten = x.Ten,
                                     CCCD = x.CCCD,
                                     DanToc = x.DanToc,
                                     Email = x.Email,
                                     SDT = x.SDT,
                                     TrangThai = x.TrangThai,
                                     NamSinh = x.NamSinh,
                                     GioiTinh = x.GioiTinh,
                                     MaLop = x.MaLop
                                 }
             ).Distinct().ToList();
                dgvSinhVien.Rows.Clear();
                foreach (var item in newlistSV)
                {
                    int newRow = dgvSinhVien.Rows.Add();
                    dgvSinhVien.Rows[newRow].Cells[0].Value = item.MSSV;
                    dgvSinhVien.Rows[newRow].Cells[1].Value = item.Ho;
                    dgvSinhVien.Rows[newRow].Cells[2].Value = item.Ten;
                    dgvSinhVien.Rows[newRow].Cells[3].Value = item.CCCD;
                    dgvSinhVien.Rows[newRow].Cells[4].Value = item.DanToc;
                    dgvSinhVien.Rows[newRow].Cells[5].Value = item.GioiTinh;
                    dgvSinhVien.Rows[newRow].Cells[6].Value = item.NamSinh;
                    dgvSinhVien.Rows[newRow].Cells[7].Value = item.SDT;
                    dgvSinhVien.Rows[newRow].Cells[8].Value = item.Email;
                    dgvSinhVien.Rows[newRow].Cells[9].Value = item.MaLop;
                    dgvSinhVien.Rows[newRow].Cells[10].Value = item.TrangThai;
                }
            }
            else
            {
                foreach (var item in listSinhVien)
                {
                    int newRow = dgvSinhVien.Rows.Add();        //them 1 dong moi
                    dgvSinhVien.Rows[newRow].Cells[0].Value = item.MSSV;
                    dgvSinhVien.Rows[newRow].Cells[1].Value = item.Ho;
                    dgvSinhVien.Rows[newRow].Cells[2].Value = item.Ten;
                    dgvSinhVien.Rows[newRow].Cells[3].Value = item.CCCD;
                    dgvSinhVien.Rows[newRow].Cells[4].Value = item.DanToc;
                    dgvSinhVien.Rows[newRow].Cells[5].Value = item.GioiTinh;
                    dgvSinhVien.Rows[newRow].Cells[6].Value = item.NamSinh;
                    dgvSinhVien.Rows[newRow].Cells[7].Value = item.SDT;
                    dgvSinhVien.Rows[newRow].Cells[8].Value = item.Email;
                    dgvSinhVien.Rows[newRow].Cells[9].Value = item.MaLop;
                    dgvSinhVien.Rows[newRow].Cells[10].Value = item.TrangThai;
                    txtTong.Text = (dgvSinhVien.Rows.Count - 1).ToString();
                }
            }
        }
        private int CheckIDSinhVien(string idSinhVien)        // kt ma so sv
        {
            for (int i = 0; i < dgvSinhVien.Rows.Count; i++)
            {
                if (dgvSinhVien.Rows[i].Cells[0].Value != null)
                {
                    if (dgvSinhVien.Rows[i].Cells[0].Value.ToString() == idSinhVien)
                    {
                        return i;
                    }
                }
            }
            return -1;  //-1 khi khong tim thay sinh vien co ma so moi
        }
        private bool CheckDataInput()           // kiem tra du lieu dau vao
        {
            if (txtMa.Text == "" || txtHo.Text == "" || txtTen.Text == "" || txtCCCD.Text == ""
                || txtDanToc.Text == "" || txtEmail.Text == "" || cbbMaLop.Text == "" || txtSDT.Text == ""
                || txtTrangThai.Text == "" )
            {
                MessageBox.Show("Thiếu dữ liệu ! Hãy nhập đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (txtMa.TextLength < 5)
            {
                MessageBox.Show("Mã số sinh viên phải có hơn 5 ký tự !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;        // neu khong co IF nao dung (k co loi)
        }
        //Cac ham chuc nang button

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnQuayVe_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            txtMa.Focus();
            flag ="them";
            labelShow.Text = "Hiện đang chọn chức năng thêm";
            LockTxt(false);
            SetButton(false);

        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            flag = "xoa";
            labelShow.Text = "Hiện đang chọn chức năng xóa";
            txtMa.Focus();
            LockTxt(false);
            SetButton(false);

        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            labelShow.Text = "Hiện đang chọn chức năng sửa";
            LockTxt(false);
                flag = "sua";
                SetButton(false);

        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvSinhVien.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    //cho phep chon 1 dong
                    dgvSinhVien.CurrentRow.Selected = true;

                    txtMa.Text = dgvSinhVien.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                    txtHo.Text = dgvSinhVien.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                    txtTen.Text = dgvSinhVien.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                    txtCCCD.Text = dgvSinhVien.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                    txtDanToc.Text = dgvSinhVien.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
                    if (dgvSinhVien.Rows[e.RowIndex].Cells[5].FormattedValue.ToString().Contains("Nam"))
                    {
                        rdoNam.Checked = true;
                    }
                    else
                    {
                        rdoNu.Checked = true;
                    }
                    //          dtpNgaySinh.Value = dgvSinhVien.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
                    txtSDT.Text = dgvSinhVien.Rows[e.RowIndex].Cells[7].FormattedValue.ToString();
                    txtEmail.Text = dgvSinhVien.Rows[e.RowIndex].Cells[8].FormattedValue.ToString();
                    cbbMaLop.SelectedItem = dgvSinhVien.Rows[e.RowIndex].Cells[9].FormattedValue.ToString();
                    txtTrangThai.Text = dgvSinhVien.Rows[e.RowIndex].Cells[10].FormattedValue.ToString();
                    DateTime dt = DateTime.Parse(dgvSinhVien.Rows[e.RowIndex].Cells[6].FormattedValue.ToString());
                    dtpNgaySinh.Value = dt;

                    cbbMaLop.Items.Clear();
                    cbbMaLop.Items.Add(dgvSinhVien.Rows[e.RowIndex].Cells[9].FormattedValue.ToString());
                    cbbMaLop.SelectedItem = dgvSinhVien.Rows[e.RowIndex].Cells[9].FormattedValue.ToString();
                    cbbMaLop.SelectedValue = dgvSinhVien.Rows[e.RowIndex].Cells[9].FormattedValue.ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        string chuoiketnoi = @"data source=LAPTOP-DENGDHRK\MSSQLSERVER03;initial catalog=QLSV;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i = 0;
        string sql;
        private void cbbMaKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbMaLop.Items.Clear();
            ketnoi.Open();
            sql = @"Select Khoa.MaKhoa,Lop.MaLop
                    From    Khoa Inner Join Lop
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

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Quyen la : {quyen}");
            MessageBox.Show($"twen dang nhap la  : {tendangnhap}");
        }

        private void btnQuayVe_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            LockTxt(true);
            string ms = txtMa.Text;
            string ht = txtTen.Text;
            if (flag =="them")
            {
                if (CheckDataInput() == true)
                {
                    if (CheckIDSinhVien(txtMa.Text) == -1)        // neu tra ve -1 thi sv chua co trong ds
                    {
                        SinhVien newSinhVien = new SinhVien();        // khoi tao sv moi
                        newSinhVien.MSSV = txtMa.Text;
                        newSinhVien.Ho = txtHo.Text;
                        newSinhVien.Ten = txtTen.Text;
                        newSinhVien.CCCD = txtCCCD.Text;
                        newSinhVien.Email = txtEmail.Text;
                        newSinhVien.SDT = txtSDT.Text;
                        newSinhVien.TrangThai = txtTrangThai.Text;
                        newSinhVien.MaLop = cbbMaLop.Text;
                        newSinhVien.DanToc = txtDanToc.Text;
                        newSinhVien.NamSinh = dtpNgaySinh.Value;
                        if (rdoNam.Checked == true)
                            newSinhVien.GioiTinh = "Nam";
                        else
                            newSinhVien.GioiTinh = "Nu";

                        //dua data xuong db va luu
                        dbContent.SinhVien.AddOrUpdate(newSinhVien);
                        dbContent.SaveChanges();
                        //reset data gird view
                        loadForm();
                        loadDGV();

                        MessageBox.Show($"Thêm sinh viên {txtMa.Text} thành công!", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show($"Thêm sinh viên {txtMa.Text} thất bại", "Thông báo");
                    }
                }
            }
            if(flag=="sua")
            {
                if (CheckDataInput() == true)
                {
                    if(quyen=="SV")
                    {
                        cbbMaKhoa.Enabled = false;
                        cbbMaLop.Enabled = false;
                    }    
                    // lay sv dua vao ma so sv
                    SinhVien updateSV = dbContent.SinhVien.Where(p => p.MSSV == txtMa.Text).FirstOrDefault();

                    if (updateSV != null)        // neu tra ve -1 thi sv chua co trong ds
                    {
                        updateSV.MSSV = txtMa.Text;
                        updateSV.Ho = txtHo.Text;
                        updateSV.Ten = txtTen.Text;
                        updateSV.DanToc = txtDanToc.Text;
                        updateSV.CCCD = txtCCCD.Text;
                        updateSV.Email = txtEmail.Text;
                        updateSV.MaLop = cbbMaLop.Text;
                        updateSV.TrangThai = txtTrangThai.Text;
                        updateSV.SDT = txtSDT.Text;
                        updateSV.NamSinh = dtpNgaySinh.Value;
                        if (rdoNam.Checked == true)
                            updateSV.GioiTinh = "Nam";
                        else
                            updateSV.GioiTinh = "Nu";

                        dbContent.SinhVien.AddOrUpdate(updateSV);

                        dbContent.SaveChanges();
                        //reset data gird view
                        loadForm();
                        loadDGV();

                        MessageBox.Show($"Sửa sinh viên {updateSV.MSSV} thành công!", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show($"Sửa sinh viên {txtMa} thất bại", "Thông báo");
                    }
                }
            }
            if(flag=="xoa")
            {
                SinhVien XoaSV = dbContent.SinhVien.Where(p => p.MSSV == txtMa.Text).FirstOrDefault();
                Diem XoaDiem = dbContent.Diem.Where(p => p.MSSV == txtMa.Text).FirstOrDefault();
                DTB XoaDTB = dbContent.DTB.Where(p => p.MSSV == txtMa.Text).FirstOrDefault();
                    DialogResult XD = MessageBox.Show("Bạn có chắc muốn xóa sinh viên ? ", "Yes/No", MessageBoxButtons.YesNo);
                    if (XD == DialogResult.Yes)
                    {
                        if(XoaSV!=null)
                            {
                                dbContent.SinhVien.Remove(XoaSV);
                            }
                        if(XoaDTB!=null)
                            {
                                dbContent.DTB.Remove(XoaDTB);
                            }
                        if(XoaDiem!=null)
                            {
                                dbContent.Diem.Remove(XoaDiem);
                            }

                        dbContent.SaveChanges();
                        loadForm();
                        loadDGV();

                        MessageBox.Show($"Xóa sinh viên {XoaSV.MSSV} thành công!", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show($"Xóa sinh viên {txtMa.Text} thất bại", "Thông báo");
                    }


            }
            SetButton(true);
            SetNull();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LockTxt(true);
            SetButton(true);
            labelShow.Text = "";
        }

        private void txtMa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Xác thực rằng phím vừa nhấn không phải CTRL hoặc không phải dạng số
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }
        private void txtHo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho nhập số và kí tự trắng , điều khiển
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtTen_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho nhập số và kí tự trắng , điều khiển
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtCCCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }
        private void txtDanToc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        private void txtTrangThai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void cbbMSSV_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
