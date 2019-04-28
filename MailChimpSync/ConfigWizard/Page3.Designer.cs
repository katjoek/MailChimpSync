namespace MailChimpSync.ConfigWizard
{
    partial class Page3
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
            this.eColunmNamesRow = new System.Windows.Forms.NumericUpDown();
            this.cbColumnNamesPresent = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.eFirstDataRow = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.eHeaderRowContents = new System.Windows.Forms.TextBox();
            this.eFirstDataRowContents = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.eColunmNamesRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eFirstDataRow)).BeginInit();
            this.SuspendLayout();
            // 
            // eColunmNamesRow
            // 
            this.eColunmNamesRow.Location = new System.Drawing.Point(285, 52);
            this.eColunmNamesRow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.eColunmNamesRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.eColunmNamesRow.Name = "eColunmNamesRow";
            this.eColunmNamesRow.Size = new System.Drawing.Size(67, 22);
            this.eColunmNamesRow.TabIndex = 2;
            this.eColunmNamesRow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.eColunmNamesRow.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // cbColumnNamesPresent
            // 
            this.cbColumnNamesPresent.AutoSize = true;
            this.cbColumnNamesPresent.Checked = true;
            this.cbColumnNamesPresent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbColumnNamesPresent.Location = new System.Drawing.Point(33, 52);
            this.cbColumnNamesPresent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbColumnNamesPresent.Name = "cbColumnNamesPresent";
            this.cbColumnNamesPresent.Size = new System.Drawing.Size(246, 21);
            this.cbColumnNamesPresent.TabIndex = 1;
            this.cbColumnNamesPresent.Text = "Column names are present on row";
            this.cbColumnNamesPresent.UseVisualStyleBackColor = true;
            this.cbColumnNamesPresent.CheckedChanged += new System.EventHandler(this.ColumnNamesPresent_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "First data row";
            // 
            // eFirstDataRow
            // 
            this.eFirstDataRow.Location = new System.Drawing.Point(285, 91);
            this.eFirstDataRow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.eFirstDataRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.eFirstDataRow.Name = "eFirstDataRow";
            this.eFirstDataRow.Size = new System.Drawing.Size(67, 22);
            this.eFirstDataRow.TabIndex = 5;
            this.eFirstDataRow.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.eFirstDataRow.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Excel file contents information:";
            // 
            // eHeaderRowContents
            // 
            this.eHeaderRowContents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eHeaderRowContents.BackColor = System.Drawing.SystemColors.Control;
            this.eHeaderRowContents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.eHeaderRowContents.ForeColor = System.Drawing.Color.DimGray;
            this.eHeaderRowContents.Location = new System.Drawing.Point(372, 54);
            this.eHeaderRowContents.Margin = new System.Windows.Forms.Padding(4);
            this.eHeaderRowContents.Name = "eHeaderRowContents";
            this.eHeaderRowContents.ReadOnly = true;
            this.eHeaderRowContents.Size = new System.Drawing.Size(75, 15);
            this.eHeaderRowContents.TabIndex = 3;
            this.eHeaderRowContents.TabStop = false;
            this.eHeaderRowContents.Text = "-";
            // 
            // eFirstDataRowContents
            // 
            this.eFirstDataRowContents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eFirstDataRowContents.BackColor = System.Drawing.SystemColors.Control;
            this.eFirstDataRowContents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.eFirstDataRowContents.ForeColor = System.Drawing.Color.DimGray;
            this.eFirstDataRowContents.Location = new System.Drawing.Point(372, 96);
            this.eFirstDataRowContents.Margin = new System.Windows.Forms.Padding(4);
            this.eFirstDataRowContents.Name = "eFirstDataRowContents";
            this.eFirstDataRowContents.ReadOnly = true;
            this.eFirstDataRowContents.Size = new System.Drawing.Size(75, 15);
            this.eFirstDataRowContents.TabIndex = 6;
            this.eFirstDataRowContents.TabStop = false;
            this.eFirstDataRowContents.Text = "-";
            // 
            // Page3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.eFirstDataRowContents);
            this.Controls.Add(this.eHeaderRowContents);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbColumnNamesPresent);
            this.Controls.Add(this.eFirstDataRow);
            this.Controls.Add(this.eColunmNamesRow);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Page3";
            this.Size = new System.Drawing.Size(451, 270);
            ((System.ComponentModel.ISupportInitialize)(this.eColunmNamesRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eFirstDataRow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown eColunmNamesRow;
        private System.Windows.Forms.CheckBox cbColumnNamesPresent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown eFirstDataRow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox eHeaderRowContents;
        private System.Windows.Forms.TextBox eFirstDataRowContents;
    }
}
