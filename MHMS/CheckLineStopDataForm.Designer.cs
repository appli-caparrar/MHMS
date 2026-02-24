namespace MHMS
{
    partial class CheckLineStopDataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckLineStopDataForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.FromLabel = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.DateTo = new System.Windows.Forms.DateTimePicker();
            this.panel9 = new System.Windows.Forms.Panel();
            this.DateFrom = new System.Windows.Forms.DateTimePicker();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.SectionDropdownList = new System.Windows.Forms.ComboBox();
            this.SectionDropdown = new System.Windows.Forms.ComboBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LineStopTextBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ExportBtn = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.DeleteLineStopBtn = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.LineStopDatagrid = new System.Windows.Forms.DataGridView();
            this.TopPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LineStopDatagrid)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(935, 48);
            this.TopPanel.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(7, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "DELETION OF DUPLICATE";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.FromLabel);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.GenerateButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(935, 50);
            this.panel1.TabIndex = 37;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(439, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 16);
            this.label2.TabIndex = 43;
            this.label2.Text = "To:";
            // 
            // FromLabel
            // 
            this.FromLabel.AutoSize = true;
            this.FromLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromLabel.Location = new System.Drawing.Point(246, 16);
            this.FromLabel.Name = "FromLabel";
            this.FromLabel.Size = new System.Drawing.Size(42, 16);
            this.FromLabel.TabIndex = 42;
            this.FromLabel.Text = "From:";
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.DateTo);
            this.panel8.Location = new System.Drawing.Point(468, 7);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(143, 35);
            this.panel8.TabIndex = 41;
            // 
            // DateTo
            // 
            this.DateTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTo.Location = new System.Drawing.Point(4, 5);
            this.DateTo.Name = "DateTo";
            this.DateTo.Size = new System.Drawing.Size(134, 23);
            this.DateTo.TabIndex = 40;
            this.DateTo.Value = new System.DateTime(2023, 8, 17, 0, 0, 0, 0);
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.DateFrom);
            this.panel9.Location = new System.Drawing.Point(288, 7);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(143, 35);
            this.panel9.TabIndex = 40;
            // 
            // DateFrom
            // 
            this.DateFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateFrom.Location = new System.Drawing.Point(4, 5);
            this.DateFrom.Name = "DateFrom";
            this.DateFrom.Size = new System.Drawing.Size(134, 23);
            this.DateFrom.TabIndex = 40;
            this.DateFrom.Value = new System.DateTime(2023, 8, 17, 0, 0, 0, 0);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.SectionDropdown);
            this.panel5.Location = new System.Drawing.Point(13, 7);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(225, 35);
            this.panel5.TabIndex = 39;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.SectionDropdownList);
            this.panel7.Location = new System.Drawing.Point(-1, -1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(225, 35);
            this.panel7.TabIndex = 40;
            // 
            // SectionDropdownList
            // 
            this.SectionDropdownList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SectionDropdownList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SectionDropdownList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SectionDropdownList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SectionDropdownList.FormattingEnabled = true;
            this.SectionDropdownList.Items.AddRange(new object[] {
            "Ink Cartridge",
            "Ink Head",
            "Molding",
            "Molding Production",
            "PCBA",
            "P-Touch",
            "Printer 1",
            "Printer 2",
            "Tape Cassette"});
            this.SectionDropdownList.Location = new System.Drawing.Point(3, 5);
            this.SectionDropdownList.Name = "SectionDropdownList";
            this.SectionDropdownList.Size = new System.Drawing.Size(217, 24);
            this.SectionDropdownList.TabIndex = 3;
            this.SectionDropdownList.Text = "Select Section";
            // 
            // SectionDropdown
            // 
            this.SectionDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SectionDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SectionDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SectionDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SectionDropdown.FormattingEnabled = true;
            this.SectionDropdown.Items.AddRange(new object[] {
            "Ink Cartridge",
            "Ink Head",
            "Molding",
            "PCBA",
            "P-Touch",
            "Printer",
            "Tape Cassette"});
            this.SectionDropdown.Location = new System.Drawing.Point(3, 5);
            this.SectionDropdown.Name = "SectionDropdown";
            this.SectionDropdown.Size = new System.Drawing.Size(217, 24);
            this.SectionDropdown.TabIndex = 3;
            this.SectionDropdown.Text = "Select Section";
            // 
            // GenerateButton
            // 
            this.GenerateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.GenerateButton.FlatAppearance.BorderSize = 0;
            this.GenerateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenerateButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateButton.ForeColor = System.Drawing.Color.White;
            this.GenerateButton.Image = global::MHMS.Properties.Resources.available_updates_24;
            this.GenerateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GenerateButton.Location = new System.Drawing.Point(624, 7);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(190, 35);
            this.GenerateButton.TabIndex = 38;
            this.GenerateButton.Text = "  GENERATE DUPLICATE";
            this.GenerateButton.UseVisualStyleBackColor = false;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.LineStopTextBox);
            this.panel2.Location = new System.Drawing.Point(12, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(403, 35);
            this.panel2.TabIndex = 37;
            this.panel2.Visible = false;
            // 
            // LineStopTextBox
            // 
            this.LineStopTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LineStopTextBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LineStopTextBox.Location = new System.Drawing.Point(6, 7);
            this.LineStopTextBox.Name = "LineStopTextBox";
            this.LineStopTextBox.Size = new System.Drawing.Size(392, 18);
            this.LineStopTextBox.TabIndex = 1;
            this.LineStopTextBox.Text = "Type or Paste Linestop";
            this.LineStopTextBox.MouseEnter += new System.EventHandler(this.LineStopTextBox_MouseEnter);
            this.LineStopTextBox.MouseLeave += new System.EventHandler(this.LineStopTextBox_MouseLeave);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.ExportBtn);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.DeleteLineStopBtn);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 498);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(13, 5, 13, 5);
            this.panel3.Size = new System.Drawing.Size(935, 49);
            this.panel3.TabIndex = 38;
            // 
            // ExportBtn
            // 
            this.ExportBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.ExportBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.ExportBtn.FlatAppearance.BorderSize = 0;
            this.ExportBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportBtn.ForeColor = System.Drawing.Color.White;
            this.ExportBtn.Image = global::MHMS.Properties.Resources.download_2_24__2_;
            this.ExportBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ExportBtn.Location = new System.Drawing.Point(666, 5);
            this.ExportBtn.Name = "ExportBtn";
            this.ExportBtn.Size = new System.Drawing.Size(121, 39);
            this.ExportBtn.TabIndex = 41;
            this.ExportBtn.Text = "  EXPORT";
            this.ExportBtn.UseVisualStyleBackColor = false;
            this.ExportBtn.Click += new System.EventHandler(this.ExportBtn_Click_1);
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(787, 5);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(15, 39);
            this.panel6.TabIndex = 40;
            // 
            // DeleteLineStopBtn
            // 
            this.DeleteLineStopBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(46)))), ((int)(((byte)(74)))));
            this.DeleteLineStopBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.DeleteLineStopBtn.FlatAppearance.BorderSize = 0;
            this.DeleteLineStopBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteLineStopBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteLineStopBtn.ForeColor = System.Drawing.Color.White;
            this.DeleteLineStopBtn.Image = ((System.Drawing.Image)(resources.GetObject("DeleteLineStopBtn.Image")));
            this.DeleteLineStopBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DeleteLineStopBtn.Location = new System.Drawing.Point(802, 5);
            this.DeleteLineStopBtn.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.DeleteLineStopBtn.Name = "DeleteLineStopBtn";
            this.DeleteLineStopBtn.Size = new System.Drawing.Size(120, 39);
            this.DeleteLineStopBtn.TabIndex = 11;
            this.DeleteLineStopBtn.Text = "DELETE";
            this.DeleteLineStopBtn.UseVisualStyleBackColor = false;
            this.DeleteLineStopBtn.Click += new System.EventHandler(this.DeleteLineStopBtn_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.Controls.Add(this.LineStopDatagrid);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 98);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(13, 0, 13, 0);
            this.panel4.Size = new System.Drawing.Size(935, 400);
            this.panel4.TabIndex = 39;
            // 
            // LineStopDatagrid
            // 
            this.LineStopDatagrid.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.LineStopDatagrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.LineStopDatagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.LineStopDatagrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.LineStopDatagrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.LineStopDatagrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LineStopDatagrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.LineStopDatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LineStopDatagrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.LineStopDatagrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LineStopDatagrid.Location = new System.Drawing.Point(13, 0);
            this.LineStopDatagrid.Name = "LineStopDatagrid";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LineStopDatagrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.LineStopDatagrid.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LineStopDatagrid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.LineStopDatagrid.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LineStopDatagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.LineStopDatagrid.Size = new System.Drawing.Size(909, 400);
            this.LineStopDatagrid.TabIndex = 26;
            this.LineStopDatagrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.LineStopDatagrid_CellFormatting);
            // 
            // CheckLineStopDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(935, 547);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CheckLineStopDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CheckLineStopDataForm_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LineStopDatagrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox LineStopTextBox;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button DeleteLineStopBtn;
        private System.Windows.Forms.DataGridView LineStopDatagrid;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox SectionDropdown;
        private System.Windows.Forms.Button ExportBtn;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ComboBox SectionDropdownList;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.DateTimePicker DateFrom;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.DateTimePicker DateTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label FromLabel;
    }
}