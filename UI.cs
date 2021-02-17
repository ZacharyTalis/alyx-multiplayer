using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace alyx_multiplayer
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();
        }

        private void form_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tempToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void logTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void toggleConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Core.ToggleConsole();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Core.isInfoOpen)
            {
                Core.isInfoOpen = true;
                Thread infoThread = new Thread(Core.InfoThread.ShowInfo);
                infoThread.Start();
            }
        }

        private void UI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}
