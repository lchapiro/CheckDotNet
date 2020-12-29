namespace CheckDotNet
{
    partial class MainWnd
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWnd));
            this.grpBoxFound = new System.Windows.Forms.GroupBox();
            this.lstBoxFound = new System.Windows.Forms.ListView();
            this.grpBoxNeed = new System.Windows.Forms.GroupBox();
            this.lstView = new System.Windows.Forms.ListView();
            this.butGetDir = new System.Windows.Forms.Button();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_GetDir = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_Report = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStripInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_Info = new System.Windows.Forms.ToolStripMenuItem();
            this.grpBoxFound.SuspendLayout();
            this.grpBoxNeed.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxFound
            // 
            this.grpBoxFound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpBoxFound.Controls.Add(this.lstBoxFound);
            this.grpBoxFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxFound.Location = new System.Drawing.Point(16, 29);
            this.grpBoxFound.Margin = new System.Windows.Forms.Padding(4);
            this.grpBoxFound.Name = "grpBoxFound";
            this.grpBoxFound.Padding = new System.Windows.Forms.Padding(4);
            this.grpBoxFound.Size = new System.Drawing.Size(394, 310);
            this.grpBoxFound.TabIndex = 1;
            this.grpBoxFound.TabStop = false;
            this.grpBoxFound.Text = "Available .NET Framworks";
            // 
            // lstBoxFound
            // 
            this.lstBoxFound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstBoxFound.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstBoxFound.GridLines = true;
            this.lstBoxFound.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstBoxFound.Location = new System.Drawing.Point(-4, 22);
            this.lstBoxFound.Margin = new System.Windows.Forms.Padding(4);
            this.lstBoxFound.MultiSelect = false;
            this.lstBoxFound.Name = "lstBoxFound";
            this.lstBoxFound.ShowGroups = false;
            this.lstBoxFound.ShowItemToolTips = true;
            this.lstBoxFound.Size = new System.Drawing.Size(377, 273);
            this.lstBoxFound.TabIndex = 0;
            this.lstBoxFound.UseCompatibleStateImageBehavior = false;
            this.lstBoxFound.View = System.Windows.Forms.View.Details;
            // 
            // grpBoxNeed
            // 
            this.grpBoxNeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxNeed.Controls.Add(this.lstView);
            this.grpBoxNeed.Controls.Add(this.butGetDir);
            this.grpBoxNeed.Controls.Add(this.txtDir);
            this.grpBoxNeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxNeed.Location = new System.Drawing.Point(418, 29);
            this.grpBoxNeed.Margin = new System.Windows.Forms.Padding(4);
            this.grpBoxNeed.Name = "grpBoxNeed";
            this.grpBoxNeed.Padding = new System.Windows.Forms.Padding(4);
            this.grpBoxNeed.Size = new System.Drawing.Size(542, 310);
            this.grpBoxNeed.TabIndex = 0;
            this.grpBoxNeed.TabStop = false;
            this.grpBoxNeed.Text = "Linked/Needed .NET Frameworks";
            // 
            // lstView
            // 
            this.lstView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstView.GridLines = true;
            this.lstView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstView.Location = new System.Drawing.Point(16, 69);
            this.lstView.Margin = new System.Windows.Forms.Padding(4);
            this.lstView.MultiSelect = false;
            this.lstView.Name = "lstView";
            this.lstView.ShowGroups = false;
            this.lstView.ShowItemToolTips = true;
            this.lstView.Size = new System.Drawing.Size(509, 226);
            this.lstView.TabIndex = 2;
            this.lstView.UseCompatibleStateImageBehavior = false;
            this.lstView.View = System.Windows.Forms.View.Details;
            // 
            // butGetDir
            // 
            this.butGetDir.Image = global::CheckDotNet.Properties.Resources.folder;
            this.butGetDir.Location = new System.Drawing.Point(482, 23);
            this.butGetDir.Margin = new System.Windows.Forms.Padding(4);
            this.butGetDir.Name = "butGetDir";
            this.butGetDir.Size = new System.Drawing.Size(43, 25);
            this.butGetDir.TabIndex = 0;
            this.butGetDir.UseVisualStyleBackColor = true;
            this.butGetDir.Click += new System.EventHandler(this.butGetDir_Click);
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(16, 26);
            this.txtDir.Margin = new System.Windows.Forms.Padding(4);
            this.txtDir.Name = "txtDir";
            this.txtDir.ReadOnly = true;
            this.txtDir.Size = new System.Drawing.Size(458, 22);
            this.txtDir.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStrip,
            this.mnuStripInfo});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(970, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuStrip
            // 
            this.mnuStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItem_GetDir,
            this.mnuItem_Report,
            this.mnuItem_Close});
            this.mnuStrip.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuStrip.Name = "mnuStrip";
            this.mnuStrip.Size = new System.Drawing.Size(39, 21);
            this.mnuStrip.Text = "&File";
            // 
            // mnuItem_GetDir
            // 
            this.mnuItem_GetDir.Name = "mnuItem_GetDir";
            this.mnuItem_GetDir.Size = new System.Drawing.Size(180, 22);
            this.mnuItem_GetDir.Text = "&Select Directory";
            this.mnuItem_GetDir.Click += new System.EventHandler(this.OnMenuGetDir);
            // 
            // mnuItem_Report
            // 
            this.mnuItem_Report.Enabled = false;
            this.mnuItem_Report.Name = "mnuItem_Report";
            this.mnuItem_Report.Size = new System.Drawing.Size(180, 22);
            this.mnuItem_Report.Text = "Create &Report";
            this.mnuItem_Report.Click += new System.EventHandler(this.OnMenuReport);
            // 
            // mnuItem_Close
            // 
            this.mnuItem_Close.Name = "mnuItem_Close";
            this.mnuItem_Close.Size = new System.Drawing.Size(180, 22);
            this.mnuItem_Close.Text = "&Close";
            this.mnuItem_Close.Click += new System.EventHandler(this.OnMenuClose);
            // 
            // mnuStripInfo
            // 
            this.mnuStripInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItem_Info});
            this.mnuStripInfo.Name = "mnuStripInfo";
            this.mnuStripInfo.Size = new System.Drawing.Size(24, 21);
            this.mnuStripInfo.Text = "&?";
            // 
            // mnuItem_Info
            // 
            this.mnuItem_Info.Name = "mnuItem_Info";
            this.mnuItem_Info.Size = new System.Drawing.Size(180, 22);
            this.mnuItem_Info.Text = "&Info";
            this.mnuItem_Info.Click += new System.EventHandler(this.OnMenuInfo);
            // 
            // MainWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(970, 349);
            this.Controls.Add(this.grpBoxNeed);
            this.Controls.Add(this.grpBoxFound);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWnd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".NET Framwork Checker";
            this.grpBoxFound.ResumeLayout(false);
            this.grpBoxNeed.ResumeLayout(false);
            this.grpBoxNeed.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxFound;
        private System.Windows.Forms.GroupBox grpBoxNeed;
        private System.Windows.Forms.Button butGetDir;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.ListView lstView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_GetDir;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_Report;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_Close;
        private System.Windows.Forms.ToolStripMenuItem mnuStripInfo;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_Info;
        private System.Windows.Forms.ListView lstBoxFound;
    }
}

