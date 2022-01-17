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
    public partial class QLDiem : Form
    {
        DbContentSinhVien dbContent = new DbContentSinhVien();
        string flag;
        public QLDiem()
        {
            InitializeComponent();
        }
        private void loadForm()
        {
        }
        public void LockTxt(bool val)
        {
            cbbMaKhoa.Enabled = !val;
            cbbMaLop.Enabled = !val;
            cbbMSSV.Enabled = !val;
            cbbHocKi.Enabled = !val;
            cbbMaMon.Enabled = !val;
            cbbHocKi.Enabled= !val;
            txtDiemQuaTrinh.Enabled = !val;
            txtDiemThi.Enabled = !val;

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
        private void QLDiem_Load(object sender, EventArgs e)
        {
            this.sinhVienTableAdapter.Fill(this.dataSetQLDiem.SinhVien);
            this.lopTableAdapter.Fill(this.dataSetQLDiem.Lop);
            this.khoaTableAdapter.Fill(this.dataSetQLDiem.Khoa);
            ketnoi = new SqlConnection(chuoiketnoi);

            List<SinhVien> listSinhVien = dbContent.SinhVien.ToList();
            List<Lop> listLop = dbContent.Lop.ToList();
            List<Khoa> listKhoa = dbContent.Khoa.ToList();
            List<Diem> listDiem = dbContent.Diem.ToList();
            List<MonHoc> listMonHoc = dbContent.MonHoc.ToList();
            List<HocKy> listHocKy = dbContent.HocKy.ToList();

            labelShow.Text = "";

            SetButton(true);
            LockTxt(true);

            FillDataCBB(listMonHoc);
            FillDataCBB_HocKi(listHocKy);
            FillDataDGV_Diem(listDiem);
            cbbMaKhoa.SelectedIndex = -1;
        }

        private void FillDataCBB_HocKi(List<HocKy> listHocKy)
        {
            cbbHocKi.DataSource = listHocKy;         // cbb hoc ky
            cbbHocKi.DisplayMember = "MaHocKy";
            cbbHocKi.ValueMember = "MaHocKy";
        }

        private void loadDGV()              //load data
        {
            List<Diem> newDiem = dbContent.Diem.ToList();
            FillDataDGV_Diem(newDiem);

        }
        private void FillDataDGV_Diem(List<Diem> listDiem)           //load data vao dgv
        {
            dgvQLDiem.Rows.Clear();
            foreach (var item in listDiem)
            {
                int newRow = dgvQLDiem.Rows.Add();        //them 1 dong moi
                dgvQLDiem.Rows[newRow].Cells[0].Value = item.MSSV;
                dgvQLDiem.Rows[newRow].Cells[1].Value = item.MaMon;
                dgvQLDiem.Rows[newRow].Cells[2].Value = item.DiemQuaTrinh;
                dgvQLDiem.Rows[newRow].Cells[3].Value = item.DiemThi;
                dgvQLDiem.Rows[newRow].Cells[4].Value = (item.DiemThi + item.DiemQuaTrinh) / 2;
                dgvQLDiem.Rows[newRow].Cells[5].Value = item.MaHocKy;
            }
        }
        private void FillDataCBB(List<MonHoc> listMonHoc)           //load mã và tên các môn học từ ds môn vào cbb
        {
            cbbMaMon.DataSource = listMonHoc;         // cbb ma mon
            cbbMaMon.DisplayMember = "MaMon";
            cbbMaMon.ValueMember = "MaMon";
        }
        private int CheckMSSV(string idMaDiem)        // kt ma so sv
        {
            for (int i = 0; i < dgvQLDiem.Rows.Count; i++)
            {
                if (dgvQLDiem.Rows[i].Cells[0].Value != null)
                {
                    if (dgvQLDiem.Rows[i].Cells[0].Value.ToString() == idMaDiem)
                    {
                        return i;                       
                    }
                }
            }
            return -1;  //-1 khi khong tim thay sinh vien co ma so moi
        }
        private int CheckMaMon(string MaMon)        // chua xong    
        {
            for (int i = 0; i < dgvQLDiem.Rows.Count; i++)
            {
                if (dgvQLDiem.Rows[i].Cells[0].Value != null)
                {
                    if ((string)dgvQLDiem.Rows[i].Cells[0].Value == temp)
                    {
                        if (dgvQLDiem.Rows[i].Cells[1].Value.ToString() == MaMon)
                        {
                            return i;
                        }
                    }
                }
            }
            return -1;
        }
        private int CheckMaHocKy(string idMaHK)        // kt ma so sv
        {
            for (int i = 0; i < dgvQLDiem.Rows.Count; i++)
            {
                if (dgvQLDiem.Rows[i].Cells[0].Value != null)
                {
                    if ((string)dgvQLDiem.Rows[i].Cells[0].Value == temp)
                    {
                        if (dgvQLDiem.Rows[i].Cells[5].Value.ToString() == idMaHK)
                        {
                            return i;
                        }
                    }
                }
            }
            return -1;  //-1 khi khong tim thay hoc ky co ma so moi
        }
        private bool CheckDataInput()           // kiem tra du lieu dau vao
        {
            if (txtDiemQuaTrinh.Text == "" || txtDiemThi.Text == "")
            {
                MessageBox.Show("Dữ liệu điểm phải là số !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if ((string)cbbMSSV.SelectedValue == "" || (string)cbbMaMon.SelectedValue == "" || (string)cbbHocKi.SelectedValue == "")
            {
                MessageBox.Show("Dữ liệu không có MSSV hoặc Mã môn học hoặc Mã Học Kỳ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if(Convert.ToDouble(txtDiemQuaTrinh.Text) < 0 || Convert.ToDouble(txtDiemQuaTrinh.Text) > 10
                ||  Convert.ToDouble(txtDiemThi.Text) < 0 || Convert.ToDouble(txtDiemThi.Text) >10 )
            {
                MessageBox.Show("Giá trị điểm không hợp lệ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        //C
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            labelShow.Text = "Hiện đang chọn chức năng thêm";
            cbbMSSV.Focus();
            cbbMaMon.Focus();
            flag = "them";
            LockTxt(false);
            SetButton(false);
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            labelShow.Text = "Hiện đang chọn chức năng sửa";
            cbbMSSV.Focus();
            cbbMaMon.Focus();
            flag = "sua";
            LockTxt(false);
            SetButton(false);
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            labelShow.Text = "Hiện đang chọn chức năng xóa";
            cbbMSSV.Focus();
            cbbMaMon.Focus();
            flag = "xoa";
            LockTxt(false);
            SetButton(false);

        }
        string temp;
        private void dgvQLDiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvQLDiem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    //cho phep chon 1 dong
                    dgvQLDiem.CurrentRow.Selected = true;
                    cbbMaMon.SelectedIndex = cbbMaMon.FindString(dgvQLDiem.Rows[e.RowIndex].Cells[1].FormattedValue.ToString());
                    txtDiemQuaTrinh.Text = dgvQLDiem.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                    txtDiemThi.Text = dgvQLDiem.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                    cbbHocKi.SelectedIndex = cbbHocKi.FindString(dgvQLDiem.Rows[e.RowIndex].Cells[5].FormattedValue.ToString());
                    //do du lieu ve cbb MSSV
                    cbbMSSV.Items.Clear();
                    cbbMSSV.Items.Add(dgvQLDiem.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
                    cbbMSSV.SelectedItem = dgvQLDiem.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                    cbbMSSV.SelectedValue = dgvQLDiem.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();

                    cbbMaMon.SelectedItem = dgvQLDiem.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
 //                   string tempMSSV = cbbMSSV.SelectedItem.ToString();
                    temp = cbbMSSV.SelectedItem.ToString();

                    cbbMaKhoa.SelectedIndex = -1;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void cbbMSSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            temp = cbbMSSV.SelectedItem.ToString();

        }
        private void cbbMaMon_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnQuayVe_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtDiemQuaTrinh_KeyPress(object sender, KeyPressEventArgs e)
        {
                // Xác thực rằng phím vừa nhấn không phải CTRL hoặc không phải dạng số
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

        private void txtDiemThi_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Xác thực rằng phím vừa nhấn không phải CTRL hoặc không phải dạng số
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (flag == "them")
            {
                if (CheckDataInput() == true)       // check đúng kiểu dữ liệu
                {
                    if (CheckMSSV(cbbMSSV.SelectedItem.ToString()) == -1)        // sv chưa có trong danh s
                    {
                        Diem newDiem = new Diem();
                        newDiem.MSSV = cbbMSSV.SelectedItem.ToString();
                        newDiem.MaMon = cbbMaMon.SelectedValue.ToString();
                        newDiem.DiemQuaTrinh = Convert.ToDouble(txtDiemQuaTrinh.Text);
                        newDiem.DiemThi = Convert.ToDouble(txtDiemThi.Text);
                        newDiem.DiemTongKet = (Convert.ToDouble(txtDiemThi.Text) + Convert.ToDouble(txtDiemQuaTrinh.Text)) / 2;
                        newDiem.MaHocKy = cbbHocKi.SelectedValue.ToString();
                        if (newDiem.DiemTongKet < 10)
                        {
                            newDiem.DiemChu = "A";
                        }
                        if (newDiem.DiemTongKet <= 8.4)
                        {
                            newDiem.DiemChu = "B+";
                        }
                        if (newDiem.DiemTongKet <= 7.7)
                        {
                            newDiem.DiemChu = "B";
                        }
                        if (newDiem.DiemTongKet <= 6.9)
                        {
                            newDiem.DiemChu = "C+";
                        }
                        if (newDiem.DiemTongKet <= 6.2)
                        {
                            newDiem.DiemChu = "C";
                        }
                        if (newDiem.DiemTongKet <= 5.4)
                        {
                            newDiem.DiemChu = "D+";
                        }
                        if (newDiem.DiemTongKet <= 4.7)
                        {
                            newDiem.DiemChu = "D";
                        }
                        if (newDiem.DiemTongKet <= 3.9)
                        {
                            newDiem.DiemChu = "F+";
                        }
                        if (newDiem.DiemTongKet <= 2.9)
                        {
                            newDiem.DiemChu = "F";
                        }
                        // tinh diem tb
                        //dua data xuong db va luu
                        dbContent.Diem.AddOrUpdate(newDiem);
                        dbContent.SaveChanges();
                        loadForm();
                        loadDGV();
                        MessageBox.Show($"Thêm điểm thành công 1 !", "Thông báo");
                        // tinh diem tb
                        int tempSL = 0;
                        double tempDTB = 0;
                        for (int i = 0; i < dgvQLDiem.Rows.Count; i++)
                        {
                            if ((string)dgvQLDiem.Rows[i].Cells[0].Value == temp)
                            {
                                tempDTB = tempDTB + Convert.ToDouble(dgvQLDiem.Rows[i].Cells[4].Value);
                                tempSL++;
                                MessageBox.Show($"Check ma thanh cong 1 {tempSL} ");
                            }
                        }
                        tempDTB = tempDTB / tempSL;
                        DTB newDTB = new DTB();
                        newDTB.MSSV = cbbMSSV.SelectedItem.ToString();
                        newDTB.MaHocKy = cbbHocKi.SelectedValue.ToString();
                        newDTB.DTB1 = tempDTB;
                        dbContent.DTB.AddOrUpdate(newDTB);
                        dbContent.SaveChanges();
                        //reset data gird view
                    }
                    else                        // sv đã có trong ds...giờ check mã môn để k trùng môn học
                    {
                        if (CheckMaMon(cbbMaMon.SelectedValue.ToString()) == -1)
                        {
                            Diem newDiem = new Diem();
                            newDiem.MSSV = cbbMSSV.SelectedItem.ToString();
                            newDiem.MaMon = cbbMaMon.SelectedValue.ToString();
                            newDiem.DiemQuaTrinh = Convert.ToDouble(txtDiemQuaTrinh.Text);
                            newDiem.DiemThi = Convert.ToDouble(txtDiemThi.Text);
                            newDiem.DiemTongKet = (Convert.ToDouble(txtDiemThi.Text) + Convert.ToDouble(txtDiemQuaTrinh.Text)) / 2;
                            newDiem.MaHocKy = cbbHocKi.SelectedValue.ToString();
                            if (newDiem.DiemTongKet < 10)
                            {
                                newDiem.DiemChu = "A";
                            }
                            if (newDiem.DiemTongKet <= 8.4)
                            {
                                newDiem.DiemChu = "B+";
                            }
                            if (newDiem.DiemTongKet <= 7.7)
                            {
                                newDiem.DiemChu = "B";
                            }
                            if (newDiem.DiemTongKet <= 6.9)
                            {
                                newDiem.DiemChu = "C+";
                            }
                            if (newDiem.DiemTongKet <= 6.2)
                            {
                                newDiem.DiemChu = "C";
                            }
                            if (newDiem.DiemTongKet <= 5.4)
                            {
                                newDiem.DiemChu = "D+";
                            }
                            if (newDiem.DiemTongKet <= 4.7)
                            {
                                newDiem.DiemChu = "D";
                            }
                            if (newDiem.DiemTongKet <= 3.9)
                            {
                                newDiem.DiemChu = "F+";
                            }
                            if (newDiem.DiemTongKet <= 2.9)
                            {
                                newDiem.DiemChu = "F";
                            }
                            //dua data xuong db va luu
                            //dua data xuong db va luu
                            dbContent.Diem.AddOrUpdate(newDiem);
                            dbContent.SaveChanges();
                            MessageBox.Show($"Thêm điểm thành công 2 !", "Thông báo");
                            // tinh diem tb
                            double tempDTB = 0;
                            int tempSL = 0;
                            if (CheckMaHocKy(cbbHocKi.SelectedValue.ToString()) == -1)
                            {
                                tempDTB = tempDTB + Convert.ToDouble(dgvQLDiem.Rows[i].Cells[4].Value);
                                tempSL++;
                                MessageBox.Show($"Check ma thanh cong 1 {tempSL} ");
                                tempDTB = tempDTB / tempSL;
                                DTB newDTB = new DTB();
                                newDTB.MSSV = cbbMSSV.SelectedItem.ToString();
                                newDTB.MaHocKy = cbbHocKi.SelectedValue.ToString();
                                newDTB.DTB1 = tempDTB;
                                dbContent.DTB.AddOrUpdate(newDTB);
                                dbContent.SaveChanges();
                                loadForm();
                                loadDGV();
                            }
                            else
                            {
                                tempDTB = tempDTB + Convert.ToDouble(dgvQLDiem.Rows[i].Cells[4].Value);
                                tempSL++;
                                MessageBox.Show($"Check ma thanh cong 2 {tempSL} ");
                                /// bug
                                tempDTB = tempDTB / tempSL;
                                DTB updateDTB = dbContent.DTB.Where(p => p.MSSV == cbbMSSV.SelectedItem.ToString() && p.MaHocKy == cbbHocKi.SelectedValue.ToString()).FirstOrDefault();
                                updateDTB.MSSV = cbbMSSV.SelectedItem.ToString();
                                updateDTB.MaHocKy = cbbHocKi.SelectedValue.ToString();
                                updateDTB.DTB1 = tempDTB;
                                dbContent.DTB.AddOrUpdate(updateDTB);
                                dbContent.SaveChanges();
                                loadForm();
                                loadDGV();
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Thất bại , SV đã có điểm môn này ", "Thông báo");
                        }
                    }
                }
            }
            if(flag == "sua")
            {
                if (CheckDataInput() == true)
                {
                    // lay sv dua vao ma so sv
                    Diem updateDiem = dbContent.Diem.Where(p => p.MaMon == cbbMaMon.SelectedValue.ToString() && p.MSSV == cbbMSSV.SelectedItem.ToString()).FirstOrDefault();
                    updateDiem.MSSV = cbbMSSV.SelectedItem.ToString();
                    updateDiem.MaMon = cbbMaMon.SelectedValue.ToString();
                    updateDiem.DiemQuaTrinh = Convert.ToDouble(txtDiemQuaTrinh.Text.ToString());
                    updateDiem.DiemThi = Convert.ToDouble(txtDiemThi.Text.ToString());
                    updateDiem.DiemTongKet = (Convert.ToDouble(txtDiemThi.Text) + Convert.ToDouble(txtDiemQuaTrinh.Text)) / 2;
                    updateDiem.MaHocKy = cbbHocKi.SelectedValue.ToString();
                    if (updateDiem.DiemTongKet < 10)
                    {
                        updateDiem.DiemChu = "A";
                    }
                    if (updateDiem.DiemTongKet <= 8.4)
                    {
                        updateDiem.DiemChu = "B+";
                    }
                    if (updateDiem.DiemTongKet <= 7.7)
                    {
                        updateDiem.DiemChu = "B";
                    }
                    if (updateDiem.DiemTongKet <= 6.9)
                    {
                        updateDiem.DiemChu = "C+";
                    }
                    if (updateDiem.DiemTongKet <= 6.2)
                    {
                        updateDiem.DiemChu = "C";
                    }
                    if (updateDiem.DiemTongKet <= 5.4)
                    {
                        updateDiem.DiemChu = "D+";
                    }
                    if (updateDiem.DiemTongKet <= 4.7)
                    {
                        updateDiem.DiemChu = "D";
                    }
                    if (updateDiem.DiemTongKet <= 3.9)
                    {
                        updateDiem.DiemChu = "F+";
                    }
                    if (updateDiem.DiemTongKet <= 2.9)
                    {
                        updateDiem.DiemChu = "F";
                    }
                    dbContent.Diem.AddOrUpdate(updateDiem);

                    dbContent.SaveChanges();
                    //reset data gird view
                    loadForm();
                    loadDGV();
                    MessageBox.Show($"Sửa điểm {updateDiem.MSSV} thành công!", "Thông báo");
                }
                double tempDTB = 0;
                int tempSL = 0;
                for (int i = 0; i < dgvQLDiem.Rows.Count; i++)
                {
                    if ((string)dgvQLDiem.Rows[i].Cells[0].Value == temp)
                    {
                        if ((string)dgvQLDiem.Rows[i].Cells[5].Value == cbbHocKi.SelectedValue.ToString())
                        {
                            if (CheckMaHocKy(cbbHocKi.SelectedValue.ToString()) == -1)
                            {
                                tempDTB = tempDTB + Convert.ToDouble(dgvQLDiem.Rows[i].Cells[4].Value);
                                tempSL++;
                                MessageBox.Show($"Check ma thanh cong 2 {tempSL} ");
                                ///
                                tempDTB = tempDTB / tempSL;
                                DTB newDTB = new DTB();
                                newDTB.MSSV = cbbMSSV.SelectedItem.ToString();
                                newDTB.MaHocKy = cbbHocKi.SelectedValue.ToString();
                                newDTB.DTB1 = tempDTB;
                                dbContent.DTB.AddOrUpdate(newDTB);
                                dbContent.SaveChanges();
                                loadForm();
                                loadDGV();
                            }
                            else
                            {
                                tempDTB = tempDTB + Convert.ToDouble(dgvQLDiem.Rows[i].Cells[4].Value);
                                tempSL++;
                                MessageBox.Show($"Check ma thanh cong 2 {tempSL} ");
                                ///
                                tempDTB = tempDTB / tempSL;
                                DTB updateDTB = dbContent.DTB.Where(p => p.MSSV == cbbMSSV.SelectedItem.ToString() && p.MaHocKy == cbbHocKi.SelectedValue.ToString()).FirstOrDefault();
                                updateDTB.MSSV = cbbMSSV.SelectedItem.ToString();
                                updateDTB.MaHocKy = cbbHocKi.SelectedValue.ToString();
                                updateDTB.DTB1 = tempDTB;
                                dbContent.DTB.AddOrUpdate(updateDTB);
                                dbContent.SaveChanges();
                            }
                        }
                    }
                }
            }
            if (flag =="xoa")
            {
                Diem XoaDiem = dbContent.Diem.Where(p => p.MaMon == cbbMaMon.SelectedValue.ToString() && p.MSSV == cbbMSSV.SelectedItem.ToString()).FirstOrDefault();
                if (XoaDiem != null)        // neu tra ve -1 thi sv chua co trong ds
                {
                    DialogResult XD = MessageBox.Show("Bạn có chắc muốn xóa điểm ? ", "Yes/No", MessageBoxButtons.YesNo);
                    if (XD == DialogResult.Yes)
                    {
                        dbContent.Diem.Remove(XoaDiem);
                        dbContent.SaveChanges();
                        loadForm();
                        loadDGV();
                        MessageBox.Show($"Xóa điểm sinh viên {cbbMSSV} thất thành công", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show($"Xóa điểm sinh viên {cbbMSSV} thất bại", "Thông báo");
                    }

                }
                else
                    MessageBox.Show($"SV {cbbMSSV} chua co diem", "Thông báo");
                double tempDTB = 0;
                int tempSL = 0;
                for (int i = 0; i < dgvQLDiem.Rows.Count; i++)
                {
                    if ((string)dgvQLDiem.Rows[i].Cells[0].Value == temp)
                    {
                        if ((string)dgvQLDiem.Rows[i].Cells[5].Value == cbbHocKi.SelectedValue.ToString())
                        {
                            tempDTB = tempDTB + Convert.ToDouble(dgvQLDiem.Rows[i].Cells[4].Value);
                            tempSL++;
                        }
                    }
                }
                if (tempSL == 0)
                {
                    DTB xoaDTB = dbContent.DTB.Where(p=>p.MSSV == cbbMSSV.SelectedItem.ToString() && p.MaHocKy == cbbHocKi.SelectedValue.ToString()).FirstOrDefault();
                    dbContent.DTB.Remove(xoaDTB);
                    dbContent.SaveChanges();
                }
                else
                {
                    tempDTB = tempDTB / tempSL;
                    DTB updateDTB = dbContent.DTB.Where(p => p.MSSV == cbbMSSV.SelectedItem.ToString() && p.MaHocKy == cbbHocKi.SelectedValue.ToString()).FirstOrDefault();
                    updateDTB.MSSV = cbbMSSV.SelectedItem.ToString();
                    updateDTB.MaHocKy = cbbHocKi.SelectedValue.ToString();
                    updateDTB.DTB1 = tempDTB;
                    dbContent.DTB.AddOrUpdate(updateDTB);
                    dbContent.SaveChanges();
                    MessageBox.Show($"Xóa sinh viên {XoaDiem.MSSV} thành công!", "Thông báo");
                }
            }
            SetButton(true);
            LockTxt(true);
            labelShow.Text = "";
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetButton(true);
            LockTxt(true);
            labelShow.Text = "";
        }
    }
}
