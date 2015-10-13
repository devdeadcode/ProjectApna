namespace FilterControlTest
{
    partial class Form1
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
            this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.filterElementControl1 = new FilterControl.FilterElementControl();
            this.radTextBoxControl1 = new Telerik.WinControls.UI.RadTextBoxControl();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.filterElementControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.radTextBoxControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(858, 426);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // filterElementControl1
            // 
            this.filterElementControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterElementControl1.Location = new System.Drawing.Point(3, 3);
            this.filterElementControl1.Name = "filterElementControl1";
            this.filterElementControl1.ShowConjunctive = true;
            this.filterElementControl1.Size = new System.Drawing.Size(852, 30);
            this.filterElementControl1.TabIndex = 0;
            this.filterElementControl1.ElementChanged += new System.EventHandler(this.filterElementControl1_ElementChanged);
            // 
            // radTextBoxControl1
            // 
            this.radTextBoxControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radTextBoxControl1.Location = new System.Drawing.Point(3, 39);
            this.radTextBoxControl1.Multiline = true;
            this.radTextBoxControl1.Name = "radTextBoxControl1";
            this.radTextBoxControl1.Size = new System.Drawing.Size(852, 384);
            this.radTextBoxControl1.TabIndex = 1;
            this.radTextBoxControl1.ThemeName = "TelerikMetroTouch";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(858, 426);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FilterControl.FilterElementControl filterElementControl1;
        private Telerik.WinControls.Themes.TelerikMetroTouchTheme telerikMetroTouchTheme1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Telerik.WinControls.UI.RadTextBoxControl radTextBoxControl1;
    }
}

