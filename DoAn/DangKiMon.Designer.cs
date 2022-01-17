namespace DoAn
{
    partial class DangKiMon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvDangKi = new System.Windows.Forms.DataGridView();
            this.dgvMaMon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTenMon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSoTiet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTinChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDangky2 = new System.Windows.Forms.DataGridView();
            this.selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvMa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTiet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSoTinChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.txtMaMonHoc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTim = new System.Windows.Forms.Button();
            this.txtTongHocPhi = new System.Windows.Forms.TextBox();
            this.txtTongTinChi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangKi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangky2)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDangKi
            // 
            this.dgvDangKi.AllowUserToAddRows = false;
            this.dgvDangKi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDangKi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDangKi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvMaMon,
            this.dgvTenMon,
            this.dgvSoTiet,
            this.dgvTinChi});
            this.dgvDangKi.Location = new System.Drawing.Point(53, 66);
            this.dgvDangKi.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgvDangKi.Name = "dgvDangKi";
            this.dgvDangKi.RowHeadersWidth = 51;
            this.dgvDangKi.RowTemplate.Height = 24;
            this.dgvDangKi.Size = new System.Drawing.Size(656, 153);
            this.dgvDangKi.TabIndex = 1;
            this.dgvDangKi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDangKi_CellClick);
            // 
            // dgvMaMon
            // 
            this.dgvMaMon.FillWeight = 105.9645F;
            this.dgvMaMon.HeaderText = "Mã Môn ";
            this.dgvMaMon.MinimumWidth = 6;
            this.dgvMaMon.Name = "dgvMaMon";
            // 
            // dgvTenMon
            // 
            this.dgvTenMon.FillWeight = 105.9645F;
            this.dgvTenMon.HeaderText = "Tên Môn";
            this.dgvTenMon.MinimumWidth = 6;
            this.dgvTenMon.Name = "dgvTenMon";
            // 
            // dgvSoTiet
            // 
            this.dgvSoTiet.FillWeight = 105.9645F;
            this.dgvSoTiet.HeaderText = "Số tiết";
            this.dgvSoTiet.MinimumWidth = 6;
            this.dgvSoTiet.Name = "dgvSoTiet";
            // 
            // dgvTinChi
            // 
            this.dgvTinChi.FillWeight = 105.9645F;
            this.dgvTinChi.HeaderText = "Số Tín Chỉ";
            this.dgvTinChi.MinimumWidth = 6;
            this.dgvTinChi.Name = "dgvTinChi";
            // 
            // dgvDangky2
            // 
            this.dgvDangky2.AllowUserToAddRows = false;
            this.dgvDangky2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDangky2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDangky2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.selected,
            this.dgvMa,
            this.dgvTen,
            this.dgvTiet,
            this.dgvSoTinChi});
            this.dgvDangky2.Location = new System.Drawing.Point(53, 225);
            this.dgvDangky2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgvDangky2.Name = "dgvDangky2";
            this.dgvDangky2.RowHeadersWidth = 51;
            this.dgvDangky2.RowTemplate.Height = 24;
            this.dgvDangky2.Size = new System.Drawing.Size(656, 164);
            this.dgvDangky2.TabIndex = 2;
            this.dgvDangky2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDangky2_CellClick);
            // 
            // selected
            // 
            this.selected.HeaderText = "selected";
            this.selected.Name = "selected";
            // 
            // dgvMa
            // 
            this.dgvMa.FillWeight = 118.6548F;
            this.dgvMa.HeaderText = "Mã Môn";
            this.dgvMa.MinimumWidth = 6;
            this.dgvMa.Name = "dgvMa";
            // 
            // dgvTen
            // 
            this.dgvTen.FillWeight = 118.6548F;
            this.dgvTen.HeaderText = "Tên Môn";
            this.dgvTen.MinimumWidth = 6;
            this.dgvTen.Name = "dgvTen";
            // 
            // dgvTiet
            // 
            this.dgvTiet.FillWeight = 118.6548F;
            this.dgvTiet.HeaderText = "Số Tiết";
            this.dgvTiet.MinimumWidth = 6;
            this.dgvTiet.Name = "dgvTiet";
            // 
            // dgvSoTinChi
            // 
            this.dgvSoTinChi.FillWeight = 118.6548F;
            this.dgvSoTinChi.HeaderText = "Số Tín Chỉ";
            this.dgvSoTinChi.MinimumWidth = 6;
            this.dgvSoTinChi.Name = "dgvSoTinChi";
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(662, 395);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(61, 19);
            this.btnXoa.TabIndex = 7;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(561, 395);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(83, 19);
            this.btnLuu.TabIndex = 6;
            this.btnLuu.Text = "Lưu đăng ký";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // txtMaMonHoc
            // 
            this.txtMaMonHoc.Location = new System.Drawing.Point(184, 21);
            this.txtMaMonHoc.Name = "txtMaMonHoc";
            this.txtMaMonHoc.Size = new System.Drawing.Size(145, 20);
            this.txtMaMonHoc.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Mã hoặc tên môn học";
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(404, 17);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(75, 23);
            this.btnTim.TabIndex = 11;
            this.btnTim.Text = "Tìm kiếm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // txtTongHocPhi
            // 
            this.txtTongHocPhi.Location = new System.Drawing.Point(404, 396);
            this.txtTongHocPhi.Name = "txtTongHocPhi";
            this.txtTongHocPhi.Size = new System.Drawing.Size(100, 20);
            this.txtTongHocPhi.TabIndex = 12;
            // 
            // txtTongTinChi
            // 
            this.txtTongTinChi.Location = new System.Drawing.Point(196, 396);
            this.txtTongTinChi.Name = "txtTongTinChi";
            this.txtTongTinChi.Size = new System.Drawing.Size(50, 20);
            this.txtTongTinChi.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 401);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Tổng tín chỉ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(326, 398);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Tổng học phí";
            // 
            // DangKiMon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 428);
            this.Controls.Add(this.txtTongTinChi);
            this.Controls.Add(this.txtTongHocPhi);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMaMonHoc);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.dgvDangky2);
            this.Controls.Add(this.dgvDangKi);
            this.Name = "DangKiMon";
            this.Text = "DangKiMon";
            this.Load += new System.EventHandler(this.DangKiMon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangKi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangky2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDangKi;
        private System.Windows.Forms.DataGridView dgvDangky2;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.TextBox txtMaMonHoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMa;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSoTinChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMaMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTenMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSoTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTinChi;
        private System.Windows.Forms.TextBox txtTongHocPhi;
        private System.Windows.Forms.TextBox txtTongTinChi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}