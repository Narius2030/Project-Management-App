﻿namespace QLCongTy.Views.NhanSu
{
    partial class fNhiemVu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnPhanCong = new System.Windows.Forms.Panel();
            this.btnXoaNhiemVu = new FontAwesome.Sharp.IconButton();
            this.btnTaoNhiemVu = new FontAwesome.Sharp.IconButton();
            this.pnPCNhom = new System.Windows.Forms.Panel();
            this.cboNhiemVuTienQuyet = new System.Windows.Forms.ComboBox();
            this.nudThoiGianUocTinh = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pnThongTinDA = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.vbLabel2 = new QLCongTy.VBLabel();
            this.artanPannel6 = new ArtanComponent.ArtanPannel();
            this.gvDSNhiemVu = new System.Windows.Forms.DataGridView();
            this.txtMaNhiemVu = new QLCongTy.CTTextBox();
            this.txtNhiemVu = new QLCongTy.CTTextBox();
            this.btnPhanCV = new QLCongTy.VBButton();
            this.txtMaGiaiDoan = new QLCongTy.CTTextBox();
            this.txtCongViec = new QLCongTy.CTTextBox();
            this.txtMaDA = new QLCongTy.CTTextBox();
            this.txtTenNhom = new QLCongTy.CTTextBox();
            this.pgbUocTinhNV = new QLCongTy.CTProgressBar();
            this.pgbThucTeNV = new QLCongTy.CTProgressBar();
            this.pnTitle = new ArtanComponent.ArtanPannel();
            this.lblTitleNhiemVu = new System.Windows.Forms.Label();
            this.cbTienQuyet = new System.Windows.Forms.CheckBox();
            this.pnPhanCong.SuspendLayout();
            this.pnPCNhom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThoiGianUocTinh)).BeginInit();
            this.pnThongTinDA.SuspendLayout();
            this.artanPannel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDSNhiemVu)).BeginInit();
            this.pnTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnPhanCong
            // 
            this.pnPhanCong.BackColor = System.Drawing.Color.White;
            this.pnPhanCong.Controls.Add(this.btnXoaNhiemVu);
            this.pnPhanCong.Controls.Add(this.btnTaoNhiemVu);
            this.pnPhanCong.Controls.Add(this.vbLabel2);
            this.pnPhanCong.Controls.Add(this.artanPannel6);
            this.pnPhanCong.Controls.Add(this.pnPCNhom);
            this.pnPhanCong.Location = new System.Drawing.Point(22, 249);
            this.pnPhanCong.Name = "pnPhanCong";
            this.pnPhanCong.Size = new System.Drawing.Size(1263, 443);
            this.pnPhanCong.TabIndex = 91;
            // 
            // btnXoaNhiemVu
            // 
            this.btnXoaNhiemVu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoaNhiemVu.BackColor = System.Drawing.Color.Crimson;
            this.btnXoaNhiemVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaNhiemVu.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.btnXoaNhiemVu.IconColor = System.Drawing.Color.White;
            this.btnXoaNhiemVu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnXoaNhiemVu.IconSize = 30;
            this.btnXoaNhiemVu.Location = new System.Drawing.Point(208, 162);
            this.btnXoaNhiemVu.Name = "btnXoaNhiemVu";
            this.btnXoaNhiemVu.Size = new System.Drawing.Size(40, 43);
            this.btnXoaNhiemVu.TabIndex = 89;
            this.btnXoaNhiemVu.UseVisualStyleBackColor = false;
            this.btnXoaNhiemVu.Click += new System.EventHandler(this.btnXoaNhiemVu_Click);
            // 
            // btnTaoNhiemVu
            // 
            this.btnTaoNhiemVu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTaoNhiemVu.BackColor = System.Drawing.Color.Lime;
            this.btnTaoNhiemVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoNhiemVu.IconChar = FontAwesome.Sharp.IconChar.Add;
            this.btnTaoNhiemVu.IconColor = System.Drawing.Color.White;
            this.btnTaoNhiemVu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnTaoNhiemVu.IconSize = 30;
            this.btnTaoNhiemVu.Location = new System.Drawing.Point(169, 162);
            this.btnTaoNhiemVu.Name = "btnTaoNhiemVu";
            this.btnTaoNhiemVu.Size = new System.Drawing.Size(40, 43);
            this.btnTaoNhiemVu.TabIndex = 88;
            this.btnTaoNhiemVu.UseVisualStyleBackColor = false;
            this.btnTaoNhiemVu.Click += new System.EventHandler(this.btnTaoNhiemVu_Click);
            // 
            // pnPCNhom
            // 
            this.pnPCNhom.Controls.Add(this.cbTienQuyet);
            this.pnPCNhom.Controls.Add(this.cboNhiemVuTienQuyet);
            this.pnPCNhom.Controls.Add(this.nudThoiGianUocTinh);
            this.pnPCNhom.Controls.Add(this.label9);
            this.pnPCNhom.Controls.Add(this.txtMaNhiemVu);
            this.pnPCNhom.Controls.Add(this.label8);
            this.pnPCNhom.Controls.Add(this.txtNhiemVu);
            this.pnPCNhom.Controls.Add(this.label3);
            this.pnPCNhom.Controls.Add(this.btnPhanCV);
            this.pnPCNhom.Location = new System.Drawing.Point(19, 21);
            this.pnPCNhom.Name = "pnPCNhom";
            this.pnPCNhom.Size = new System.Drawing.Size(1221, 111);
            this.pnPCNhom.TabIndex = 85;
            // 
            // cboNhiemVuTienQuyet
            // 
            this.cboNhiemVuTienQuyet.Enabled = false;
            this.cboNhiemVuTienQuyet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNhiemVuTienQuyet.FormattingEnabled = true;
            this.cboNhiemVuTienQuyet.Items.AddRange(new object[] {
            "BUSINESS ANALYST",
            "UI/UX DESIGNER",
            "DEVOPS",
            "TESTER",
            "DATABASE",
            "BACK-END",
            "FRONT-END",
            "SECURITY"});
            this.cboNhiemVuTienQuyet.Location = new System.Drawing.Point(480, 56);
            this.cboNhiemVuTienQuyet.Name = "cboNhiemVuTienQuyet";
            this.cboNhiemVuTienQuyet.Size = new System.Drawing.Size(271, 33);
            this.cboNhiemVuTienQuyet.TabIndex = 112;
            // 
            // nudThoiGianUocTinh
            // 
            this.nudThoiGianUocTinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudThoiGianUocTinh.Location = new System.Drawing.Point(801, 56);
            this.nudThoiGianUocTinh.Name = "nudThoiGianUocTinh";
            this.nudThoiGianUocTinh.Size = new System.Drawing.Size(104, 34);
            this.nudThoiGianUocTinh.TabIndex = 111;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(796, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(234, 26);
            this.label9.TabIndex = 110;
            this.label9.Text = "Thời gian ước tính (hrs)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(28, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 26);
            this.label8.TabIndex = 108;
            this.label8.Text = "Mã Nhiệm vụ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(252, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 26);
            this.label3.TabIndex = 96;
            this.label3.Text = "Tên nhiệm vụ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(314, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 26);
            this.label7.TabIndex = 91;
            this.label7.Text = "Công việc";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(314, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 26);
            this.label6.TabIndex = 84;
            this.label6.Text = "Tên nhóm";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(21, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 26);
            this.label12.TabIndex = 96;
            this.label12.Text = "Mã dự án";
            // 
            // pnThongTinDA
            // 
            this.pnThongTinDA.BackColor = System.Drawing.Color.White;
            this.pnThongTinDA.Controls.Add(this.txtMaGiaiDoan);
            this.pnThongTinDA.Controls.Add(this.txtCongViec);
            this.pnThongTinDA.Controls.Add(this.txtMaDA);
            this.pnThongTinDA.Controls.Add(this.txtTenNhom);
            this.pnThongTinDA.Controls.Add(this.label7);
            this.pnThongTinDA.Controls.Add(this.label2);
            this.pnThongTinDA.Controls.Add(this.label12);
            this.pnThongTinDA.Controls.Add(this.label1);
            this.pnThongTinDA.Controls.Add(this.pgbUocTinhNV);
            this.pnThongTinDA.Controls.Add(this.pgbThucTeNV);
            this.pnThongTinDA.Controls.Add(this.label6);
            this.pnThongTinDA.Controls.Add(this.label13);
            this.pnThongTinDA.Location = new System.Drawing.Point(22, 86);
            this.pnThongTinDA.Name = "pnThongTinDA";
            this.pnThongTinDA.Size = new System.Drawing.Size(1263, 139);
            this.pnThongTinDA.TabIndex = 90;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(650, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 27);
            this.label2.TabIndex = 102;
            this.label2.Text = "Ước tính";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(650, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 27);
            this.label1.TabIndex = 101;
            this.label1.Text = "Thực tế";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(17, 84);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(137, 27);
            this.label13.TabIndex = 97;
            this.label13.Text = "Mã giai đoạn";
            // 
            // vbLabel2
            // 
            this.vbLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.vbLabel2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.vbLabel2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.vbLabel2.BorderRadius = 0;
            this.vbLabel2.BorderSize = 0;
            this.vbLabel2.FlatAppearance.BorderSize = 0;
            this.vbLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vbLabel2.ForeColor = System.Drawing.Color.White;
            this.vbLabel2.Location = new System.Drawing.Point(22, 162);
            this.vbLabel2.Name = "vbLabel2";
            this.vbLabel2.Size = new System.Drawing.Size(224, 43);
            this.vbLabel2.TabIndex = 87;
            this.vbLabel2.Text = "     Bảng nhiệm vụ";
            this.vbLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vbLabel2.TextColor = System.Drawing.Color.White;
            this.vbLabel2.UseVisualStyleBackColor = false;
            // 
            // artanPannel6
            // 
            this.artanPannel6.BackColor = System.Drawing.Color.White;
            this.artanPannel6.BorderRadius = 0;
            this.artanPannel6.Controls.Add(this.gvDSNhiemVu);
            this.artanPannel6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.artanPannel6.ForeColor = System.Drawing.Color.Black;
            this.artanPannel6.GradientAngle = 90F;
            this.artanPannel6.GradientBttomColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.artanPannel6.GradientTopcolor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.artanPannel6.Location = new System.Drawing.Point(22, 195);
            this.artanPannel6.Name = "artanPannel6";
            this.artanPannel6.Padding = new System.Windows.Forms.Padding(10);
            this.artanPannel6.Size = new System.Drawing.Size(1218, 227);
            this.artanPannel6.TabIndex = 86;
            // 
            // gvDSNhiemVu
            // 
            this.gvDSNhiemVu.AllowUserToAddRows = false;
            this.gvDSNhiemVu.AllowUserToDeleteRows = false;
            this.gvDSNhiemVu.AllowUserToResizeColumns = false;
            this.gvDSNhiemVu.AllowUserToResizeRows = false;
            this.gvDSNhiemVu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvDSNhiemVu.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvDSNhiemVu.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.gvDSNhiemVu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gvDSNhiemVu.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvDSNhiemVu.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(96)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(232)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(96)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(232)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvDSNhiemVu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvDSNhiemVu.ColumnHeadersHeight = 35;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(229)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(101)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvDSNhiemVu.DefaultCellStyle = dataGridViewCellStyle5;
            this.gvDSNhiemVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvDSNhiemVu.EnableHeadersVisualStyles = false;
            this.gvDSNhiemVu.GridColor = System.Drawing.Color.Silver;
            this.gvDSNhiemVu.Location = new System.Drawing.Point(10, 10);
            this.gvDSNhiemVu.Name = "gvDSNhiemVu";
            this.gvDSNhiemVu.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(93)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(229)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(94)))), ((int)(((byte)(229)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(229)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvDSNhiemVu.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gvDSNhiemVu.RowHeadersVisible = false;
            this.gvDSNhiemVu.RowHeadersWidth = 51;
            this.gvDSNhiemVu.RowTemplate.Height = 24;
            this.gvDSNhiemVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvDSNhiemVu.Size = new System.Drawing.Size(1198, 207);
            this.gvDSNhiemVu.TabIndex = 2;
            this.gvDSNhiemVu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvDSNhiemVu_CellClick);
            // 
            // txtMaNhiemVu
            // 
            this.txtMaNhiemVu.BackColor = System.Drawing.Color.White;
            this.txtMaNhiemVu.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.txtMaNhiemVu.BorderFocusColor = System.Drawing.Color.HotPink;
            this.txtMaNhiemVu.BorderRadius = 0;
            this.txtMaNhiemVu.BorderSize = 2;
            this.txtMaNhiemVu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaNhiemVu.ForeColor = System.Drawing.Color.Black;
            this.txtMaNhiemVu.Location = new System.Drawing.Point(33, 56);
            this.txtMaNhiemVu.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaNhiemVu.Multiline = false;
            this.txtMaNhiemVu.Name = "txtMaNhiemVu";
            this.txtMaNhiemVu.Padding = new System.Windows.Forms.Padding(7);
            this.txtMaNhiemVu.PasswordChar = false;
            this.txtMaNhiemVu.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtMaNhiemVu.PlaceholderText = "";
            this.txtMaNhiemVu.Size = new System.Drawing.Size(171, 35);
            this.txtMaNhiemVu.TabIndex = 109;
            this.txtMaNhiemVu.Texts = "";
            this.txtMaNhiemVu.UnderlinedStyle = false;
            // 
            // txtNhiemVu
            // 
            this.txtNhiemVu.BackColor = System.Drawing.Color.White;
            this.txtNhiemVu.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.txtNhiemVu.BorderFocusColor = System.Drawing.Color.HotPink;
            this.txtNhiemVu.BorderRadius = 0;
            this.txtNhiemVu.BorderSize = 2;
            this.txtNhiemVu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhiemVu.ForeColor = System.Drawing.Color.Black;
            this.txtNhiemVu.Location = new System.Drawing.Point(257, 56);
            this.txtNhiemVu.Margin = new System.Windows.Forms.Padding(4);
            this.txtNhiemVu.Multiline = false;
            this.txtNhiemVu.Name = "txtNhiemVu";
            this.txtNhiemVu.Padding = new System.Windows.Forms.Padding(7);
            this.txtNhiemVu.PasswordChar = false;
            this.txtNhiemVu.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtNhiemVu.PlaceholderText = "";
            this.txtNhiemVu.Size = new System.Drawing.Size(171, 35);
            this.txtNhiemVu.TabIndex = 107;
            this.txtNhiemVu.Texts = "";
            this.txtNhiemVu.UnderlinedStyle = false;
            // 
            // btnPhanCV
            // 
            this.btnPhanCV.BackColor = System.Drawing.Color.Azure;
            this.btnPhanCV.BackgroundColor = System.Drawing.Color.Azure;
            this.btnPhanCV.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnPhanCV.BorderRadius = 3;
            this.btnPhanCV.BorderSize = 2;
            this.btnPhanCV.FlatAppearance.BorderSize = 0;
            this.btnPhanCV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPhanCV.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            this.btnPhanCV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.btnPhanCV.Location = new System.Drawing.Point(1064, 30);
            this.btnPhanCV.Name = "btnPhanCV";
            this.btnPhanCV.Size = new System.Drawing.Size(139, 63);
            this.btnPhanCV.TabIndex = 95;
            this.btnPhanCV.Text = "Phân công công việc";
            this.btnPhanCV.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.btnPhanCV.UseVisualStyleBackColor = false;
            this.btnPhanCV.Click += new System.EventHandler(this.btnPhanCV_Click);
            // 
            // txtMaGiaiDoan
            // 
            this.txtMaGiaiDoan.BackColor = System.Drawing.Color.White;
            this.txtMaGiaiDoan.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.txtMaGiaiDoan.BorderFocusColor = System.Drawing.Color.HotPink;
            this.txtMaGiaiDoan.BorderRadius = 0;
            this.txtMaGiaiDoan.BorderSize = 2;
            this.txtMaGiaiDoan.Enabled = false;
            this.txtMaGiaiDoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaGiaiDoan.ForeColor = System.Drawing.Color.Black;
            this.txtMaGiaiDoan.Location = new System.Drawing.Point(156, 77);
            this.txtMaGiaiDoan.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaGiaiDoan.Multiline = false;
            this.txtMaGiaiDoan.Name = "txtMaGiaiDoan";
            this.txtMaGiaiDoan.Padding = new System.Windows.Forms.Padding(7);
            this.txtMaGiaiDoan.PasswordChar = false;
            this.txtMaGiaiDoan.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtMaGiaiDoan.PlaceholderText = "";
            this.txtMaGiaiDoan.Size = new System.Drawing.Size(129, 35);
            this.txtMaGiaiDoan.TabIndex = 104;
            this.txtMaGiaiDoan.Texts = "";
            this.txtMaGiaiDoan.UnderlinedStyle = false;
            // 
            // txtCongViec
            // 
            this.txtCongViec.BackColor = System.Drawing.Color.White;
            this.txtCongViec.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.txtCongViec.BorderFocusColor = System.Drawing.Color.HotPink;
            this.txtCongViec.BorderRadius = 0;
            this.txtCongViec.BorderSize = 2;
            this.txtCongViec.Enabled = false;
            this.txtCongViec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCongViec.ForeColor = System.Drawing.Color.Black;
            this.txtCongViec.Location = new System.Drawing.Point(429, 77);
            this.txtCongViec.Margin = new System.Windows.Forms.Padding(4);
            this.txtCongViec.Multiline = false;
            this.txtCongViec.Name = "txtCongViec";
            this.txtCongViec.Padding = new System.Windows.Forms.Padding(7);
            this.txtCongViec.PasswordChar = false;
            this.txtCongViec.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtCongViec.PlaceholderText = "";
            this.txtCongViec.Size = new System.Drawing.Size(171, 35);
            this.txtCongViec.TabIndex = 106;
            this.txtCongViec.Texts = "";
            this.txtCongViec.UnderlinedStyle = false;
            // 
            // txtMaDA
            // 
            this.txtMaDA.BackColor = System.Drawing.Color.White;
            this.txtMaDA.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.txtMaDA.BorderFocusColor = System.Drawing.Color.HotPink;
            this.txtMaDA.BorderRadius = 0;
            this.txtMaDA.BorderSize = 2;
            this.txtMaDA.Enabled = false;
            this.txtMaDA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaDA.ForeColor = System.Drawing.Color.Black;
            this.txtMaDA.Location = new System.Drawing.Point(156, 22);
            this.txtMaDA.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaDA.Multiline = false;
            this.txtMaDA.Name = "txtMaDA";
            this.txtMaDA.Padding = new System.Windows.Forms.Padding(7);
            this.txtMaDA.PasswordChar = false;
            this.txtMaDA.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtMaDA.PlaceholderText = "";
            this.txtMaDA.Size = new System.Drawing.Size(129, 35);
            this.txtMaDA.TabIndex = 103;
            this.txtMaDA.Texts = "";
            this.txtMaDA.UnderlinedStyle = false;
            // 
            // txtTenNhom
            // 
            this.txtTenNhom.BackColor = System.Drawing.Color.White;
            this.txtTenNhom.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.txtTenNhom.BorderFocusColor = System.Drawing.Color.HotPink;
            this.txtTenNhom.BorderRadius = 0;
            this.txtTenNhom.BorderSize = 2;
            this.txtTenNhom.Enabled = false;
            this.txtTenNhom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenNhom.ForeColor = System.Drawing.Color.Black;
            this.txtTenNhom.Location = new System.Drawing.Point(429, 22);
            this.txtTenNhom.Margin = new System.Windows.Forms.Padding(4);
            this.txtTenNhom.Multiline = false;
            this.txtTenNhom.Name = "txtTenNhom";
            this.txtTenNhom.Padding = new System.Windows.Forms.Padding(7);
            this.txtTenNhom.PasswordChar = false;
            this.txtTenNhom.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtTenNhom.PlaceholderText = "";
            this.txtTenNhom.Size = new System.Drawing.Size(171, 35);
            this.txtTenNhom.TabIndex = 105;
            this.txtTenNhom.Texts = "";
            this.txtTenNhom.UnderlinedStyle = false;
            // 
            // pgbUocTinhNV
            // 
            this.pgbUocTinhNV.ChannelColor = System.Drawing.Color.LightSteelBlue;
            this.pgbUocTinhNV.ChannelHeight = 6;
            this.pgbUocTinhNV.ForeBackColor = System.Drawing.Color.DarkOrange;
            this.pgbUocTinhNV.ForeColor = System.Drawing.Color.White;
            this.pgbUocTinhNV.Location = new System.Drawing.Point(775, 81);
            this.pgbUocTinhNV.Name = "pgbUocTinhNV";
            this.pgbUocTinhNV.ShowMaximun = false;
            this.pgbUocTinhNV.ShowValue = QLCongTy.TextPosition.Center;
            this.pgbUocTinhNV.Size = new System.Drawing.Size(447, 30);
            this.pgbUocTinhNV.SliderColor = System.Drawing.Color.Chartreuse;
            this.pgbUocTinhNV.SliderHeight = 10;
            this.pgbUocTinhNV.SymbolAfter = "hrs";
            this.pgbUocTinhNV.SymbolBefore = "";
            this.pgbUocTinhNV.TabIndex = 100;
            // 
            // pgbThucTeNV
            // 
            this.pgbThucTeNV.ChannelColor = System.Drawing.Color.LightSteelBlue;
            this.pgbThucTeNV.ChannelHeight = 6;
            this.pgbThucTeNV.ForeBackColor = System.Drawing.Color.DarkOrange;
            this.pgbThucTeNV.ForeColor = System.Drawing.Color.White;
            this.pgbThucTeNV.Location = new System.Drawing.Point(775, 22);
            this.pgbThucTeNV.Name = "pgbThucTeNV";
            this.pgbThucTeNV.ShowMaximun = false;
            this.pgbThucTeNV.ShowValue = QLCongTy.TextPosition.Center;
            this.pgbThucTeNV.Size = new System.Drawing.Size(447, 30);
            this.pgbThucTeNV.SliderColor = System.Drawing.Color.Chartreuse;
            this.pgbThucTeNV.SliderHeight = 10;
            this.pgbThucTeNV.SymbolAfter = "hrs";
            this.pgbThucTeNV.SymbolBefore = "";
            this.pgbThucTeNV.TabIndex = 99;
            // 
            // pnTitle
            // 
            this.pnTitle.BackColor = System.Drawing.Color.White;
            this.pnTitle.BorderRadius = 0;
            this.pnTitle.Controls.Add(this.lblTitleNhiemVu);
            this.pnTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTitle.ForeColor = System.Drawing.Color.Black;
            this.pnTitle.GradientAngle = 90F;
            this.pnTitle.GradientBttomColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.pnTitle.GradientTopcolor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.pnTitle.Location = new System.Drawing.Point(0, 0);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Padding = new System.Windows.Forms.Padding(10);
            this.pnTitle.Size = new System.Drawing.Size(1313, 72);
            this.pnTitle.TabIndex = 89;
            // 
            // lblTitleNhiemVu
            // 
            this.lblTitleNhiemVu.BackColor = System.Drawing.Color.Transparent;
            this.lblTitleNhiemVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitleNhiemVu.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleNhiemVu.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTitleNhiemVu.Location = new System.Drawing.Point(10, 10);
            this.lblTitleNhiemVu.Name = "lblTitleNhiemVu";
            this.lblTitleNhiemVu.Size = new System.Drawing.Size(1293, 52);
            this.lblTitleNhiemVu.TabIndex = 1;
            this.lblTitleNhiemVu.Text = "MaNV-TenNV - TenNhom";
            this.lblTitleNhiemVu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbTienQuyet
            // 
            this.cbTienQuyet.AutoSize = true;
            this.cbTienQuyet.Font = new System.Drawing.Font("Times New Roman", 13.8F);
            this.cbTienQuyet.Location = new System.Drawing.Point(480, 20);
            this.cbTienQuyet.Name = "cbTienQuyet";
            this.cbTienQuyet.Size = new System.Drawing.Size(158, 30);
            this.cbTienQuyet.TabIndex = 113;
            this.cbTienQuyet.Text = "Có tiên quyết";
            this.cbTienQuyet.UseVisualStyleBackColor = true;
            this.cbTienQuyet.CheckedChanged += new System.EventHandler(this.cbTienQuyet_CheckedChanged);
            // 
            // fNhiemVu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1313, 717);
            this.Controls.Add(this.pnPhanCong);
            this.Controls.Add(this.pnThongTinDA);
            this.Controls.Add(this.pnTitle);
            this.Name = "fNhiemVu";
            this.Text = "fNhiemVu";
            this.Load += new System.EventHandler(this.fNhiemVu_Load);
            this.pnPhanCong.ResumeLayout(false);
            this.pnPCNhom.ResumeLayout(false);
            this.pnPCNhom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThoiGianUocTinh)).EndInit();
            this.pnThongTinDA.ResumeLayout(false);
            this.pnThongTinDA.PerformLayout();
            this.artanPannel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvDSNhiemVu)).EndInit();
            this.pnTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnPhanCong;
        private System.Windows.Forms.Panel pnPCNhom;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private VBButton btnPhanCV;
        private System.Windows.Forms.Panel pnThongTinDA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private CTProgressBar pgbUocTinhNV;
        private CTProgressBar pgbThucTeNV;
        private System.Windows.Forms.Label label13;
        private ArtanComponent.ArtanPannel pnTitle;
        private System.Windows.Forms.Label lblTitleNhiemVu;
        private System.Windows.Forms.Label label3;
        private VBLabel vbLabel2;
        private ArtanComponent.ArtanPannel artanPannel6;
        private System.Windows.Forms.DataGridView gvDSNhiemVu;
        private FontAwesome.Sharp.IconButton btnTaoNhiemVu;
        private CTTextBox txtTenNhom;
        private CTTextBox txtMaGiaiDoan;
        private CTTextBox txtMaDA;
        private CTTextBox txtNhiemVu;
        private CTTextBox txtCongViec;
        private CTTextBox txtMaNhiemVu;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudThoiGianUocTinh;
        private System.Windows.Forms.Label label9;
        private FontAwesome.Sharp.IconButton btnXoaNhiemVu;
        private System.Windows.Forms.ComboBox cboNhiemVuTienQuyet;
        private System.Windows.Forms.CheckBox cbTienQuyet;
    }
}