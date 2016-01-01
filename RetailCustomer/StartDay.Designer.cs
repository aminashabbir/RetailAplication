namespace RetailCustomer
{
    partial class StartDay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartDay));
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.nup_addamount = new System.Windows.Forms.NumericUpDown();
            this.lbl_addstart = new System.Windows.Forms.Label();
            this.btn_startday = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nup_addamount)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.CalendarForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.dateTimePicker1.CalendarMonthBackground = System.Drawing.SystemColors.InactiveCaption;
            this.dateTimePicker1.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateTimePicker1.CalendarTrailingForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(182, 124);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(243, 22);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // nup_addamount
            // 
            this.nup_addamount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nup_addamount.Location = new System.Drawing.Point(182, 215);
            this.nup_addamount.Name = "nup_addamount";
            this.nup_addamount.Size = new System.Drawing.Size(243, 20);
            this.nup_addamount.TabIndex = 1;
            // 
            // lbl_addstart
            // 
            this.lbl_addstart.AutoSize = true;
            this.lbl_addstart.Font = new System.Drawing.Font("Ravie", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_addstart.ForeColor = System.Drawing.Color.Gray;
            this.lbl_addstart.Image = ((System.Drawing.Image)(resources.GetObject("lbl_addstart.Image")));
            this.lbl_addstart.Location = new System.Drawing.Point(183, 165);
            this.lbl_addstart.Name = "lbl_addstart";
            this.lbl_addstart.Size = new System.Drawing.Size(242, 36);
            this.lbl_addstart.TabIndex = 2;
            this.lbl_addstart.Text = "Add Amount";
            // 
            // btn_startday
            // 
            this.btn_startday.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_startday.ForeColor = System.Drawing.Color.Silver;
            this.btn_startday.Image = ((System.Drawing.Image)(resources.GetObject("btn_startday.Image")));
            this.btn_startday.Location = new System.Drawing.Point(221, 258);
            this.btn_startday.Name = "btn_startday";
            this.btn_startday.Size = new System.Drawing.Size(168, 66);
            this.btn_startday.TabIndex = 3;
            this.btn_startday.Text = "StartDay";
            this.btn_startday.UseVisualStyleBackColor = true;
            this.btn_startday.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(239, 378);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // StartDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(612, 452);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_startday);
            this.Controls.Add(this.lbl_addstart);
            this.Controls.Add(this.nup_addamount);
            this.Controls.Add(this.dateTimePicker1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "StartDay";
            this.Text = "StartDay";
            ((System.ComponentModel.ISupportInitialize)(this.nup_addamount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.NumericUpDown nup_addamount;
        private System.Windows.Forms.Label lbl_addstart;
        private System.Windows.Forms.Button btn_startday;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}