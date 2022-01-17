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
    public partial class FormChucNangSV : Form
    {
        string tendangnhap = "", matkhau = "", quyen = "";
        public FormChucNangSV()
        {
            InitializeComponent();
        }
        public FormChucNangSV (string tendangnhap, string matkhau, string quyen)
        {
            InitializeComponent();
            this.tendangnhap = tendangnhap;
            this.matkhau = matkhau;
            this.quyen = quyen;
        }
        DataTable dt = new DataTable();
        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"data source=LAPTOP-DENGDHRK\MSSQLSERVER03;initial catalog=QLSV;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            try
            {
                conn.Open();
                string tk = tendangnhap;
                string mk = matkhau;
                string quyen1 = quyen;
                string sql = "select *from TaiKhoan where ID='" + tk + "'and MatKhau='" + mk + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter("select *from TaiKhoan where ID='" + tk + "'and MatKhau='" + mk + "'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                SqlDataReader dta = cmd.ExecuteReader();

                DoiMatKhau formDoiMatKhau = new DoiMatKhau(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString());
                formDoiMatKhau.ShowDialog(); ;
            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi kết nối SQL");
            }
        }
        private void sửaTTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"data source=LAPTOP-DENGDHRK\MSSQLSERVER03;initial catalog=QLSV;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            try
            {
                conn.Open();
                string tk = tendangnhap;
                string mk = matkhau;
                string quyen1 = quyen;
                string sql = "select *from TaiKhoan where ID='" + tk + "'and MatKhau='" + mk + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter("select *from TaiKhoan where ID='" + tk + "'and MatKhau='" + mk + "'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                SqlDataReader dta = cmd.ExecuteReader();

                FormQLSinhViencs formQLSinhVien = new FormQLSinhViencs(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString());
                formQLSinhVien.ShowDialog();
            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi kết nối SQL");
            }
            

            
        }

        private void xemĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"data source=LAPTOP-DENGDHRK\MSSQLSERVER03;initial catalog=QLSV;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            try
            {
                conn.Open();
                string tk = tendangnhap;
                string mk = matkhau;
                string quyen1 = quyen;
                string sql = "select *from TaiKhoan where ID='" + tk + "'and MatKhau='" + mk + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter("select *from TaiKhoan where ID='" + tk + "'and MatKhau='" + mk + "'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                SqlDataReader dta = cmd.ExecuteReader();

                XemDiemTheoMSSVcs formXemDiemMSSV = new XemDiemTheoMSSVcs(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString());
                formXemDiemMSSV.ShowDialog();
            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi kết nối SQL");
            }
        }

        private void đăngKíMônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"data source=LAPTOP-DENGDHRK\MSSQLSERVER03;initial catalog=QLSV;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
/*            try
            {*/
                conn.Open();
                string tk = tendangnhap;
                string mk = matkhau;
                string quyen1 = quyen;
                string sql = "select *from TaiKhoan where ID='" + tk + "'and MatKhau='" + mk + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter("select *from TaiKhoan where ID='" + tk + "'and MatKhau='" + mk + "'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                SqlDataReader dta = cmd.ExecuteReader();
                DangKiMon dangKiMon = new DangKiMon(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString());
                dangKiMon.ShowDialog();
/*            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi kết nối SQL");
            }*/

        }

        private void họcPhíToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"QUyen la :{quyen} ");
        }

        private void FormChucNangSV_Load(object sender, EventArgs e)
        {

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult XD = MessageBox.Show("Bạn có chắc muốn đăng xuất ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (XD == DialogResult.Yes)
            {
                Close();
            }
        }

    }
}
