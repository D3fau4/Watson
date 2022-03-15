using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Watson.Lib.IO;
using Watson.Lib.Utils;

namespace Watson {
    public partial class TMPFontImportWindow : Form
    {
        public TMPFontImportWindow()
        {
            InitializeComponent();
        }

        private void OpenOldFilebutton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OldAssettextBox.Text = openFileDialog1.FileName;
            }
        }

        private void OpenNewFilebutton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                NewAssettextBox.Text = openFileDialog1.FileName;
            }
        }

        private void OpenOldDatabutton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OldDataFoldertextBox.Text = openFileDialog1.FileName;
            }
        }

        private void OpenNewDatabutton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                NewDataFoldertextBox.Text = openFileDialog1.FileName;
            }
        }

        private void Submitbutton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NewAssettextBox.Text) || string.IsNullOrEmpty(OldAssettextBox.Text) || 
                string.IsNullOrEmpty(NewDataFoldertextBox.Text) || string.IsNullOrEmpty(OldDataFoldertextBox.Text))
            {
                MessageBox.Show("Porfavor rellene todos los campos");
                return;
            }

            if (!File.Exists(NewAssettextBox.Text) || !File.Exists(OldAssettextBox.Text))
            {
                MessageBox.Show("Porfavor establece un asset.");
                return;
            }

            if (!Directory.Exists(NewDataFoldertextBox.Text) || !Directory.Exists(OldDataFoldertextBox.Text))
            {
                MessageBox.Show("Porfavor estable la carpeta Data");
                return;
            }

            TMPFont m_tmpold = new TMPFont(OldAssettextBox.Text, new Assembly(OldDataFoldertextBox.Text));
            TMPFont m_tmpnew = new TMPFont(NewAssettextBox.Text, new Assembly(NewDataFoldertextBox.Text));

            var asset = TMPFont_Importer.Import(m_tmpnew.m_FontNames, m_tmpold.m_FontNames);

            TMPFont_Importer.Save(m_tmpold.m_Assets, asset);

            MessageBox.Show("Done!");
        }
    }
}
