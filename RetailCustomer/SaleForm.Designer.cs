namespace RetailCustomer
{
    partial class SaleForm
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
            this.txt_search1 = new System.Windows.Forms.TextBox();
            this.txt_search2 = new System.Windows.Forms.TextBox();
            this.list_saleitems = new System.Windows.Forms.ListView();
            this.Btn_CloseSale = new System.Windows.Forms.Button();
            this.Btn_recievereturn = new System.Windows.Forms.Button();
            this.btn_suspend = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.lbl_name = new System.Windows.Forms.Label();
            this.lbl_productname = new System.Windows.Forms.Label();
            this.lbl_items = new System.Windows.Forms.Label();
            this.lbl_quantity = new System.Windows.Forms.Label();
            this.lbl_delete = new System.Windows.Forms.Label();
            this.lbl_price = new System.Windows.Forms.Label();
            this.lbl_total1 = new System.Windows.Forms.Label();
            this.lbl_itemsincart = new System.Windows.Forms.Label();
            this.lbl_subtotal = new System.Windows.Forms.Label();
            this.lbl_discount = new System.Windows.Forms.Label();
            this.lbl_discountinrupees = new System.Windows.Forms.Label();
            this.lbl_total2 = new System.Windows.Forms.Label();
            this.lbl_addpayment = new System.Windows.Forms.Label();
            this.txt_discount = new System.Windows.Forms.TextBox();
            this.txt_discountinrupees = new System.Windows.Forms.TextBox();
            this.list_suspendedsale = new System.Windows.Forms.ListView();
            this.cmb_payment = new System.Windows.Forms.ComboBox();
            this.rdb_withprint = new System.Windows.Forms.RadioButton();
            this.rdb_noprint = new System.Windows.Forms.RadioButton();
            this.lbl_suspendedsale = new System.Windows.Forms.Label();
            this.btn_saleandprint = new System.Windows.Forms.Button();
            this.txt_amount = new System.Windows.Forms.TextBox();
            this.lbl_amounttoreturn = new System.Windows.Forms.Label();
            this.lbl_todaysale = new System.Windows.Forms.Label();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_search1
            // 
            this.txt_search1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_search1.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_search1.Location = new System.Drawing.Point(18, 77);
            this.txt_search1.Name = "txt_search1";
            this.txt_search1.Size = new System.Drawing.Size(202, 21);
            this.txt_search1.TabIndex = 0;
            this.txt_search1.Text = "Search by barcode";
            this.txt_search1.TextChanged += new System.EventHandler(this.txt_search1_TextChanged);
            // 
            // txt_search2
            // 
            this.txt_search2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_search2.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_search2.Location = new System.Drawing.Point(330, 76);
            this.txt_search2.Name = "txt_search2";
            this.txt_search2.Size = new System.Drawing.Size(221, 21);
            this.txt_search2.TabIndex = 1;
            this.txt_search2.Text = "Search by product name";
            this.txt_search2.TextChanged += new System.EventHandler(this.txt_search2_TextChanged);
            // 
            // list_saleitems
            // 
            this.list_saleitems.BackColor = System.Drawing.SystemColors.Menu;
            this.list_saleitems.Location = new System.Drawing.Point(2, 107);
            this.list_saleitems.Name = "list_saleitems";
            this.list_saleitems.Size = new System.Drawing.Size(814, 526);
            this.list_saleitems.TabIndex = 16;
            this.list_saleitems.UseCompatibleStateImageBehavior = false;
            // 
            // Btn_CloseSale
            // 
            this.Btn_CloseSale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Btn_CloseSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_CloseSale.Location = new System.Drawing.Point(18, 8);
            this.Btn_CloseSale.Name = "Btn_CloseSale";
            this.Btn_CloseSale.Size = new System.Drawing.Size(221, 39);
            this.Btn_CloseSale.TabIndex = 3;
            this.Btn_CloseSale.Text = "Close current sale";
            this.Btn_CloseSale.UseVisualStyleBackColor = false;
            this.Btn_CloseSale.Click += new System.EventHandler(this.button1_Click);
            // 
            // Btn_recievereturn
            // 
            this.Btn_recievereturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Btn_recievereturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_recievereturn.Location = new System.Drawing.Point(1091, 9);
            this.Btn_recievereturn.Name = "Btn_recievereturn";
            this.Btn_recievereturn.Size = new System.Drawing.Size(227, 39);
            this.Btn_recievereturn.TabIndex = 4;
            this.Btn_recievereturn.Text = "Recieve Return";
            this.Btn_recievereturn.UseVisualStyleBackColor = false;
            // 
            // btn_suspend
            // 
            this.btn_suspend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_suspend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_suspend.Location = new System.Drawing.Point(848, 57);
            this.btn_suspend.Name = "btn_suspend";
            this.btn_suspend.Size = new System.Drawing.Size(134, 44);
            this.btn_suspend.TabIndex = 5;
            this.btn_suspend.Text = "Suspended Sale";
            this.btn_suspend.UseVisualStyleBackColor = false;
            this.btn_suspend.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Location = new System.Drawing.Point(1018, 55);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(142, 44);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "Cancel This Sale";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.button4_Click);
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_name.Location = new System.Drawing.Point(850, 114);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(90, 15);
            this.lbl_name.TabIndex = 23;
            this.lbl_name.Text = "Customer Name";
            this.lbl_name.Click += new System.EventHandler(this.lbl_name_Click);
            // 
            // lbl_productname
            // 
            this.lbl_productname.AutoSize = true;
            this.lbl_productname.Location = new System.Drawing.Point(16, 111);
            this.lbl_productname.Name = "lbl_productname";
            this.lbl_productname.Size = new System.Drawing.Size(94, 15);
            this.lbl_productname.TabIndex = 24;
            this.lbl_productname.Text = "Product Name";
            // 
            // lbl_items
            // 
            this.lbl_items.AutoSize = true;
            this.lbl_items.Location = new System.Drawing.Point(174, 111);
            this.lbl_items.Name = "lbl_items";
            this.lbl_items.Size = new System.Drawing.Size(99, 15);
            this.lbl_items.TabIndex = 25;
            this.lbl_items.Text = "Items In Stock";
            this.lbl_items.Click += new System.EventHandler(this.label5_Click);
            // 
            // lbl_quantity
            // 
            this.lbl_quantity.AutoSize = true;
            this.lbl_quantity.Location = new System.Drawing.Point(327, 111);
            this.lbl_quantity.Name = "lbl_quantity";
            this.lbl_quantity.Size = new System.Drawing.Size(61, 15);
            this.lbl_quantity.TabIndex = 26;
            this.lbl_quantity.Text = "Quantity";
            // 
            // lbl_delete
            // 
            this.lbl_delete.AutoSize = true;
            this.lbl_delete.Location = new System.Drawing.Point(686, 110);
            this.lbl_delete.Name = "lbl_delete";
            this.lbl_delete.Size = new System.Drawing.Size(45, 15);
            this.lbl_delete.TabIndex = 27;
            this.lbl_delete.Text = "Delete";
            // 
            // lbl_price
            // 
            this.lbl_price.AutoSize = true;
            this.lbl_price.Location = new System.Drawing.Point(490, 111);
            this.lbl_price.Name = "lbl_price";
            this.lbl_price.Size = new System.Drawing.Size(39, 15);
            this.lbl_price.TabIndex = 28;
            this.lbl_price.Text = "Price";
            // 
            // lbl_total1
            // 
            this.lbl_total1.AutoSize = true;
            this.lbl_total1.Location = new System.Drawing.Point(594, 111);
            this.lbl_total1.Name = "lbl_total1";
            this.lbl_total1.Size = new System.Drawing.Size(40, 15);
            this.lbl_total1.TabIndex = 29;
            this.lbl_total1.Text = "Total";
            // 
            // lbl_itemsincart
            // 
            this.lbl_itemsincart.AutoSize = true;
            this.lbl_itemsincart.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_itemsincart.Location = new System.Drawing.Point(862, 159);
            this.lbl_itemsincart.Name = "lbl_itemsincart";
            this.lbl_itemsincart.Size = new System.Drawing.Size(78, 15);
            this.lbl_itemsincart.TabIndex = 30;
            this.lbl_itemsincart.Text = "Items In Cart";
            // 
            // lbl_subtotal
            // 
            this.lbl_subtotal.AutoSize = true;
            this.lbl_subtotal.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_subtotal.Location = new System.Drawing.Point(862, 186);
            this.lbl_subtotal.Name = "lbl_subtotal";
            this.lbl_subtotal.Size = new System.Drawing.Size(59, 15);
            this.lbl_subtotal.TabIndex = 31;
            this.lbl_subtotal.Text = "Sub Total";
            this.lbl_subtotal.Click += new System.EventHandler(this.lbl_subtotal_Click);
            // 
            // lbl_discount
            // 
            this.lbl_discount.AutoSize = true;
            this.lbl_discount.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_discount.Location = new System.Drawing.Point(862, 222);
            this.lbl_discount.Name = "lbl_discount";
            this.lbl_discount.Size = new System.Drawing.Size(60, 15);
            this.lbl_discount.TabIndex = 32;
            this.lbl_discount.Text = "Discount%";
            // 
            // lbl_discountinrupees
            // 
            this.lbl_discountinrupees.AutoSize = true;
            this.lbl_discountinrupees.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_discountinrupees.Location = new System.Drawing.Point(850, 249);
            this.lbl_discountinrupees.Name = "lbl_discountinrupees";
            this.lbl_discountinrupees.Size = new System.Drawing.Size(137, 18);
            this.lbl_discountinrupees.TabIndex = 33;
            this.lbl_discountinrupees.Text = "Discount In Rupees";
            // 
            // lbl_total2
            // 
            this.lbl_total2.AutoSize = true;
            this.lbl_total2.Location = new System.Drawing.Point(862, 307);
            this.lbl_total2.Name = "lbl_total2";
            this.lbl_total2.Size = new System.Drawing.Size(40, 15);
            this.lbl_total2.TabIndex = 34;
            this.lbl_total2.Text = "Total";
            // 
            // lbl_addpayment
            // 
            this.lbl_addpayment.AutoSize = true;
            this.lbl_addpayment.Location = new System.Drawing.Point(862, 348);
            this.lbl_addpayment.Name = "lbl_addpayment";
            this.lbl_addpayment.Size = new System.Drawing.Size(89, 15);
            this.lbl_addpayment.TabIndex = 36;
            this.lbl_addpayment.Text = "Add Payment";
            // 
            // txt_discount
            // 
            this.txt_discount.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_discount.Location = new System.Drawing.Point(994, 219);
            this.txt_discount.Name = "txt_discount";
            this.txt_discount.Size = new System.Drawing.Size(166, 21);
            this.txt_discount.TabIndex = 8;
            this.txt_discount.TextChanged += new System.EventHandler(this.txt_discountinrupees_TextChanged);
            // 
            // txt_discountinrupees
            // 
            this.txt_discountinrupees.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_discountinrupees.Location = new System.Drawing.Point(994, 275);
            this.txt_discountinrupees.Name = "txt_discountinrupees";
            this.txt_discountinrupees.Size = new System.Drawing.Size(166, 21);
            this.txt_discountinrupees.TabIndex = 9;
            this.txt_discountinrupees.TextChanged += new System.EventHandler(this.txt_discount_TextChanged);
            // 
            // list_suspendedsale
            // 
            this.list_suspendedsale.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.list_suspendedsale.Location = new System.Drawing.Point(848, 522);
            this.list_suspendedsale.Name = "list_suspendedsale";
            this.list_suspendedsale.Size = new System.Drawing.Size(385, 111);
            this.list_suspendedsale.TabIndex = 15;
            this.list_suspendedsale.UseCompatibleStateImageBehavior = false;
            this.list_suspendedsale.SelectedIndexChanged += new System.EventHandler(this.list_suspendedsale_SelectedIndexChanged);
            // 
            // cmb_payment
            // 
            this.cmb_payment.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.cmb_payment.FormattingEnabled = true;
            this.cmb_payment.Location = new System.Drawing.Point(1040, 345);
            this.cmb_payment.Name = "cmb_payment";
            this.cmb_payment.Size = new System.Drawing.Size(93, 23);
            this.cmb_payment.TabIndex = 10;
            this.cmb_payment.SelectedIndexChanged += new System.EventHandler(this.cmb_payment_SelectedIndexChanged);
            // 
            // rdb_withprint
            // 
            this.rdb_withprint.AutoSize = true;
            this.rdb_withprint.Location = new System.Drawing.Point(854, 372);
            this.rdb_withprint.Name = "rdb_withprint";
            this.rdb_withprint.Size = new System.Drawing.Size(95, 19);
            this.rdb_withprint.TabIndex = 11;
            this.rdb_withprint.TabStop = true;
            this.rdb_withprint.Text = "With Print";
            this.rdb_withprint.UseVisualStyleBackColor = true;
            // 
            // rdb_noprint
            // 
            this.rdb_noprint.AutoSize = true;
            this.rdb_noprint.Location = new System.Drawing.Point(989, 372);
            this.rdb_noprint.Name = "rdb_noprint";
            this.rdb_noprint.Size = new System.Drawing.Size(115, 19);
            this.rdb_noprint.TabIndex = 12;
            this.rdb_noprint.TabStop = true;
            this.rdb_noprint.Text = "Without Print";
            this.rdb_noprint.UseVisualStyleBackColor = true;
            this.rdb_noprint.CheckedChanged += new System.EventHandler(this.rdb_noprint_CheckedChanged);
            // 
            // lbl_suspendedsale
            // 
            this.lbl_suspendedsale.AutoSize = true;
            this.lbl_suspendedsale.Location = new System.Drawing.Point(850, 504);
            this.lbl_suspendedsale.Name = "lbl_suspendedsale";
            this.lbl_suspendedsale.Size = new System.Drawing.Size(101, 15);
            this.lbl_suspendedsale.TabIndex = 43;
            this.lbl_suspendedsale.Text = "Suspended Sale";
            this.lbl_suspendedsale.Click += new System.EventHandler(this.label17_Click);
            // 
            // btn_saleandprint
            // 
            this.btn_saleandprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_saleandprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_saleandprint.Location = new System.Drawing.Point(1040, 399);
            this.btn_saleandprint.Name = "btn_saleandprint";
            this.btn_saleandprint.Size = new System.Drawing.Size(194, 37);
            this.btn_saleandprint.TabIndex = 14;
            this.btn_saleandprint.Text = "Sale And Print";
            this.btn_saleandprint.UseVisualStyleBackColor = false;
            this.btn_saleandprint.Click += new System.EventHandler(this.btn_saleandprint_Click);
            // 
            // txt_amount
            // 
            this.txt_amount.AccessibleName = "";
            this.txt_amount.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_amount.Location = new System.Drawing.Point(848, 402);
            this.txt_amount.Multiline = true;
            this.txt_amount.Name = "txt_amount";
            this.txt_amount.Size = new System.Drawing.Size(166, 33);
            this.txt_amount.TabIndex = 13;
            this.txt_amount.TextChanged += new System.EventHandler(this.txt_amount_TextChanged);
            // 
            // lbl_amounttoreturn
            // 
            this.lbl_amounttoreturn.AutoSize = true;
            this.lbl_amounttoreturn.Location = new System.Drawing.Point(1074, 457);
            this.lbl_amounttoreturn.Name = "lbl_amounttoreturn";
            this.lbl_amounttoreturn.Size = new System.Drawing.Size(117, 15);
            this.lbl_amounttoreturn.TabIndex = 46;
            this.lbl_amounttoreturn.Text = "Amount to return";
            // 
            // lbl_todaysale
            // 
            this.lbl_todaysale.AutoSize = true;
            this.lbl_todaysale.Location = new System.Drawing.Point(525, 21);
            this.lbl_todaysale.Name = "lbl_todaysale";
            this.lbl_todaysale.Size = new System.Drawing.Size(167, 15);
            this.lbl_todaysale.TabIndex = 47;
            this.lbl_todaysale.Text = "Todays Sale(till now) Rs.";
            // 
            // txt_Name
            // 
            this.txt_Name.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txt_Name.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Name.Location = new System.Drawing.Point(989, 114);
            this.txt_Name.Multiline = true;
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(166, 21);
            this.txt_Name.TabIndex = 7;
            this.txt_Name.TextChanged += new System.EventHandler(this.txt_Name_TextChanged);
            // 
            // SaleForm
            // 
            this.AccessibleName = "sale";
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MintCream;
            this.ClientSize = new System.Drawing.Size(1210, 617);
            this.Controls.Add(this.lbl_todaysale);
            this.Controls.Add(this.lbl_amounttoreturn);
            this.Controls.Add(this.txt_amount);
            this.Controls.Add(this.btn_saleandprint);
            this.Controls.Add(this.lbl_suspendedsale);
            this.Controls.Add(this.rdb_noprint);
            this.Controls.Add(this.rdb_withprint);
            this.Controls.Add(this.cmb_payment);
            this.Controls.Add(this.list_suspendedsale);
            this.Controls.Add(this.txt_discountinrupees);
            this.Controls.Add(this.txt_discount);
            this.Controls.Add(this.lbl_addpayment);
            this.Controls.Add(this.lbl_total2);
            this.Controls.Add(this.lbl_discountinrupees);
            this.Controls.Add(this.lbl_discount);
            this.Controls.Add(this.lbl_subtotal);
            this.Controls.Add(this.lbl_itemsincart);
            this.Controls.Add(this.lbl_total1);
            this.Controls.Add(this.lbl_price);
            this.Controls.Add(this.lbl_delete);
            this.Controls.Add(this.lbl_quantity);
            this.Controls.Add(this.lbl_items);
            this.Controls.Add(this.lbl_productname);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_suspend);
            this.Controls.Add(this.Btn_recievereturn);
            this.Controls.Add(this.Btn_CloseSale);
            this.Controls.Add(this.list_saleitems);
            this.Controls.Add(this.txt_search2);
            this.Controls.Add(this.txt_search1);
            this.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SaleForm";
            this.Text = "SaleForm";
            this.Load += new System.EventHandler(this.SaleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_search1;
        private System.Windows.Forms.TextBox txt_search2;
        private System.Windows.Forms.ListView list_saleitems;
        private System.Windows.Forms.Button Btn_CloseSale;
        private System.Windows.Forms.Button Btn_recievereturn;
        private System.Windows.Forms.Button btn_suspend;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label lbl_productname;
        private System.Windows.Forms.Label lbl_items;
        private System.Windows.Forms.Label lbl_quantity;
        private System.Windows.Forms.Label lbl_delete;
        private System.Windows.Forms.Label lbl_price;
        private System.Windows.Forms.Label lbl_total1;
        private System.Windows.Forms.Label lbl_itemsincart;
        private System.Windows.Forms.Label lbl_subtotal;
        private System.Windows.Forms.Label lbl_discount;
        private System.Windows.Forms.Label lbl_discountinrupees;
        private System.Windows.Forms.Label lbl_total2;
        private System.Windows.Forms.Label lbl_addpayment;
        private System.Windows.Forms.TextBox txt_discount;
        private System.Windows.Forms.TextBox txt_discountinrupees;
        private System.Windows.Forms.ListView list_suspendedsale;
        private System.Windows.Forms.ComboBox cmb_payment;
        private System.Windows.Forms.RadioButton rdb_withprint;
        private System.Windows.Forms.RadioButton rdb_noprint;
        private System.Windows.Forms.Label lbl_suspendedsale;
        private System.Windows.Forms.Button btn_saleandprint;
        private System.Windows.Forms.TextBox txt_amount;
        private System.Windows.Forms.Label lbl_amounttoreturn;
        private System.Windows.Forms.Label lbl_todaysale;
        private System.Windows.Forms.TextBox txt_Name;
    }
}