using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn.Model;
using Microsoft.Reporting.WinForms;
namespace DoAn
{
    public partial class ReportSVLop : Form
    {
        DbContentSinhVien dbSinhVien = new DbContentSinhVien();
        public ReportSVLop()
        {
            InitializeComponent();
        }

        private void ReportSVLop_Load(object sender, EventArgs e)
        {
            List<SinhVien> listSinhVien = dbSinhVien.SinhVien.ToList();
            List<Lop> listLop = dbSinhVien.Lop.ToList();
            FillDataCBB(listLop);
            this.reportViewer1.RefreshReport();
            
        }
        private void FillDataCBB(List<Lop> listLop)
        {
            cbbLop.DataSource = listLop;
            cbbLop.DisplayMember = "MaLop";
            cbbLop.ValueMember = "MaLop";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lop lop = dbSinhVien.Lop.FirstOrDefault(p => p.MaLop == cbbLop.Text);
            List<SinhVien> SinhVien = dbSinhVien.SinhVien.Where(p => p.MaLop == cbbLop.Text).ToList();//sua thanh cbb ma lop
            List<StudentReport> studentReports = new List<StudentReport>();


            foreach (var item in SinhVien)
            {
                StudentReport studentReport = new StudentReport();

                studentReport.MSSV = item.MSSV;
                studentReport.Ho = item.Ho;
                studentReport.Ten = item.Ten;
                studentReport.NamSinh = (DateTime)item.NamSinh;
                studentReport.CCCD = item.CCCD;
                studentReport.Dantoc = item.DanToc;

                studentReport.GioiTinh = item.GioiTinh;

                studentReport.Email = item.Email;
                studentReport.SDT = item.SDT;
                studentReport.TrangThai = item.TrangThai;


                studentReports.Add(studentReport);
            }
            if (lop == null || SinhVien.Count() == 0)
            {
                MessageBox.Show("Không tìm thấy thông tinh sinh viên theo lớp ");
                return;
            }
            this.reportViewer1.LocalReport.ReportPath = "ReportSVLop.rdlc";
            var reportDataSource = new ReportDataSource("DataSetSVLop", studentReports);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            //this.reportViewer1.LocalReport.DisplayName = "DANH SÁCH SINH VIÊN THEO LỚP";
            this.reportViewer1.RefreshReport();
        }
    }
}
