using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace alyx_multiplayer
{
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }

        private void Info_Load(object sender, EventArgs e)
        {

        }

        private void Info_FormClosed(object sender, FormClosedEventArgs e)
        {
            Core.isInfoOpen = false;
        }
        
        private void buttonSourceCode_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ZacharyTalis/alyx-multiplayer");
        }

        private void buttonWebsite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://alyx-multiplayer.com/");
        }
    }
}
