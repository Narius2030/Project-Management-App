﻿namespace QLCongTy
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
            this.artanPannel1 = new ArtanComponent.ArtanPannel();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlThongtinCheckIn = new ArtanComponent.ArtanPannel();
            this.label10 = new System.Windows.Forms.Label();
            this.cbCheckInsang = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlgridviewsang = new ArtanComponent.ArtanPannel();
            this.gvChecksang = new System.Windows.Forms.DataGridView();
            this.txtManvsang = new QLCongTy.CTTextBox();
            this.txtMacvsang = new QLCongTy.CTTextBox();
            this.dtpCheckIn = new QLCongTy.CTDateTimePicker();
            this.btnSubmitSang = new QLCongTy.VBButton();
            this.tabDiemdanh = new System.Windows.Forms.TabControl();
            this.tpCheckSang.SuspendLayout();
            this.artanPannel1.SuspendLayout();
            this.pnlThongtinCheckIn.SuspendLayout();
            this.pnlgridviewsang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvChecksang)).BeginInit();
            this.tabDiemdanh.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpCheckSang
            // 
            this.tpCheckSang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(207)))), ((int)(((byte)(245)))));
            this.tpCheckSang.Controls.Add(this.pnlThongtinCheckIn);
            this.tpCheckSang.Controls.Add(this.artanPannel1);
            this.tpCheckSang.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.tpCheckSang.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpCheckSang.ForeColor = System.Drawing.Color.Black;
            this.tpCheckSang.Location = new System.Drawing.Point(4, 38);
            this.tpCheckSang.Name = "tpCheckSang";
            this.tpCheckSang.Padding = new System.Windows.Forms.Padding(3);
            this.tpCheckSang.Size = new System.Drawing.Size(1347, 777);
            this.tpCheckSang.TabIndex = 0;
            this.tpCheckSang.Text = "Buổi Sáng ";
            // 
            // artanPannel1
            // 
            this.artanPannel1.BackColor = System.Drawing.Color.White;
            this.artanPannel1.BorderRadius = 60;
            this.artanPannel1.Controls.Add(this.label3);
            this.artanPannel1.ForeColor = System.Drawing.Color.Black;
            this.artanPannel1.GradientAngle = 90F;
            this.artanPannel1.GradientBttomColor = System.Drawing.Color.DarkOrchid;
            this.artanPannel1.GradientTopcolor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(95)))), ((int)(((byte)(231)))));
            this.artanPannel1.Location = new System.Drawing.Point(458, 16);
            this.artanPannel1.Name = "artanPannel1";
            this.artanPannel1.Padding = new System.Windows.Forms.Padding(10);
            this.artanPannel1.Size = new System.Drawing.Size(444, 64);
            this.artanPannel1.TabIndex = 40;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(424, 44);
            this.label3.TabIndex = 37;
            this.label3.Text = "Thông Tin Điểm Danh";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlThongtinCheckIn
            // 
            this.pnlThongtinCheckIn.BackColor = System.Drawing.Color.White;
            this.pnlThongtinCheckIn.BorderRadius = 50;
            this.pnlThongtinCheckIn.Controls.Add(this.btnSubmitSang);
            this.pnlThongtinCheckIn.Controls.Add(this.dtpCheckIn);
            this.pnlThongtinCheckIn.Controls.Add(this.txtMacvsang);
            this.pnlThongtinCheckIn.Controls.Add(this.txtManvsang);
            this.pnlThongtinCheckIn.Controls.Add(this.pnlgridviewsang);
            this.pnlThongtinCheckIn.Controls.Add(this.label8);
            this.pnlThongtinCheckIn.Controls.Add(this.label4);
            this.pnlThongtinCheckIn.Controls.Add(this.cbCheckInsang);
            this.pnlThongtinCheckIn.Controls.Add(this.label10);
            this.pnlThongtinCheckIn.ForeColor = System.Drawing.Color.Black;
            this.pnlThongtinCheckIn.GradientAngle = 90F;
            this.pnlThongtinCheckIn.GradientBttomColor = System.Drawing.Color.WhiteSmoke;
            this.pnlThongtinCheckIn.GradientTopcolor = System.Drawing.Color.WhiteSmoke;
            this.pnlThongtinCheckIn.Location = new System.Drawing.Point(264, 102);
            this.pnlThongtinCheckIn.Name = "pnlThongtinCheckIn";
            this.pnlThongtinCheckIn.Size = new System.Drawing.Size(847, 601);
            this.pnlThongtinCheckIn.TabIndex = 51;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(90, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(176, 30);
            this.label10.TabIndex = 29;
            this.label10.Text = "Mã Nhân viên ";
            // 
            // cbCheckInsang
            // 
            this.cbCheckInsang.BackColor = System.Drawing.Color.Transparent;
            this.cbCheckInsang.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCheckInsang.ForeColor = System.Drawing.Color.Black;
            this.cbCheckInsang.Location = new System.Drawing.Point(554, 119);
            this.cbCheckInsang.Name = "cbCheckInsang";
            this.cbCheckInsang.Size = new System.Drawing.Size(109, 50);
            this.cbCheckInsang.TabIndex = 32;
            this.cbCheckInsang.Text = "Now";
            this.cbCheckInsang.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(459, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 33);
            this.label4.TabIndex = 36;
            this.label4.Text = "Mã chức vụ";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(90, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 30);
            this.label8.TabIndex = 30;
            this.label8.Text = "Check In ";
            // 
            // pnlgridviewsang
            // 
            this.pnlgridviewsang.BackColor = System.Drawing.Color.White;
            this.pnlgridviewsang.BorderRadius = 30;
            this.pnlgridviewsang.Controls.Add(this.gvChecksang);
            this.pnlgridviewsang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(94)))), ((int)(((byte)(231)))));
            this.pnlgridviewsang.GradientAngle = 90F;
            this.pnlgridviewsang.GradientBttomColor = System.Drawing.Color.DarkOrchid;
            this.pnlgridviewsang.GradientTopcolor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(95)))), ((int)(((byte)(231)))));
            this.pnlgridviewsang.Location = new System.Drawing.Point(89, 262);
            this.pnlgridviewsang.Name = "pnlgridviewsang";
            this.pnlgridviewsang.Padding = new System.Windows.Forms.Padding(10);
            this.pnlgridviewsang.Size = new System.Drawing.Size(674, 306);
            this.pnlgridviewsang.TabIndex = 50;
            // 
            // gvChecksang
            // 
            this.gvChecksang.AllowUserToResizeColumns = false;
            this.gvChecksang.AllowUserToResizeRows = false;
            this.gvChecksang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.gvChecksang.BackgroundColor = System.Drawing.Color.WhiteSmoke;
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
            this.gvChecksang.Size = new System.Drawing.Size(654, 286);
            this.gvChecksang.TabIndex = 38;
            // 
            // txtManvsang
            // 
            this.txtManvsang.BackColor = System.Drawing.Color.White;
            this.txtManvsang.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtManvsang.BorderSize = 2;
            this.txtManvsang.ForeColor = System.Drawing.Color.Black;
            this.txtManvsang.Location = new System.Drawing.Point(272, 54);
            this.txtManvsang.Margin = new System.Windows.Forms.Padding(4);
            this.txtManvsang.Multiline = false;
            this.txtManvsang.Name = "txtManvsang";
            this.txtManvsang.Padding = new System.Windows.Forms.Padding(7);
            this.txtManvsang.Password = false;
            this.txtManvsang.Size = new System.Drawing.Size(132, 43);
            this.txtManvsang.TabIndex = 59;
            this.txtManvsang.Texts = "";
            this.txtManvsang.UnderlinedStyle = false;
            // 
            // txtMacvsang
            // 
            this.txtMacvsang.BackColor = System.Drawing.Color.White;
            this.txtMacvsang.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtMacvsang.BorderSize = 2;
            this.txtMacvsang.ForeColor = System.Drawing.Color.Black;
            this.txtMacvsang.Location = new System.Drawing.Point(613, 54);
            this.txtMacvsang.Margin = new System.Windows.Forms.Padding(4);
            this.txtMacvsang.Multiline = false;
            this.txtMacvsang.Name = "txtMacvsang";
            this.txtMacvsang.Padding = new System.Windows.Forms.Padding(7);
            this.txtMacvsang.Password = false;
            this.txtMacvsang.Size = new System.Drawing.Size(120, 43);
            this.txtMacvsang.TabIndex = 60;
            this.txtMacvsang.Texts = "";
            this.txtMacvsang.UnderlinedStyle = false;
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.dtpCheckIn.BorderSize = 0;
            this.dtpCheckIn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckIn.Location = new System.Drawing.Point(272, 124);
            this.dtpCheckIn.MinimumSize = new System.Drawing.Size(4, 35);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(257, 35);
            this.dtpCheckIn.SkinColor = System.Drawing.Color.MediumSlateBlue;
            this.dtpCheckIn.TabIndex = 62;
            this.dtpCheckIn.TextColor = System.Drawing.Color.White;
            // 
            // btnSubmitSang
            // 
            this.btnSubmitSang.BackColor = System.Drawing.Color.Azure;
            this.btnSubmitSang.BackgroundColor = System.Drawing.Color.Azure;
            this.btnSubmitSang.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSubmitSang.BorderRadius = 10;
            this.btnSubmitSang.BorderSize = 1;
            this.btnSubmitSang.FlatAppearance.BorderSize = 0;
            this.btnSubmitSang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmitSang.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmitSang.ForeColor = System.Drawing.Color.Black;
            this.btnSubmitSang.Location = new System.Drawing.Point(630, 183);
            this.btnSubmitSang.Name = "btnSubmitSang";
            this.btnSubmitSang.Size = new System.Drawing.Size(103, 43);
            this.btnSubmitSang.TabIndex = 88;
            this.btnSubmitSang.Text = "Submit";
            this.btnSubmitSang.TextColor = System.Drawing.Color.Black;
            this.btnSubmitSang.UseVisualStyleBackColor = false;
            this.btnSubmitSang.Click += new System.EventHandler(this.btnSubmitSang_Click);
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
            this.artanPannel1.ResumeLayout(false);
            this.pnlThongtinCheckIn.ResumeLayout(false);
            this.pnlThongtinCheckIn.PerformLayout();
            this.pnlgridviewsang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvChecksang)).EndInit();
            this.tabDiemdanh.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tpCheckSang;
        private ArtanComponent.ArtanPannel pnlThongtinCheckIn;
        private VBButton btnSubmitSang;
        private CTDateTimePicker dtpCheckIn;
        private CTTextBox txtMacvsang;
        private CTTextBox txtManvsang;
        private ArtanComponent.ArtanPannel pnlgridviewsang;
        private System.Windows.Forms.DataGridView gvChecksang;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbCheckInsang;
        private System.Windows.Forms.Label label10;
        private ArtanComponent.ArtanPannel artanPannel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabDiemdanh;
    }
}