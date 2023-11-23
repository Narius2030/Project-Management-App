namespace QLCongTy
{
    partial class fCheckin_Checkout
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tpCheckSang = new System.Windows.Forms.TabPage();
            this.artanPannel7 = new ArtanComponent.ArtanPannel();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlThongtinCheckIn = new ArtanComponent.ArtanPannel();
            this.cboNoiDungNghi = new QLCongTy.Custome_control.CTComboBox();
            this.btnSubmitNghi = new QLCongTy.VBButton();
            this.dtpNgayNghi = new QLCongTy.CTDateTimePicker();
            this.txtMaNV = new QLCongTy.CTTextBox();
            this.pnlgridviewsang = new ArtanComponent.ArtanPannel();
            this.gvChecksang = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tabDiemdanh = new System.Windows.Forms.TabControl();
            this.tpCheckSang.SuspendLayout();
            this.artanPannel7.SuspendLayout();
            this.pnlThongtinCheckIn.SuspendLayout();
            this.pnlgridviewsang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvChecksang)).BeginInit();
            this.tabDiemdanh.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpCheckSang
            // 
            this.tpCheckSang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(248)))));
            this.tpCheckSang.Controls.Add(this.artanPannel7);
            this.tpCheckSang.Controls.Add(this.pnlThongtinCheckIn);
            this.tpCheckSang.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.tpCheckSang.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpCheckSang.ForeColor = System.Drawing.Color.Black;
            this.tpCheckSang.Location = new System.Drawing.Point(4, 38);
            this.tpCheckSang.Name = "tpCheckSang";
            this.tpCheckSang.Padding = new System.Windows.Forms.Padding(3);
            this.tpCheckSang.Size = new System.Drawing.Size(1347, 777);
            this.tpCheckSang.TabIndex = 0;
            this.tpCheckSang.Text = "Buổi Sáng ";
            this.tpCheckSang.Click += new System.EventHandler(this.tpCheckSang_Click);
            // 
            // artanPannel7
            // 
            this.artanPannel7.BackColor = System.Drawing.Color.White;
            this.artanPannel7.BorderRadius = 0;
            this.artanPannel7.Controls.Add(this.label14);
            this.artanPannel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.artanPannel7.ForeColor = System.Drawing.Color.Black;
            this.artanPannel7.GradientAngle = 90F;
            this.artanPannel7.GradientBttomColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.artanPannel7.GradientTopcolor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.artanPannel7.Location = new System.Drawing.Point(3, 3);
            this.artanPannel7.Name = "artanPannel7";
            this.artanPannel7.Padding = new System.Windows.Forms.Padding(10);
            this.artanPannel7.Size = new System.Drawing.Size(1341, 72);
            this.artanPannel7.TabIndex = 93;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label14.Location = new System.Drawing.Point(10, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1321, 52);
            this.label14.TabIndex = 1;
            this.label14.Text = "THÔNG TIN ĐIỂM DANH";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlThongtinCheckIn
            // 
            this.pnlThongtinCheckIn.BackColor = System.Drawing.SystemColors.Window;
            this.pnlThongtinCheckIn.BorderRadius = 3;
            this.pnlThongtinCheckIn.Controls.Add(this.cboNoiDungNghi);
            this.pnlThongtinCheckIn.Controls.Add(this.btnSubmitNghi);
            this.pnlThongtinCheckIn.Controls.Add(this.dtpNgayNghi);
            this.pnlThongtinCheckIn.Controls.Add(this.txtMaNV);
            this.pnlThongtinCheckIn.Controls.Add(this.pnlgridviewsang);
            this.pnlThongtinCheckIn.Controls.Add(this.label8);
            this.pnlThongtinCheckIn.Controls.Add(this.label10);
            this.pnlThongtinCheckIn.ForeColor = System.Drawing.Color.Black;
            this.pnlThongtinCheckIn.GradientAngle = 90F;
            this.pnlThongtinCheckIn.GradientBttomColor = System.Drawing.Color.White;
            this.pnlThongtinCheckIn.GradientTopcolor = System.Drawing.Color.White;
            this.pnlThongtinCheckIn.Location = new System.Drawing.Point(79, 136);
            this.pnlThongtinCheckIn.Name = "pnlThongtinCheckIn";
            this.pnlThongtinCheckIn.Size = new System.Drawing.Size(1191, 601);
            this.pnlThongtinCheckIn.TabIndex = 51;
            // 
            // cboNoiDungNghi
            // 
            this.cboNoiDungNghi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.cboNoiDungNghi.BorderColor = System.Drawing.Color.White;
            this.cboNoiDungNghi.BorderSize = 3;
            this.cboNoiDungNghi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboNoiDungNghi.ForeColor = System.Drawing.Color.Transparent;
            this.cboNoiDungNghi.FormattingEnabled = true;
            this.cboNoiDungNghi.IconColor = System.Drawing.Color.White;
            this.cboNoiDungNghi.Items.AddRange(new object[] {
            "Nghỉ ốm",
            "Nghỉ phép",
            "Nghỉ thai sản"});
            this.cboNoiDungNghi.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.cboNoiDungNghi.ListTextColor = System.Drawing.Color.DimGray;
            this.cboNoiDungNghi.Location = new System.Drawing.Point(562, 45);
            this.cboNoiDungNghi.MinimumSize = new System.Drawing.Size(200, 0);
            this.cboNoiDungNghi.Name = "cboNoiDungNghi";
            this.cboNoiDungNghi.Size = new System.Drawing.Size(200, 28);
            this.cboNoiDungNghi.TabIndex = 90;
            this.cboNoiDungNghi.Texts = "";
            // 
            // btnSubmitNghi
            // 
            this.btnSubmitNghi.BackColor = System.Drawing.Color.Azure;
            this.btnSubmitNghi.BackgroundColor = System.Drawing.Color.Azure;
            this.btnSubmitNghi.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnSubmitNghi.BorderRadius = 3;
            this.btnSubmitNghi.BorderSize = 2;
            this.btnSubmitNghi.FlatAppearance.BorderSize = 0;
            this.btnSubmitNghi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmitNghi.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmitNghi.ForeColor = System.Drawing.Color.Black;
            this.btnSubmitNghi.Location = new System.Drawing.Point(659, 107);
            this.btnSubmitNghi.Name = "btnSubmitNghi";
            this.btnSubmitNghi.Size = new System.Drawing.Size(103, 43);
            this.btnSubmitNghi.TabIndex = 88;
            this.btnSubmitNghi.Text = "Submit";
            this.btnSubmitNghi.TextColor = System.Drawing.Color.Black;
            this.btnSubmitNghi.UseVisualStyleBackColor = false;
            this.btnSubmitNghi.Click += new System.EventHandler(this.btnSubmitNghi_Click);
            // 
            // dtpNgayNghi
            // 
            this.dtpNgayNghi.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.dtpNgayNghi.BorderSize = 0;
            this.dtpNgayNghi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayNghi.Location = new System.Drawing.Point(379, 115);
            this.dtpNgayNghi.MinimumSize = new System.Drawing.Size(4, 35);
            this.dtpNgayNghi.Name = "dtpNgayNghi";
            this.dtpNgayNghi.Size = new System.Drawing.Size(257, 35);
            this.dtpNgayNghi.SkinColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.dtpNgayNghi.TabIndex = 62;
            this.dtpNgayNghi.TextColor = System.Drawing.Color.White;
            // 
            // txtMaNV
            // 
            this.txtMaNV.BackColor = System.Drawing.Color.White;
            this.txtMaNV.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.txtMaNV.BorderFocusColor = System.Drawing.Color.HotPink;
            this.txtMaNV.BorderRadius = 0;
            this.txtMaNV.BorderSize = 2;
            this.txtMaNV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaNV.ForeColor = System.Drawing.Color.Black;
            this.txtMaNV.Location = new System.Drawing.Point(379, 45);
            this.txtMaNV.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaNV.Multiline = false;
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Padding = new System.Windows.Forms.Padding(7);
            this.txtMaNV.PasswordChar = false;
            this.txtMaNV.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtMaNV.PlaceholderText = "";
            this.txtMaNV.Size = new System.Drawing.Size(169, 35);
            this.txtMaNV.TabIndex = 59;
            this.txtMaNV.Texts = "";
            this.txtMaNV.UnderlinedStyle = false;
            // 
            // pnlgridviewsang
            // 
            this.pnlgridviewsang.BackColor = System.Drawing.Color.White;
            this.pnlgridviewsang.BorderRadius = 30;
            this.pnlgridviewsang.Controls.Add(this.gvChecksang);
            this.pnlgridviewsang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(94)))), ((int)(((byte)(231)))));
            this.pnlgridviewsang.GradientAngle = 90F;
            this.pnlgridviewsang.GradientBttomColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.pnlgridviewsang.GradientTopcolor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.pnlgridviewsang.Location = new System.Drawing.Point(162, 206);
            this.pnlgridviewsang.Name = "pnlgridviewsang";
            this.pnlgridviewsang.Padding = new System.Windows.Forms.Padding(10);
            this.pnlgridviewsang.Size = new System.Drawing.Size(870, 341);
            this.pnlgridviewsang.TabIndex = 50;
            // 
            // gvChecksang
            // 
            this.gvChecksang.AllowUserToResizeColumns = false;
            this.gvChecksang.AllowUserToResizeRows = false;
            this.gvChecksang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvChecksang.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvChecksang.BackgroundColor = System.Drawing.Color.White;
            this.gvChecksang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gvChecksang.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvChecksang.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(96)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(96)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvChecksang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvChecksang.ColumnHeadersHeight = 35;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(94)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvChecksang.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvChecksang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvChecksang.EnableHeadersVisualStyles = false;
            this.gvChecksang.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.gvChecksang.Location = new System.Drawing.Point(10, 10);
            this.gvChecksang.MultiSelect = false;
            this.gvChecksang.Name = "gvChecksang";
            this.gvChecksang.ReadOnly = true;
            this.gvChecksang.RowHeadersVisible = false;
            this.gvChecksang.RowHeadersWidth = 51;
            this.gvChecksang.RowTemplate.Height = 24;
            this.gvChecksang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvChecksang.Size = new System.Drawing.Size(850, 321);
            this.gvChecksang.TabIndex = 38;
            this.gvChecksang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvChecksang_CellClick);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(197, 116);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 30);
            this.label8.TabIndex = 30;
            this.label8.Text = "Ngày nghỉ";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(197, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(176, 30);
            this.label10.TabIndex = 29;
            this.label10.Text = "Mã Nhân viên ";
            // 
            // tabDiemdanh
            // 
            this.tabDiemdanh.Controls.Add(this.tpCheckSang);
            this.tabDiemdanh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDiemdanh.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabDiemdanh.Location = new System.Drawing.Point(0, 0);
            this.tabDiemdanh.Name = "tabDiemdanh";
            this.tabDiemdanh.SelectedIndex = 0;
            this.tabDiemdanh.Size = new System.Drawing.Size(1355, 819);
            this.tabDiemdanh.TabIndex = 0;
            // 
            // fCheckin_Checkout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1355, 819);
            this.Controls.Add(this.tabDiemdanh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fCheckin_Checkout";
            this.Text = "Checkin_Checkout";
            this.Load += new System.EventHandler(this.fCheckin_Checkout_Load);
            this.tpCheckSang.ResumeLayout(false);
            this.artanPannel7.ResumeLayout(false);
            this.pnlThongtinCheckIn.ResumeLayout(false);
            this.pnlgridviewsang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvChecksang)).EndInit();
            this.tabDiemdanh.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tpCheckSang;
        private ArtanComponent.ArtanPannel pnlThongtinCheckIn;
        private VBButton btnSubmitNghi;
        private CTDateTimePicker dtpNgayNghi;
        private CTTextBox txtMaNV;
        private ArtanComponent.ArtanPannel pnlgridviewsang;
        private System.Windows.Forms.DataGridView gvChecksang;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabControl tabDiemdanh;
        private ArtanComponent.ArtanPannel artanPannel7;
        private System.Windows.Forms.Label label14;
        private Custome_control.CTComboBox cboNoiDungNghi;
    }
}