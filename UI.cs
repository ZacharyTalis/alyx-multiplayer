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
            if (path.Length <= 0 || path.Equals(Core.scriptPath))
            {
                // do nothing
            } else
            {
                Core.scriptPath = @path;
                Core.Log("Script path set to \"" + path + "\"", false);
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
            Core.entPrefixIndex = 1;
        }
    }
}
