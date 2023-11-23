namespace QLCongTy.Views.MainMenu
{
    partial class fDoiMatKhau
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
            this.pnlBaoMat = new System.Windows.Forms.Panel();
            this.pnlDoiMatKhau = new System.Windows.Forms.Panel();
            this.btnUpdateMatKhau = new QLCongTy.VBButton();
            this.txtMatKhauMoi = new System.Windows.Forms.TextBox();
            this.txtMatKhauCu = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.pnlBaoMat.SuspendLayout();
            this.pnlDoiMatKhau.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBaoMat
            // 
            this.pnlBaoMat.Controls.Add(this.pnlDoiMatKhau);
            this.pnlBaoMat.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBaoMat.Location = new System.Drawing.Point(0, 0);
            this.pnlBaoMat.Name = "pnlBaoMat";
            this.pnlBaoMat.Size = new System.Drawing.Size(1337, 775);
            this.pnlBaoMat.TabIndex = 79;
            // 
            // pnlDoiMatKhau
            // 
            this.pnlDoiMatKhau.BackColor = System.Drawing.Color.White;
            this.pnlDoiMatKhau.Controls.Add(this.btnUpdateMatKhau);
            this.pnlDoiMatKhau.Controls.Add(this.txtMatKhauMoi);
            this.pnlDoiMatKhau.Controls.Add(this.txtMatKhauCu);
            this.pnlDoiMatKhau.Controls.Add(this.label18);
            this.pnlDoiMatKhau.Controls.Add(this.label19);
            this.pnlDoiMatKhau.Location = new System.Drawing.Point(409, 200);
            this.pnlDoiMatKhau.Name = "pnlDoiMatKhau";
            this.pnlDoiMatKhau.Size = new System.Drawing.Size(529, 295);
            this.pnlDoiMatKhau.TabIndex = 89;
            // 
            // btnUpdateMatKhau
            // 
            this.btnUpdateMatKhau.BackColor = System.Drawing.Color.White;
            this.btnUpdateMatKhau.BackgroundColor = System.Drawing.Color.White;
            this.btnUpdateMatKhau.BorderColor = System.Drawing.Color.LightGray;
            this.btnUpdateMatKhau.BorderRadius = 15;
            this.btnUpdateMatKhau.BorderSize = 1;
            this.btnUpdateMatKhau.FlatAppearance.BorderSize = 0;
            this.btnUpdateMatKhau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateMatKhau.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateMatKhau.ForeColor = System.Drawing.Color.Black;
            this.btnUpdateMatKhau.Location = new System.Drawing.Point(360, 208);
            this.btnUpdateMatKhau.Name = "btnUpdateMatKhau";
            this.btnUpdateMatKhau.Size = new System.Drawing.Size(116, 58);
            this.btnUpdateMatKhau.TabIndex = 82;
            this.btnUpdateMatKhau.Text = "Update";
            this.btnUpdateMatKhau.TextColor = System.Drawing.Color.Black;
            this.btnUpdateMatKhau.UseVisualStyleBackColor = false;
            this.btnUpdateMatKhau.Click += new System.EventHandler(this.btnUpdateMatKhau_Click);
            // 
            // txtMatKhauMoi
            // 
            this.txtMatKhauMoi.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatKhauMoi.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtMatKhauMoi.Location = new System.Drawing.Point(253, 129);
            this.txtMatKhauMoi.Name = "txtMatKhauMoi";
            this.txtMatKhauMoi.Size = new System.Drawing.Size(223, 42);
            this.txtMatKhauMoi.TabIndex = 84;
            // 
            // txtMatKhauCu
            // 
            this.txtMatKhauCu.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatKhauCu.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtMatKhauCu.Location = new System.Drawing.Point(253, 40);
            this.txtMatKhauCu.Name = "txtMatKhauCu";
            this.txtMatKhauCu.Size = new System.Drawing.Size(223, 42);
            this.txtMatKhauCu.TabIndex = 83;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label18.Location = new System.Drawing.Point(32, 134);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(161, 31);
            this.label18.TabIndex = 81;
            this.label18.Text = "Mật khẩu mới:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label19.Location = new System.Drawing.Point(32, 45);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(145, 31);
            this.label19.TabIndex = 80;
            this.label19.Text = "Mật khẩu cũ:";
            // 
            // fDoiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1337, 772);
            this.Controls.Add(this.pnlBaoMat);
            this.Name = "fDoiMatKhau";
            this.Text = "fDoiMatKhau";
            this.pnlBaoMat.ResumeLayout(false);
            this.pnlDoiMatKhau.ResumeLayout(false);
            this.pnlDoiMatKhau.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBaoMat;
        private System.Windows.Forms.Panel pnlDoiMatKhau;
        private VBButton btnUpdateMatKhau;
        private System.Windows.Forms.TextBox txtMatKhauMoi;
        private System.Windows.Forms.TextBox txtMatKhauCu;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
    }
}