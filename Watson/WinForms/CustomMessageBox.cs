using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watson
{
    public partial class CustomMessageBox : Form
    {
        public int Result = 0;

        public CustomMessageBox(string Title, string Comment, string Yes = "Yes", string No = "No", string Cancel = "Cancel")
        {
            InitializeComponent();
            label1.Text = Comment;
            button1.Text = Yes;
            button2.Text = No;
            button3.Text = Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
