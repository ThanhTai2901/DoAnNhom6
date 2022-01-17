using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn.Model;
using System.Security.Cryptography;
namespace DoAn
{
    public partial class FormDangNhap : Form
    {
        string tendangnhap = "", matkhau = "", quyen = "";
        DbContentSinhVien dbContent = new DbContentSinhVien();
        public FormDangNhap()
        {
            InitializeComponent();
        }
        public FormDangNhap(string tendangnhap, string matkhau, string quyen)
        {
            InitializeComponent();
            this.tendangnhap = tendangnhap;
            this.matkhau = matkhau;
            this.quyen = quyen;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            List<TaiKhoan> listTaiKhoan = dbContent.TaiKhoan.ToList();
            txtTenDangNhap.Text = Properties.Settings.Default.taikhoan;
            txtMatKhau.Text = Properties.Settings.Default.matkhau;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnDangNhap_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void cbSave_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"data source=LAPTOP-DENGDHRK\MSSQLSERVER03;initial catalog=QLSV;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
/*            try
            {*/
                conn.Open();
                string tk = txtTenDangNhap.Text;
                string mk = txtMatKhau.Text;
                string sql = "select *from TaiKhoan where ID='" + tk + "'and MatKhau='" + mk + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter("select *from TaiKhoan where ID='" + tk + "'and MatKhau='" + mk + "'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                SqlDataReader dta = cmd.ExecuteReader();
                if (txtTenDangNhap.Text == "" || txtMatKhau.Text == "")
                {
                    MessageBox.Show("Phải nhập đủ thông tin", "Thông báo");
                }
                else
                {
                    if (dta.Read() == true)
                    {
                        if (tk != "Admin" && tk != "TaiChinh" && tk != "CTSV")
                        {
                            MessageBox.Show("Đăng nhập SV thành công", "Thông báo");
                            FormChucNangSV formSV = new FormChucNangSV(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString());
                            formSV.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show($"Đăng nhập thành công", "Thông báo");
                            FormHeThong formHeThong = new FormHeThong(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString());
                            formHeThong.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu", "Thông báo");
                    }
                }
/*            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi kết nối SQL");
            }*/
            if (cbSave.Checked == true)
            {
                Properties.Settings.Default.taikhoan = txtTenDangNhap.Text;
                Properties.Settings.Default.matkhau = txtMatKhau.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.taikhoan = "";
                Properties.Settings.Default.matkhau = "";
                Properties.Settings.Default.Save();
            }
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            Close();
        }

    }
}