namespace eOne.Common.Experiments
{
    partial class GetSummary
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboConnector = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.treeGroupFields = new System.Windows.Forms.TreeView();
            this.treeSummaryFields = new System.Windows.Forms.TreeView();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRawCount = new System.Windows.Forms.TextBox();
            this.txtSummaryCount = new System.Windows.Forms.TextBox();
            this.rawData = new System.Windows.Forms.DataGridView();
            this.summaryData = new System.Windows.Forms.DataGridView();
            this.cboEntity = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rawData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.summaryData)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cboConnector, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.treeGroupFields, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.treeSummaryFields, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.button2, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtRawCount, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtSummaryCount, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.rawData, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.summaryData, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.cboEntity, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(681, 525);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(64, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connector:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3);
            this.label2.Size = new System.Drawing.Size(64, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Group by:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label3, 2);
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 292);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3);
            this.label3.Size = new System.Drawing.Size(264, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Summary fields:";
            // 
            // cboConnector
            // 
            this.cboConnector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboConnector.FormattingEnabled = true;
            this.cboConnector.Location = new System.Drawing.Point(73, 3);
            this.cboConnector.Name = "cboConnector";
            this.cboConnector.Size = new System.Drawing.Size(194, 21);
            this.cboConnector.TabIndex = 3;
            this.cboConnector.SelectedIndexChanged += new System.EventHandler(this.cboConnector_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(273, 60);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(3);
            this.label4.Size = new System.Drawing.Size(265, 24);
            this.label4.TabIndex = 4;
            this.label4.Text = "Raw data:";
            // 
            // treeGroupFields
            // 
            this.treeGroupFields.CheckBoxes = true;
            this.tableLayoutPanel1.SetColumnSpan(this.treeGroupFields, 2);
            this.treeGroupFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeGroupFields.Location = new System.Drawing.Point(3, 87);
            this.treeGroupFields.Name = "treeGroupFields";
            this.treeGroupFields.Size = new System.Drawing.Size(264, 202);
            this.treeGroupFields.TabIndex = 5;
            // 
            // treeSummaryFields
            // 
            this.treeSummaryFields.CheckBoxes = true;
            this.tableLayoutPanel1.SetColumnSpan(this.treeSummaryFields, 2);
            this.treeSummaryFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeSummaryFields.Location = new System.Drawing.Point(3, 319);
            this.treeSummaryFields.Name = "treeSummaryFields";
            this.treeSummaryFields.Size = new System.Drawing.Size(264, 203);
            this.treeSummaryFields.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(614, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "get data";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(544, 60);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(3);
            this.label5.Size = new System.Drawing.Size(64, 24);
            this.label5.TabIndex = 9;
            this.label5.Text = "Count:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(544, 292);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(3);
            this.label6.Size = new System.Drawing.Size(64, 24);
            this.label6.TabIndex = 10;
            this.label6.Text = "Count:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(273, 292);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(3);
            this.label7.Size = new System.Drawing.Size(265, 24);
            this.label7.TabIndex = 11;
            this.label7.Text = "Summary data:";
            // 
            // txtRawCount
            // 
            this.txtRawCount.Location = new System.Drawing.Point(614, 63);
            this.txtRawCount.Name = "txtRawCount";
            this.txtRawCount.Size = new System.Drawing.Size(64, 20);
            this.txtRawCount.TabIndex = 12;
            // 
            // txtSummaryCount
            // 
            this.txtSummaryCount.Location = new System.Drawing.Point(614, 295);
            this.txtSummaryCount.Name = "txtSummaryCount";
            this.txtSummaryCount.Size = new System.Drawing.Size(64, 20);
            this.txtSummaryCount.TabIndex = 13;
            // 
            // rawData
            // 
            this.rawData.AllowUserToAddRows = false;
            this.rawData.AllowUserToDeleteRows = false;
            this.rawData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.rawData, 3);
            this.rawData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rawData.Location = new System.Drawing.Point(273, 87);
            this.rawData.Name = "rawData";
            this.rawData.ReadOnly = true;
            this.rawData.Size = new System.Drawing.Size(405, 202);
            this.rawData.TabIndex = 14;
            // 
            // summaryData
            // 
            this.summaryData.AllowUserToAddRows = false;
            this.summaryData.AllowUserToDeleteRows = false;
            this.summaryData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.summaryData, 3);
            this.summaryData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryData.Location = new System.Drawing.Point(273, 319);
            this.summaryData.Name = "summaryData";
            this.summaryData.ReadOnly = true;
            this.summaryData.Size = new System.Drawing.Size(405, 203);
            this.summaryData.TabIndex = 15;
            // 
            // cboEntity
            // 
            this.cboEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboEntity.FormattingEnabled = true;
            this.cboEntity.Location = new System.Drawing.Point(73, 33);
            this.cboEntity.Name = "cboEntity";
            this.cboEntity.Size = new System.Drawing.Size(194, 21);
            this.cboEntity.TabIndex = 16;
            this.cboEntity.SelectedIndexChanged += new System.EventHandler(this.cboEntity_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 30);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(3);
            this.label8.Size = new System.Drawing.Size(64, 30);
            this.label8.TabIndex = 17;
            this.label8.Text = "Entity:";
            // 
            // GetSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 525);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GetSummary";
            this.Text = "GetSummary";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rawData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.summaryData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboConnector;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TreeView treeGroupFields;
        private System.Windows.Forms.TreeView treeSummaryFields;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRawCount;
        private System.Windows.Forms.TextBox txtSummaryCount;
        private System.Windows.Forms.DataGridView rawData;
        private System.Windows.Forms.DataGridView summaryData;
        private System.Windows.Forms.ComboBox cboEntity;
        private System.Windows.Forms.Label label8;
    }
}