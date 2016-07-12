using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneiFurius
{
    public partial class ConsoleForm : Form
    {

        private int countMessages = 0;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        public ConsoleForm()
        {
            InitializeComponent();
        }

        public void PrintMessageConsole(string message)
        {
            if (countMessages > 10)
            {
                ConsoleTextBox.Clear();
                ConsoleTextBox.Text = "--------- GeneiAO ---------";
                countMessages = 0;
            }  
            if (!string.IsNullOrEmpty(message))
            {
                ConsoleTextBox.AppendText(Environment.NewLine);
                ConsoleTextBox.AppendText(message.Trim());
                countMessages++;                    
            }
        }

        private void ConsoleForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                BasicForm.ReleaseCapture();
                BasicForm.SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ConsoleForm_Load(object sender, EventArgs e)
        {
            ConsoleTextBox.Clear();
            ConsoleTextBox.ResetText();
            ConsoleTextBox.Text = "--------- GeneiAO ---------";
            ConsoleTextBox.ForeColor = Color.HotPink;
            if (PortListener.isConnected)
                PrintMessageConsole("GeneiAO> The DLL is Connected!.");
        }
    }
}
