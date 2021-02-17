
namespace alyx_multiplayer
{
    partial class UI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxLog = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelLog = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelOptions = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tempToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelVersionNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toggleConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.panel3);
            this.splitContainer.Panel1.Controls.Add(this.panel1);
            this.splitContainer.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panel4);
            this.splitContainer.Panel2.Controls.Add(this.panel2);
            this.splitContainer.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer.Size = new System.Drawing.Size(800, 426);
            this.splitContainer.SplitterDistance = 265;
            this.splitContainer.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBoxLog);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 13);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(265, 413);
            this.panel3.TabIndex = 3;
            // 
            // textBoxLog
            // 
            this.textBoxLog.BackColor = System.Drawing.Color.Black;
            this.textBoxLog.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.ForeColor = System.Drawing.Color.White;
            this.textBoxLog.Location = new System.Drawing.Point(0, 0);
            this.textBoxLog.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.textBoxLog.Size = new System.Drawing.Size(265, 413);
            this.textBoxLog.TabIndex = 1;
            this.textBoxLog.Text = "";
            this.textBoxLog.TextChanged += new System.EventHandler(this.logTextBox_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelLog);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(265, 13);
            this.panel1.TabIndex = 2;
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.Location = new System.Drawing.Point(6, 0);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(25, 13);
            this.labelLog.TabIndex = 0;
            this.labelLog.Text = "Log";
            this.labelLog.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelOptions);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(531, 13);
            this.panel2.TabIndex = 3;
            // 
            // labelOptions
            // 
            this.labelOptions.AutoSize = true;
            this.labelOptions.Location = new System.Drawing.Point(4, 0);
            this.labelOptions.Name = "labelOptions";
            this.labelOptions.Size = new System.Drawing.Size(43, 13);
            this.labelOptions.TabIndex = 1;
            this.labelOptions.Text = "Options";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 18);
            this.button1.MaximumSize = new System.Drawing.Size(75, 23);
            this.button1.MinimumSize = new System.Drawing.Size(75, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "I do nothing!";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tempToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip";
            this.menuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // tempToolStripMenuItem
            // 
            this.tempToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleConsoleToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.tempToolStripMenuItem.Name = "tempToolStripMenuItem";
            this.tempToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.tempToolStripMenuItem.Text = "File";
            this.tempToolStripMenuItem.Click += new System.EventHandler(this.tempToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelVersionNumber});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelVersionNumber
            // 
            this.labelVersionNumber.Name = "labelVersionNumber";
            this.labelVersionNumber.Size = new System.Drawing.Size(28, 17);
            this.labelVersionNumber.Text = "v1.0";
            this.labelVersionNumber.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // toggleConsoleToolStripMenuItem
            // 
            this.toggleConsoleToolStripMenuItem.Name = "toggleConsoleToolStripMenuItem";
            this.toggleConsoleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.toggleConsoleToolStripMenuItem.Text = "Show/Hide Console";
            this.toggleConsoleToolStripMenuItem.Click += new System.EventHandler(this.toggleConsoleToolStripMenuItem_Click);
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.Controls.Add(this.button1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 13);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(531, 413);
            this.panel4.TabIndex = 4;
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "UI";
            this.Text = "alyx-multiplayer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UI_FormClosed);
            this.Load += new System.EventHandler(this.form_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem tempToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelVersionNumber;
        private System.Windows.Forms.RichTextBox textBoxLog;
        private System.Windows.Forms.Label labelOptions;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStripMenuItem toggleConsoleToolStripMenuItem;
        private System.Windows.Forms.Panel panel4;
    }
}