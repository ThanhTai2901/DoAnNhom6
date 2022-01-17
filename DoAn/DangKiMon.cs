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
    public partial class DangKiMon : Form
    {
        DbContentSinhVien dbContent = new DbContentSinhVien();
        string tendangnhap = "", matkhau = "", quyen = "";
        public DangKiMon()
        {
            InitializeComponent();
        }
        public DangKiMon(string tendangnhap, string matkhau, string quyen)
        {
            InitializeComponent();
            this.tendangnhap = tendangnhap;
            this.matkhau = matkhau;
            this.quyen = quyen;
        }
        private void DangKiMon_Load(object sender, EventArgs e)
        {
            List<MonHoc> listDangKy = dbContent.MonHoc.ToList();
            List<SinhVien> listSinhVien = dbContent.SinhVien.ToList();
            List<SVDangKiMonHoc> listDangKiMon = dbContent.SVDangKiMonHoc.ToList();
            List<HocPhi> listHocPhi = dbContent.HocPhi.ToList();    
            FillDataDGV(listDangKiMon);
        }
        private void loadDGV()              //load data
        {
            List<SVDangKiMonHoc> newDangKiMon = dbContent.SVDangKiMonHoc.ToList();
            FillDataDGV(newDangKiMon);
        }
        private void FillDataDGV(List<SVDangKiMonHoc> listDangKiMon)
        {
            dgvDangky2.Rows.Clear();
                var newlistSV = (from x in dbContent.MonHoc
                                 from y in dbContent.SVDangKiMonHoc
                                 where
                                 (
                                 y.MSSV.CompareTo(tendangnhap) == 0 &&
                                 (x.MaMon.Contains(txtMaMonHoc.Text) || x.TenMon.Contains(txtMaMonHoc.Text)))
                                 && y.MaMon == x.MaMon
                                 select new
                                 {
                                     y.MaMon,
                                     x.TenMon,
                                     x.SoTiet,
                                     y.SoTinChi
                                 }
                 ).Distinct().ToList();
            foreach (var item in newlistSV)
            {
                int newRow = dgvDangky2.Rows.Add();
                dgvDangky2.Rows[newRow].Cells[1].Value = item.MaMon;
                dgvDangky2.Rows[newRow].Cells[2].Value = item.TenMon;
                dgvDangky2.Rows[newRow].Cells[3].Value = item.SoTiet;
                dgvDangky2.Rows[newRow].Cells[4].Value = item.SoTinChi;
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            int j = 0;
            for (int i = 0; i < dgvDangky2.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvDangky2.Rows[i].Cells[0].Value) == true)
                    {
                    j++;
                        SVDangKiMonHoc newDangKiMon = new SVDangKiMonHoc();
                        newDangKiMon.MSSV = tendangnhap;
                        newDangKiMon.MaMon = dgvDangky2.Rows[i].Cells[1].Value.ToString();
                        newDangKiMon.SoTinChi = Convert.ToInt32(dgvDangky2.Rows[i].Cells[4].Value);
                        dbContent.SVDangKiMonHoc.AddOrUpdate(newDangKiMon);
                        dbContent.SaveChanges();
                    }
                }
            MessageBox.Show($"Dang ki tren {j} mon hoc thanh cong");
            loadDGV();
            int tempTinChi = 0;
            for (int i = 0; i < dgvDangky2.Rows.Count; i++)
            {
                tempTinChi = tempTinChi + Convert.ToInt32(dgvDangky2.Rows[i].Cells[4].Value.ToString());
            }
            txtTongTinChi.Text = tempTinChi.ToString();
            long tempHocPhi = 0;
            tempHocPhi = tempTinChi * 900000;

            if (tempHocPhi == 0)
            {
                txtTongHocPhi.Text = "0 VND";
            }

            if (tempHocPhi.ToString().Length == 6)
            {
                txtTongHocPhi.Text = $"{tempHocPhi.ToString().Substring(0, 3)},{tempHocPhi.ToString().Substring(3, 3)},VND";
            }

            if (tempHocPhi.ToString().Length == 7)
            {
                txtTongHocPhi.Text = $"{tempHocPhi.ToString().Substring(0, 1)},{tempHocPhi.ToString().Substring(1, 3)},{tempHocPhi.ToString().Substring(3,3)},VND";
            }
            if (tempHocPhi.ToString().Length == 8)
            {
                txtTongHocPhi.Text = $"{tempHocPhi.ToString().Substring(0, 2)},{tempHocPhi.ToString().Substring(2, 3)},{tempHocPhi.ToString().Substring(5, 3)},VND";
            }
            HocPhi HocPhiSV = new HocPhi();
            HocPhiSV.MSSV = tendangnhap;
            HocPhiSV.MaHocPhi = tendangnhap;
            HocPhiSV.SoTien = tempHocPhi;
            dbContent.HocPhi.AddOrUpdate(HocPhiSV);
            dbContent.SaveChanges();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
                DialogResult XD = MessageBox.Show("Bạn có chắc muốn xóa  ? ", "Yes/No", MessageBoxButtons.YesNo);
                if (XD == DialogResult.Yes)
                {
                for (int i = 0; i < dgvDangky2.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean( dgvDangky2.Rows[i].Cells[0].Value)==true)
                        {
                        string MM = dgvDangky2.Rows[i].Cells[1].Value.ToString();
                        SVDangKiMonHoc xoaDangKiMon = dbContent.SVDangKiMonHoc.Where(p => p.MSSV == tendangnhap.ToString() && p.MaMon ==MM).FirstOrDefault();
                        dbContent.SVDangKiMonHoc.Remove(xoaDangKiMon);
                        dbContent.SaveChanges();
                        }
                }
                }
            loadDGV();
            int tempTinChi = 0;
            for (int i = 0; i < dgvDangky2.Rows.Count; i++)
            {
                tempTinChi = tempTinChi +Convert.ToInt32(dgvDangky2.Rows[i].Cells[4].Value.ToString());
            }
            txtTongTinChi.Text = tempTinChi.ToString();
            long tempHocPhi = 0;
            tempHocPhi = tempTinChi * 900000;

            if(tempHocPhi==0)
            {
                txtTongHocPhi.Text = "0 VND";
            }
            if (tempHocPhi.ToString().Length == 6)
            {
                txtTongHocPhi.Text = $"{tempHocPhi.ToString().Substring(0, 3)},{tempHocPhi.ToString().Substring(3, 3)},VND";
            }

            if (tempHocPhi.ToString().Length == 7)
            {
                txtTongHocPhi.Text = $"{tempHocPhi.ToString().Substring(0, 1)},{tempHocPhi.ToString().Substring(1, 3)},{tempHocPhi.ToString().Substring(3, 3)},VND";
            }
            if (tempHocPhi.ToString().Length == 8)
            {
                txtTongHocPhi.Text = $"{tempHocPhi.ToString().Substring(0, 2)},{tempHocPhi.ToString().Substring(2, 3)},{tempHocPhi.ToString().Substring(5, 3)},VND";
            }
            HocPhi suaHocPhiSV = dbContent.HocPhi.Where(p => p.MSSV == tendangnhap.ToString() && p.MaHocPhi == tendangnhap.ToString()).FirstOrDefault();
            suaHocPhiSV.MSSV = tendangnhap;
            suaHocPhiSV.MaHocPhi = tendangnhap;
            suaHocPhiSV.SoTien = tempHocPhi;
            dbContent.HocPhi.AddOrUpdate(suaHocPhiSV);
            dbContent.SaveChanges();
        }
        private void btnTim_Click(object sender, EventArgs e)
        {
            var newlistSV = (from x in dbContent.MonHoc
                             where
                             (
                             x.MaMon.Contains(txtMaMonHoc.Text) || x.TenMon.Contains(txtMaMonHoc.Text))
                             select new
                             {
                                 x.MaMon,
                                 x.TenMon,
                                 x.SoTiet,
                                 x.SoTinChi
                             }
             ).Distinct().ToList();
            dgvDangKi.Rows.Clear();
            foreach (var item in newlistSV)
            {
                int newRow = dgvDangKi.Rows.Add();
                dgvDangKi.Rows[newRow].Cells[0].Value = item.MaMon;
                dgvDangKi.Rows[newRow].Cells[1].Value = item.TenMon;
                dgvDangKi.Rows[newRow].Cells[2].Value = item.SoTiet;
                dgvDangKi.Rows[newRow].Cells[3].Value = item.SoTinChi;
            }
        }

        private void dgvDangKi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDangKi.CurrentRow.Selected = true;
                int newRow = dgvDangky2.Rows.Add();
                dgvDangky2.Rows[newRow].Cells[1].Value = dgvDangKi.Rows[e.RowIndex].Cells[0].Value;
                dgvDangky2.Rows[newRow].Cells[2].Value = dgvDangKi.Rows[e.RowIndex].Cells[1].Value;
                dgvDangky2.Rows[newRow].Cells[3].Value = dgvDangKi.Rows[e.RowIndex].Cells[2].Value;
                dgvDangky2.Rows[newRow].Cells[4].Value = dgvDangKi.Rows[e.RowIndex].Cells[3].Value;
        }
        private void dgvDangky2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
