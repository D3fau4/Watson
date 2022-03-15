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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_ImportTMPfont_Click(object sender, EventArgs e)
        {
            TMPFontImportWindow tMPFontImportWindow = new TMPFontImportWindow();
            tMPFontImportWindow.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SpriteImportWindow tSpriteImportWindow = new SpriteImportWindow();
            tSpriteImportWindow.ShowDialog();
        }
    }
}
