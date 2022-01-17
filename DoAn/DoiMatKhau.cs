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
    public partial class DoiMatKhau : Form
    {
        DbContentSinhVien dbContent = new DbContentSinhVien();
        string tendangnhap = "", matkhau = "", quyen = "";
        public DoiMatKhau()
        {
            InitializeComponent();
        }

        private void DoiMatKhau_Load(object sender, EventArgs e)
        {
            List<TaiKhoan> listTaiKhoan = dbContent.TaiKhoan.ToList();
            btnDoiCaptcha_Click(sender, e);
            if(quyen!="Admin")
            {
                txtTenDangNhap.Enabled = false;
            }
            else { txtTenDangNhap.Enabled=true;}
            txtTenDangNhap.Text = tendangnhap;

        }
        public DoiMatKhau(string tendangnhap, string matkhau, string quyen)
        {
            InitializeComponent();
            this.tendangnhap = tendangnhap;
            this.matkhau = matkhau;
            this.quyen = quyen;
        }
        private string captchaText;
        public String randomString()
        {
            Random rnd = new Random();
            int number = rnd.Next(10000, 99999);
            string text = md5(number.ToString());
            text = text.ToUpper();
            text = text.Substring(0, 6);
            return text;

        }
        public static byte[] encryptData(String data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
        public static string md5(String data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();
        }
        private Bitmap drawImage(string txt, int w, int h)
        {
            Bitmap bt = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(bt);
            SolidBrush sb = new SolidBrush(Color.White);
            g.FillRectangle(sb, 0, 0, bt.Width, bt.Height);
            System.Drawing.Font font = new System.Drawing.Font("Tahoma", 30);
            sb = new SolidBrush(Color.Blue);
            g.DrawString(txt, font, sb, bt.Width / 2 - (txt.Length / 2) * font.Size, (bt.Height / 2) - font.Size);
            // Tạo hiệu ứng cho captcha
            // vẽ dấu chấm
            int count = 0;
            Random rand = new Random();
            while (count < 1000)
            {
                sb = new SolidBrush(Color.YellowGreen);
                g.FillEllipse(sb, rand.Next(0, bt.Width), rand.Next(0, bt.Height), 4, 2);
                count++;
            }
            count = 0;
            // vẽ đường gạch ngang
            while (count < 25)
            {
                g.DrawLine(new Pen(Color.Pink), rand.Next(0, bt.Width), rand.Next(0, bt.Height), rand.Next(0, bt.Width), rand.Next(0, bt.Height));
                count++;
            }
            // End tạo hiệu ứng
            return bt;
        }

        private void btnDoiCaptcha_Click(object sender, EventArgs e)
        {
            captchaText = this.randomString();
            txtXacNhanCaptcha.Text = "";
            // vẽ captcha lên panel 1
            panel1.BackgroundImage = drawImage(captchaText, panel1.Width, panel1.Height);
        }
        SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-ER788FD;Initial Catalog=QLSV;Integrated Security=True");
        private void btnDoi_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TaiKhoan WHERE ID=N'" + txtTenDangNhap.Text + "'AND MatKhau=N'" + txtMatKhau.Text + "'", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count.ToString() == "1")
            {
                if (txtXacNhanCaptcha.Text != captchaText)
                {
                    MessageBox.Show("Bạn đã nhập mã xác nhận không chính xác!", "Thông báo");
                }
                if (txtMatKhauMoi.Text == txtXacNhanMatKhau.Text && txtXacNhanCaptcha.Text == captchaText)
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("UPDATE TaiKhoan SET MatKhau =N'" + txtMatKhauMoi.Text + "'WHERE ID=N'" + txtTenDangNhap.Text + "' AND MatKhau=N'" + txtMatKhau.Text + "'", cn);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    MessageBox.Show("Đổi mật khẩu thành công !", "Thông báo !", MessageBoxButtons.OK);
                }
                else if (txtMatKhauMoi.Text == "") MessageBox.Show("Bạn chưa nhập mật khẩu mới");
                else if (txtMatKhauMoi.Text == "") MessageBox.Show("Bạn chưa nhập lại mật khẩu mới");
                else if (txtMatKhauMoi.Text != txtXacNhanMatKhau.Text) MessageBox.Show("Mật khẩu mới của bạn không trùng khớp");
            }
            else if (txtMatKhau.Text == "" || txtMatKhauMoi.Text == "" || txtXacNhanMatKhau.Text == "") MessageBox.Show("Bạn chưa nhập đủ thông tin đổi mật khẩu");
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
