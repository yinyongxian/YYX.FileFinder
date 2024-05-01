namespace YYX.FileFinder
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.AutoRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBoxContent = new System.Windows.Forms.RichTextBox();
            this.richTextBoxSites = new System.Windows.Forms.RichTextBox();
            this.toolStripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStripMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(800, 23);
            this.toolStripMenu.TabIndex = 0;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AutoRunToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::YYX.FileFinder.Properties.Resources.settings;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 20);
            // 
            // AutoRunToolStripMenuItem
            // 
            this.AutoRunToolStripMenuItem.Name = "AutoRunToolStripMenuItem";
            this.AutoRunToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.AutoRunToolStripMenuItem.Text = "开机启动";
            this.AutoRunToolStripMenuItem.Click += new System.EventHandler(this.AutoRunToolStripMenuItem_Click);
            // 
            // richTextBoxContent
            // 
            this.richTextBoxContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxContent.Location = new System.Drawing.Point(0, 111);
            this.richTextBoxContent.Name = "richTextBoxContent";
            this.richTextBoxContent.Size = new System.Drawing.Size(800, 377);
            this.richTextBoxContent.TabIndex = 2;
            this.richTextBoxContent.Text = "";
            this.richTextBoxContent.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxContent_LinkClicked);
            // 
            // richTextBoxSites
            // 
            this.richTextBoxSites.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxSites.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBoxSites.Location = new System.Drawing.Point(0, 23);
            this.richTextBoxSites.Name = "richTextBoxSites";
            this.richTextBoxSites.Size = new System.Drawing.Size(800, 88);
            this.richTextBoxSites.TabIndex = 1;
            this.richTextBoxSites.Text = "";
            this.richTextBoxSites.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxSites_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 488);
            this.Controls.Add(this.richTextBoxContent);
            this.Controls.Add(this.richTextBoxSites);
            this.Controls.Add(this.toolStripMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Finder";
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.RichTextBox richTextBoxContent;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem AutoRunToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBoxSites;
    }
}

