namespace MailChimpSync.ConfigWizard
{
    partial class Page4
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbEmailAddress = new System.Windows.Forms.ComboBox();
            this.lbSelectedNameColumns = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbAvailableColumns = new System.Windows.Forms.ListBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnDeselect = new System.Windows.Forms.Button();
            this.lblEmailAddress = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbEmailAddress2 = new System.Windows.Forms.ComboBox();
            this.lblEmailAddress2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email address column";
            // 
            // cbEmailAddress
            // 
            this.cbEmailAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEmailAddress.FormattingEnabled = true;
            this.cbEmailAddress.Location = new System.Drawing.Point(173, 60);
            this.cbEmailAddress.Name = "cbEmailAddress";
            this.cbEmailAddress.Size = new System.Drawing.Size(120, 24);
            this.cbEmailAddress.TabIndex = 1;
            this.cbEmailAddress.SelectedIndexChanged += new System.EventHandler(this.CheckBoxEmailAddress_SelectedIndexChanged);
            // 
            // lbSelectedNameColumns
            // 
            this.lbSelectedNameColumns.FormattingEnabled = true;
            this.lbSelectedNameColumns.ItemHeight = 16;
            this.lbSelectedNameColumns.Location = new System.Drawing.Point(387, 129);
            this.lbSelectedNameColumns.Name = "lbSelectedNameColumns";
            this.lbSelectedNameColumns.Size = new System.Drawing.Size(120, 84);
            this.lbSelectedNameColumns.TabIndex = 2;
            this.lbSelectedNameColumns.SelectedIndexChanged += new System.EventHandler(this.LisBoxSelectedFields_SelectedIndexChanged);
            this.lbSelectedNameColumns.DoubleClick += new System.EventHandler(this.BtnDeselect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Full name columns";
            // 
            // lbAvailableColumns
            // 
            this.lbAvailableColumns.FormattingEnabled = true;
            this.lbAvailableColumns.ItemHeight = 16;
            this.lbAvailableColumns.Location = new System.Drawing.Point(173, 130);
            this.lbAvailableColumns.Name = "lbAvailableColumns";
            this.lbAvailableColumns.Size = new System.Drawing.Size(120, 84);
            this.lbAvailableColumns.TabIndex = 2;
            this.lbAvailableColumns.DoubleClick += new System.EventHandler(this.BtnSelect_Click);
            this.lbAvailableColumns.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxAvailableColumns_MouseDoubleClick);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(325, 130);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(30, 30);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = ">";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // btnDeselect
            // 
            this.btnDeselect.Location = new System.Drawing.Point(325, 183);
            this.btnDeselect.Name = "btnDeselect";
            this.btnDeselect.Size = new System.Drawing.Size(30, 30);
            this.btnDeselect.TabIndex = 3;
            this.btnDeselect.Text = "<";
            this.btnDeselect.UseVisualStyleBackColor = true;
            this.btnDeselect.Click += new System.EventHandler(this.BtnDeselect_Click);
            // 
            // lblEmailAddress
            // 
            this.lblEmailAddress.AutoSize = true;
            this.lblEmailAddress.ForeColor = System.Drawing.Color.DimGray;
            this.lblEmailAddress.Location = new System.Drawing.Point(309, 63);
            this.lblEmailAddress.Name = "lblEmailAddress";
            this.lblEmailAddress.Size = new System.Drawing.Size(13, 17);
            this.lblEmailAddress.TabIndex = 4;
            this.lblEmailAddress.Text = "-";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.ForeColor = System.Drawing.Color.DimGray;
            this.lblFullName.Location = new System.Drawing.Point(513, 129);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(46, 17);
            this.lblFullName.TabIndex = 4;
            this.lblFullName.Text = "label3";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(389, 35);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select column where to find the email address and which columns make up the full " +
    "name (order may be important!)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Email address column 2";
            // 
            // cbEmailAddress2
            // 
            this.cbEmailAddress2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEmailAddress2.FormattingEnabled = true;
            this.cbEmailAddress2.Location = new System.Drawing.Point(173, 90);
            this.cbEmailAddress2.Name = "cbEmailAddress2";
            this.cbEmailAddress2.Size = new System.Drawing.Size(120, 24);
            this.cbEmailAddress2.TabIndex = 1;
            this.cbEmailAddress2.SelectedIndexChanged += new System.EventHandler(this.CheckBoxEmailAddress2_SelectedIndexChanged);
            // 
            // lblEmailAddress2
            // 
            this.lblEmailAddress2.AutoSize = true;
            this.lblEmailAddress2.ForeColor = System.Drawing.Color.DimGray;
            this.lblEmailAddress2.Location = new System.Drawing.Point(309, 93);
            this.lblEmailAddress2.Name = "lblEmailAddress2";
            this.lblEmailAddress2.Size = new System.Drawing.Size(13, 17);
            this.lblEmailAddress2.TabIndex = 4;
            this.lblEmailAddress2.Text = "-";
            // 
            // Page4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.lblEmailAddress2);
            this.Controls.Add(this.lblEmailAddress);
            this.Controls.Add(this.btnDeselect);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lbAvailableColumns);
            this.Controls.Add(this.cbEmailAddress2);
            this.Controls.Add(this.lbSelectedNameColumns);
            this.Controls.Add(this.cbEmailAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "Page4";
            this.Size = new System.Drawing.Size(651, 278);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbEmailAddress;
        private System.Windows.Forms.ListBox lbSelectedNameColumns;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbAvailableColumns;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnDeselect;
        private System.Windows.Forms.Label lblEmailAddress;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbEmailAddress2;
        private System.Windows.Forms.Label lblEmailAddress2;
    }
}
