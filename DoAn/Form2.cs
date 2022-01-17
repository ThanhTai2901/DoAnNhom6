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
    public partial class FormHeThong : Form
    {
        string tendangnhap = "", matkhau = "", quyen = "";
        public FormHeThong()
        {
            InitializeComponent();
        }
        public FormHeThong(string tendangnhap, string matkhau, string quyen)
        {
            InitializeComponent();
            this.tendangnhap = tendangnhap;
            this.matkhau = matkhau;
            this.quyen = quyen;
        }
        private void hệThốngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void xemThôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýĐiểmToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (quyen == "Admin")
            {
                QLDiem formQLDiem = new QLDiem();
                formQLDiem.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }


        private void Form1_Load(object sender, EventArgs e)
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

        private void tìmKiếmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (quyen == "Admin" || quyen == "CTSV")
            {
                MessageBox.Show($"Đăng nhập {quyen} thành công", "Thông báo");
                QLTimKiem formTimKiem = new QLTimKiem();
                formTimKiem.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (quyen == "Admin"||quyen=="CTSV")
            { 
            FormQLSinhViencs formQLSinhVien = new FormQLSinhViencs();
            formQLSinhVien.ShowDialog();
            }
        }

        private void quảnLýĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (quyen == "Admin" || quyen == "CTSV")
            {
                QLMonHoc formQLMonHoc = new QLMonHoc();
                formQLMonHoc.ShowDialog();
            }
        }

        private void quảnLýKhoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (quyen == "Admin")
            {
                QLKhoa formQLKhoa = new QLKhoa();

                formQLKhoa.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void xemTheoMSSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XemDiemTheoMSSVcs formXemDiemMSSV = new XemDiemTheoMSSVcs();
            formXemDiemMSSV.ShowDialog();
        }

        private void xemTheoMônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XemDiemTheoMon formXemDiemMon = new XemDiemTheoMon();
            formXemDiemMon.ShowDialog();
        }

        private void xemTấtCảSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void xemThôngTinToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void xemSinhViênTheoKhoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportSV xemSVTheoKhoa = new ReportSV();
            xemSVTheoKhoa.ShowDialog();
        }

        private void thoátToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DialogResult XD = MessageBox.Show("Bạn có chắc muốn thoát ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (XD == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void lớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (quyen == "Admin")
            {
                QLLop formQLLop = new QLLop();

                formQLLop.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

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

        private void bXHToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BXH formBXH = new BXH();
            formBXH.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Quyen la : {quyen}");
        }

        private void reportLopToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void xemHọcPhíSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLHocPhi HocPhiSV = new QLHocPhi();
            HocPhiSV.ShowDialog();
        }

        private void reportSinhVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportSVLop reportSVLop = new ReportSVLop();
            reportSVLop.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReportSVKhoa reportSVKhoa = new ReportSVKhoa();
            reportSVKhoa.ShowDialog();
        }

        private void đăngKíMônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DangKiMon dangKiMon = new DangKiMon();
            dangKiMon.ShowDialog();
        }

        private void bXHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BXH bxh = new BXH();
            bxh.ShowDialog();
        }
    }
}
