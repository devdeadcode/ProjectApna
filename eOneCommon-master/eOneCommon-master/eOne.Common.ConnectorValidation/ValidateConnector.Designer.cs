namespace eOne.Common.ConnectorValidation
{
    partial class ValidateConnector
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValidateConnector));
            this.cboConnector = new System.Windows.Forms.ComboBox();
            this.lblConnector = new System.Windows.Forms.Label();
            this.txtErrors = new System.Windows.Forms.TextBox();
            this.btnValidate = new System.Windows.Forms.Button();
            this.treeEntities = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txtWarnings = new System.Windows.Forms.TextBox();
            this.lblErrors = new System.Windows.Forms.Label();
            this.lblWarnings = new System.Windows.Forms.Label();
            this.lblEntities = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboConnector
            // 
            this.cboConnector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboConnector.FormattingEnabled = true;
            this.cboConnector.Location = new System.Drawing.Point(73, 3);
            this.cboConnector.Name = "cboConnector";
            this.cboConnector.Size = new System.Drawing.Size(222, 21);
            this.cboConnector.TabIndex = 0;
            this.cboConnector.SelectedIndexChanged += new System.EventHandler(this.cboConnector_SelectedIndexChanged);
            // 
            // lblConnector
            // 
            this.lblConnector.AutoSize = true;
            this.lblConnector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblConnector.Location = new System.Drawing.Point(3, 0);
            this.lblConnector.Name = "lblConnector";
            this.lblConnector.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblConnector.Size = new System.Drawing.Size(64, 30);
            this.lblConnector.TabIndex = 1;
            this.lblConnector.Text = "Connector:";
            // 
            // txtErrors
            // 
            this.txtErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtErrors.Location = new System.Drawing.Point(301, 57);
            this.txtErrors.Multiline = true;
            this.txtErrors.Name = "txtErrors";
            this.txtErrors.Size = new System.Drawing.Size(529, 187);
            this.txtErrors.TabIndex = 2;
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(301, 3);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(75, 23);
            this.btnValidate.TabIndex = 3;
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // treeEntities
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.treeEntities, 2);
            this.treeEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeEntities.ImageIndex = 0;
            this.treeEntities.ImageList = this.imageList1;
            this.treeEntities.Location = new System.Drawing.Point(3, 57);
            this.treeEntities.Name = "treeEntities";
            this.tableLayoutPanel1.SetRowSpan(this.treeEntities, 3);
            this.treeEntities.SelectedImageIndex = 0;
            this.treeEntities.Size = new System.Drawing.Size(292, 404);
            this.treeEntities.TabIndex = 4;
            this.treeEntities.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeEntities_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ok_button.png");
            this.imageList1.Images.SetKeyName(1, "warning.png");
            this.imageList1.Images.SetKeyName(2, "stop.png");
            // 
            // txtWarnings
            // 
            this.txtWarnings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWarnings.Location = new System.Drawing.Point(301, 274);
            this.txtWarnings.Multiline = true;
            this.txtWarnings.Name = "txtWarnings";
            this.txtWarnings.Size = new System.Drawing.Size(529, 187);
            this.txtWarnings.TabIndex = 5;
            // 
            // lblErrors
            // 
            this.lblErrors.AutoSize = true;
            this.lblErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblErrors.Location = new System.Drawing.Point(301, 30);
            this.lblErrors.Name = "lblErrors";
            this.lblErrors.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblErrors.Size = new System.Drawing.Size(529, 24);
            this.lblErrors.TabIndex = 6;
            this.lblErrors.Text = "Errors:";
            // 
            // lblWarnings
            // 
            this.lblWarnings.AutoSize = true;
            this.lblWarnings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWarnings.Location = new System.Drawing.Point(301, 247);
            this.lblWarnings.Name = "lblWarnings";
            this.lblWarnings.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblWarnings.Size = new System.Drawing.Size(529, 24);
            this.lblWarnings.TabIndex = 7;
            this.lblWarnings.Text = "Warnings:";
            // 
            // lblEntities
            // 
            this.lblEntities.AutoSize = true;
            this.lblEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEntities.Location = new System.Drawing.Point(3, 30);
            this.lblEntities.Name = "lblEntities";
            this.lblEntities.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblEntities.Size = new System.Drawing.Size(64, 24);
            this.lblEntities.TabIndex = 8;
            this.lblEntities.Text = "Entities:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.lblConnector, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblEntities, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cboConnector, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnValidate, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeEntities, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtErrors, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtWarnings, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblWarnings, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblErrors, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(833, 464);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // ValidateConnector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 464);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ValidateConnector";
            this.Text = "Validate ";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboConnector;
        private System.Windows.Forms.Label lblConnector;
        private System.Windows.Forms.TextBox txtErrors;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.TreeView treeEntities;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblEntities;
        private System.Windows.Forms.TextBox txtWarnings;
        private System.Windows.Forms.Label lblWarnings;
        private System.Windows.Forms.Label lblErrors;
        private System.Windows.Forms.ImageList imageList1;
    }
}

