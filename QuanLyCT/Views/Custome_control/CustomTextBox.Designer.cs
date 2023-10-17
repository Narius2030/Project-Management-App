namespace QLCongTy
{
    partial class CTTextBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textbox = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // textbox
            // 
            this.textbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textbox.Location = new System.Drawing.Point(10, 7);
            this.textbox.Name = "textbox";
            this.textbox.Size = new System.Drawing.Size(230, 18);
            this.textbox.TabIndex = 0;
            this.textbox.Click += new System.EventHandler(this.textBox1_Click);
            this.textbox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textbox.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            this.textbox.Leave += new System.EventHandler(this.textBox1_Leave);
            this.textbox.MouseEnter += new System.EventHandler(this.textBox1_MouseEnter);
            this.textbox.MouseLeave += new System.EventHandler(this.textBox1_MouseLeave);
            // 
            // CTTextBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.textbox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CTTextBox";
            this.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.Size = new System.Drawing.Size(250, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textbox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
