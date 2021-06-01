using System;
using System.Threading;
using System.Windows.Forms;

namespace alyx_multiplayer
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Executed when the UI form loads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Terminate all threads when form closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        /// <summary>
        /// Copy IP when labelIP is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelIP_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(labelIP.Text);
        }

        /// <summary>
        /// Toggle console visibility.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemConsole_Click(object sender, EventArgs e)
        {
            Core.ToggleConsole();
        }

        /// <summary>
        /// Open the Info form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemInfo_Click(object sender, EventArgs e)
        {
            if (!Core.isInfoOpen)
            {
                Core.isInfoOpen = true;
                Thread infoThread = new Thread(Core.InfoThread.ShowInfo);
                infoThread.Start();
            }
        }

        /// <summary>
        /// Tooltip functionality for labelIP.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelIP_MouseHover(object sender, EventArgs e)
        {
            toolTipIP.Show("Click to copy IP!", labelIP.Owner);
        }

        /// <summary>
        /// Submit the script path (by pressing either buttonPath or the return key).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPath_Click(object sender, EventArgs e)
        {
            String path = textBoxPath.Text;
            if (path.Length <= 0)
            {
                // do nothing
            } else
            {
                Core.scriptPath = @path;
                Core.Log("Set script path set to: \"" + path + "\"", false);
            }
        }

        /// <summary>
        /// Call buttonPath_Click() when return key pressed inside textBoxPath.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                buttonPath_Click(sender, e);
                e.Handled = true;
            }
        }

        private void buttonEntSearch_Click(object sender, EventArgs e)
        {
            Core.entPrefixIndex = 0;
        }

        private void buttonLocalPort_Click(object sender, EventArgs e)
        {
            string newServerPort = textBoxLocalPort.Text;
            if (newServerPort.Length <= 0)
            {
                // do nothing
            }
            else
            {
                Core.Log("Set local port to: " + newServerPort, false);
                Core.networkHandler.ReconfigureServerPort(newServerPort);
            }
        }

        private void textBoxLocalPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                buttonLocalPort_Click(sender, e);
                e.Handled = true;
            }
        }

        private void buttonPeerIP_Click(object sender, EventArgs e)
        {
            string newClientIP = textBoxPeerIP.Text;
            if (newClientIP.Length <= 0)
            {
                // do nothing
            }
            else
            {
                Core.Log("Set client IP to: " + newClientIP, false);
                Core.networkHandler.ReconfigureClientIP(newClientIP);
            }
        }

        private void textBoxPeerIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                buttonPeerIP_Click(sender, e);
                e.Handled = true;
            }
        }

        private void buttonPeerPort_Click(object sender, EventArgs e)
        {
            string newClientPort = textBoxPeerPort.Text;
            if (newClientPort.Length <= 0)
            {
                // do nothing
            }
            else
            {
                Core.Log("Set client port to: " + newClientPort, false);
                Core.networkHandler.ReconfigureClientPort(newClientPort);
            }
        }

        private void textBoxPeerPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                buttonPeerPort_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
