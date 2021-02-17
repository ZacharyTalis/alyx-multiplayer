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

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Info_FormClosed(object sender, FormClosedEventArgs e)
        {
            Core.isInfoOpen = false;
        }
    }
}
