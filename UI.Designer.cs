
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripStatusLabel labelHeader;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxLog = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelLog = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPeerPort = new System.Windows.Forms.TextBox();
            this.buttonPeerPort = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPeerIP = new System.Windows.Forms.TextBox();
            this.buttonPeerIP = new System.Windows.Forms.Button();
            this.buttonEntSearch = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.labelPeer = new System.Windows.Forms.Label();
            this.textBoxLocalPort = new System.Windows.Forms.TextBox();
            this.buttonLocalPort = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.labelPath = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonPath = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelOptions = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tempToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.labelVersionNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelIP = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTipIP = new System.Windows.Forms.ToolTip(this.components);
            labelHeader = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeader
            // 
            labelHeader.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            labelHeader.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            labelHeader.Name = "labelHeader";
            labelHeader.Size = new System.Drawing.Size(30, 19);
            labelHeader.Text = "  IP:";
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.panel3);
            this.splitContainer.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panel4);
            this.splitContainer.Panel2.Controls.Add(this.panel2);
            this.splitContainer.Size = new System.Drawing.Size(800, 400);
            this.splitContainer.SplitterDistance = 265;
            this.splitContainer.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBoxLog);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 13);
            this.panel3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(265, 387);
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
            this.textBoxLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(265, 387);
            this.textBoxLog.TabIndex = 1;
            this.textBoxLog.Text = "";
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
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.buttonEntSearch);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 13);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(531, 387);
            this.panel4.TabIndex = 4;
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.Controls.Add(this.label2);
            this.panel8.Controls.Add(this.textBoxPeerPort);
            this.panel8.Controls.Add(this.buttonPeerPort);
            this.panel8.Location = new System.Drawing.Point(0, 88);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(531, 26);
            this.panel8.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Peer port:";
            // 
            // textBoxPeerPort
            // 
            this.textBoxPeerPort.Location = new System.Drawing.Point(108, 3);
            this.textBoxPeerPort.Name = "textBoxPeerPort";
            this.textBoxPeerPort.Size = new System.Drawing.Size(295, 20);
            this.textBoxPeerPort.TabIndex = 2;
            this.textBoxPeerPort.Text = "6655";
            this.textBoxPeerPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPeerPort_KeyPress);
            // 
            // buttonPeerPort
            // 
            this.buttonPeerPort.Location = new System.Drawing.Point(409, 1);
            this.buttonPeerPort.MaximumSize = new System.Drawing.Size(75, 23);
            this.buttonPeerPort.MinimumSize = new System.Drawing.Size(75, 23);
            this.buttonPeerPort.Name = "buttonPeerPort";
            this.buttonPeerPort.Size = new System.Drawing.Size(75, 23);
            this.buttonPeerPort.TabIndex = 3;
            this.buttonPeerPort.Text = "Submit";
            this.buttonPeerPort.UseVisualStyleBackColor = true;
            this.buttonPeerPort.Click += new System.EventHandler(this.buttonPeerPort_Click);
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.Controls.Add(this.label1);
            this.panel7.Controls.Add(this.textBoxPeerIP);
            this.panel7.Controls.Add(this.buttonPeerIP);
            this.panel7.Location = new System.Drawing.Point(0, 61);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(531, 26);
            this.panel7.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Peer IP:";
            // 
            // textBoxPeerIP
            // 
            this.textBoxPeerIP.Location = new System.Drawing.Point(108, 3);
            this.textBoxPeerIP.Name = "textBoxPeerIP";
            this.textBoxPeerIP.Size = new System.Drawing.Size(295, 20);
            this.textBoxPeerIP.TabIndex = 2;
            this.textBoxPeerIP.Text = "127.0.0.1";
            this.textBoxPeerIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPeerIP_KeyPress);
            // 
            // buttonPeerIP
            // 
            this.buttonPeerIP.Location = new System.Drawing.Point(409, 1);
            this.buttonPeerIP.MaximumSize = new System.Drawing.Size(75, 23);
            this.buttonPeerIP.MinimumSize = new System.Drawing.Size(75, 23);
            this.buttonPeerIP.Name = "buttonPeerIP";
            this.buttonPeerIP.Size = new System.Drawing.Size(75, 23);
            this.buttonPeerIP.TabIndex = 3;
            this.buttonPeerIP.Text = "Submit";
            this.buttonPeerIP.UseVisualStyleBackColor = true;
            this.buttonPeerIP.Click += new System.EventHandler(this.buttonPeerIP_Click);
            // 
            // buttonEntSearch
            // 
            this.buttonEntSearch.Location = new System.Drawing.Point(374, 118);
            this.buttonEntSearch.MaximumSize = new System.Drawing.Size(110, 23);
            this.buttonEntSearch.MinimumSize = new System.Drawing.Size(110, 23);
            this.buttonEntSearch.Name = "buttonEntSearch";
            this.buttonEntSearch.Size = new System.Drawing.Size(110, 23);
            this.buttonEntSearch.TabIndex = 4;
            this.buttonEntSearch.Text = "Restart Ent Search";
            this.buttonEntSearch.UseVisualStyleBackColor = true;
            this.buttonEntSearch.Click += new System.EventHandler(this.buttonEntSearch_Click);
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.Controls.Add(this.labelPeer);
            this.panel6.Controls.Add(this.textBoxLocalPort);
            this.panel6.Controls.Add(this.buttonLocalPort);
            this.panel6.Location = new System.Drawing.Point(0, 34);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(531, 26);
            this.panel6.TabIndex = 3;
            // 
            // labelPeer
            // 
            this.labelPeer.AutoSize = true;
            this.labelPeer.Location = new System.Drawing.Point(2, 6);
            this.labelPeer.Name = "labelPeer";
            this.labelPeer.Size = new System.Drawing.Size(57, 13);
            this.labelPeer.TabIndex = 2;
            this.labelPeer.Text = "Local port:";
            // 
            // textBoxLocalPort
            // 
            this.textBoxLocalPort.Location = new System.Drawing.Point(108, 3);
            this.textBoxLocalPort.Name = "textBoxLocalPort";
            this.textBoxLocalPort.Size = new System.Drawing.Size(295, 20);
            this.textBoxLocalPort.TabIndex = 2;
            this.textBoxLocalPort.Text = "6655";
            this.textBoxLocalPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxLocalPort_KeyPress);
            // 
            // buttonLocalPort
            // 
            this.buttonLocalPort.Location = new System.Drawing.Point(409, 1);
            this.buttonLocalPort.MaximumSize = new System.Drawing.Size(75, 23);
            this.buttonLocalPort.MinimumSize = new System.Drawing.Size(75, 23);
            this.buttonLocalPort.Name = "buttonLocalPort";
            this.buttonLocalPort.Size = new System.Drawing.Size(75, 23);
            this.buttonLocalPort.TabIndex = 3;
            this.buttonLocalPort.Text = "Submit";
            this.buttonLocalPort.UseVisualStyleBackColor = true;
            this.buttonLocalPort.Click += new System.EventHandler(this.buttonLocalPort_Click);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.labelPath);
            this.panel5.Controls.Add(this.textBoxPath);
            this.panel5.Controls.Add(this.buttonPath);
            this.panel5.Location = new System.Drawing.Point(0, 7);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(531, 26);
            this.panel5.TabIndex = 1;
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(2, 6);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(103, 13);
            this.labelPath.TabIndex = 2;
            this.labelPath.Text = "Folder path of script:";
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(108, 3);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(295, 20);
            this.textBoxPath.TabIndex = 0;
            this.textBoxPath.Text = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Half-Life Alyx\\game\\hlvr_addons\\aly" +
    "x_multiplayer\\scripts\\vscripts";
            this.textBoxPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPath_KeyPress);
            // 
            // buttonPath
            // 
            this.buttonPath.Location = new System.Drawing.Point(409, 1);
            this.buttonPath.MaximumSize = new System.Drawing.Size(75, 23);
            this.buttonPath.MinimumSize = new System.Drawing.Size(75, 23);
            this.buttonPath.Name = "buttonPath";
            this.buttonPath.Size = new System.Drawing.Size(75, 23);
            this.buttonPath.TabIndex = 1;
            this.buttonPath.Text = "Submit";
            this.buttonPath.UseVisualStyleBackColor = true;
            this.buttonPath.Click += new System.EventHandler(this.buttonPath_Click);
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
            this.labelOptions.Location = new System.Drawing.Point(2, 0);
            this.labelOptions.Name = "labelOptions";
            this.labelOptions.Size = new System.Drawing.Size(43, 13);
            this.labelOptions.TabIndex = 1;
            this.labelOptions.Text = "Options";
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
            // 
            // tempToolStripMenuItem
            // 
            this.tempToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemConsole,
            this.toolStripMenuItemInfo});
            this.tempToolStripMenuItem.Name = "tempToolStripMenuItem";
            this.tempToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.tempToolStripMenuItem.Text = "File";
            // 
            // toolStripMenuItemConsole
            // 
            this.toolStripMenuItemConsole.Name = "toolStripMenuItemConsole";
            this.toolStripMenuItemConsole.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItemConsole.Text = "Show/Hide Console";
            this.toolStripMenuItemConsole.Click += new System.EventHandler(this.toolStripMenuItemConsole_Click);
            // 
            // toolStripMenuItemInfo
            // 
            this.toolStripMenuItemInfo.Name = "toolStripMenuItemInfo";
            this.toolStripMenuItemInfo.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItemInfo.Text = "Info";
            this.toolStripMenuItemInfo.Click += new System.EventHandler(this.toolStripMenuItemInfo_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelVersionNumber,
            labelHeader,
            this.labelIP});
            this.statusStrip.Location = new System.Drawing.Point(0, 426);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 24);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip";
            // 
            // labelVersionNumber
            // 
            this.labelVersionNumber.Margin = new System.Windows.Forms.Padding(0, 3, 3, 2);
            this.labelVersionNumber.Name = "labelVersionNumber";
            this.labelVersionNumber.Size = new System.Drawing.Size(28, 19);
            this.labelVersionNumber.Text = "v1.0";
            // 
            // labelIP
            // 
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(73, 19);
            this.labelIP.Text = "Not fetched!";
            this.labelIP.Click += new System.EventHandler(this.labelIP_Click);
            this.labelIP.MouseHover += new System.EventHandler(this.labelIP_MouseHover);
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip);
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
            this.panel4.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.Button buttonPath;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem tempToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemInfo;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel labelVersionNumber;
        private System.Windows.Forms.RichTextBox textBoxLog;
        private System.Windows.Forms.Label labelOptions;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemConsole;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label labelPeer;
        private System.Windows.Forms.TextBox textBoxLocalPort;
        private System.Windows.Forms.Button buttonLocalPort;
        private System.Windows.Forms.ToolStripStatusLabel labelIP;
        private System.Windows.Forms.ToolTip toolTipIP;
        private System.Windows.Forms.Button buttonEntSearch;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPeerPort;
        private System.Windows.Forms.Button buttonPeerPort;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPeerIP;
        private System.Windows.Forms.Button buttonPeerIP;
    }
}