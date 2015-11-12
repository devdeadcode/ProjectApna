namespace eOne.Common.Experiments
{
    partial class CompareRecords
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cboConnectorFrom = new System.Windows.Forms.ComboBox();
            this.cboListFrom = new System.Windows.Forms.ComboBox();
            this.cboConnectorTo = new System.Windows.Forms.ComboBox();
            this.cboListTo = new System.Windows.Forms.ComboBox();
            this.cboFieldFrom = new System.Windows.Forms.ComboBox();
            this.cboFieldTo = new System.Windows.Forms.ComboBox();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.btnCompare = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.listResults = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.cboConnectorFrom, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboListFrom, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cboConnectorTo, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboListTo, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.cboFieldFrom, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cboFieldTo, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.cboType, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnCompare, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.listResults, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(731, 503);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cboConnectorFrom
            // 
            this.cboConnectorFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboConnectorFrom.FormattingEnabled = true;
            this.cboConnectorFrom.Location = new System.Drawing.Point(106, 6);
            this.cboConnectorFrom.Name = "cboConnectorFrom";
            this.cboConnectorFrom.Size = new System.Drawing.Size(246, 21);
            this.cboConnectorFrom.TabIndex = 0;
            this.cboConnectorFrom.SelectedIndexChanged += new System.EventHandler(this.cboConnectorFrom_SelectedIndexChanged);
            // 
            // cboListFrom
            // 
            this.cboListFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboListFrom.FormattingEnabled = true;
            this.cboListFrom.Location = new System.Drawing.Point(106, 30);
            this.cboListFrom.Name = "cboListFrom";
            this.cboListFrom.Size = new System.Drawing.Size(246, 21);
            this.cboListFrom.TabIndex = 1;
            this.cboListFrom.SelectedIndexChanged += new System.EventHandler(this.cboListFrom_SelectedIndexChanged);
            // 
            // cboConnectorTo
            // 
            this.cboConnectorTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboConnectorTo.FormattingEnabled = true;
            this.cboConnectorTo.Location = new System.Drawing.Point(478, 6);
            this.cboConnectorTo.Name = "cboConnectorTo";
            this.cboConnectorTo.Size = new System.Drawing.Size(247, 21);
            this.cboConnectorTo.TabIndex = 2;
            this.cboConnectorTo.SelectedIndexChanged += new System.EventHandler(this.cboConnectorTo_SelectedIndexChanged);
            // 
            // cboListTo
            // 
            this.cboListTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboListTo.FormattingEnabled = true;
            this.cboListTo.Location = new System.Drawing.Point(478, 30);
            this.cboListTo.Name = "cboListTo";
            this.cboListTo.Size = new System.Drawing.Size(247, 21);
            this.cboListTo.TabIndex = 3;
            this.cboListTo.SelectedIndexChanged += new System.EventHandler(this.cboListTo_SelectedIndexChanged);
            // 
            // cboFieldFrom
            // 
            this.cboFieldFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboFieldFrom.FormattingEnabled = true;
            this.cboFieldFrom.Location = new System.Drawing.Point(106, 54);
            this.cboFieldFrom.Name = "cboFieldFrom";
            this.cboFieldFrom.Size = new System.Drawing.Size(246, 21);
            this.cboFieldFrom.TabIndex = 4;
            this.cboFieldFrom.SelectedIndexChanged += new System.EventHandler(this.cboFieldFrom_SelectedIndexChanged);
            // 
            // cboFieldTo
            // 
            this.cboFieldTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboFieldTo.FormattingEnabled = true;
            this.cboFieldTo.Location = new System.Drawing.Point(478, 54);
            this.cboFieldTo.Name = "cboFieldTo";
            this.cboFieldTo.Size = new System.Drawing.Size(247, 21);
            this.cboFieldTo.TabIndex = 5;
            this.cboFieldTo.SelectedIndexChanged += new System.EventHandler(this.cboFieldTo_SelectedIndexChanged);
            // 
            // cboType
            // 
            this.cboType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(106, 94);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(246, 21);
            this.cboType.TabIndex = 6;
            // 
            // btnCompare
            // 
            this.btnCompare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCompare.Location = new System.Drawing.Point(378, 94);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(94, 24);
            this.btnCompare.TabIndex = 7;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(62, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "Connector";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3);
            this.label2.Size = new System.Drawing.Size(29, 19);
            this.label2.TabIndex = 9;
            this.label2.Text = "List";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 51);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3);
            this.label3.Size = new System.Drawing.Size(77, 19);
            this.label3.TabIndex = 10;
            this.label3.Text = "Compare field";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 91);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(3);
            this.label4.Size = new System.Drawing.Size(91, 19);
            this.label4.TabIndex = 11;
            this.label4.Text = "Comparison type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(378, 3);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(3);
            this.label5.Size = new System.Drawing.Size(62, 19);
            this.label5.TabIndex = 12;
            this.label5.Text = "Connector";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(378, 27);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(3);
            this.label6.Size = new System.Drawing.Size(29, 19);
            this.label6.TabIndex = 13;
            this.label6.Text = "List";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(378, 51);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(3);
            this.label7.Size = new System.Drawing.Size(77, 19);
            this.label7.TabIndex = 14;
            this.label7.Text = "Compare field";
            // 
            // listResults
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.listResults, 5);
            this.listResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listResults.Location = new System.Drawing.Point(6, 124);
            this.listResults.Name = "listResults";
            this.listResults.Size = new System.Drawing.Size(719, 373);
            this.listResults.TabIndex = 15;
            this.listResults.UseCompatibleStateImageBehavior = false;
            // 
            // CompareRecords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 503);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CompareRecords";
            this.Text = "Compare records";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cboConnectorFrom;
        private System.Windows.Forms.ComboBox cboListFrom;
        private System.Windows.Forms.ComboBox cboConnectorTo;
        private System.Windows.Forms.ComboBox cboListTo;
        private System.Windows.Forms.ComboBox cboFieldFrom;
        private System.Windows.Forms.ComboBox cboFieldTo;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView listResults;
    }
}

