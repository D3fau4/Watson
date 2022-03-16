using AssetsTools.NET;
using Watson.Lib.IO;
using Watson.Lib.Utils;
using Watson.Lib.Utils.Helpers;

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

            AssetBundleCompressionType compression = AssetBundleCompressionType.NONE;

            if (m_tmpold.m_Assets.IsBundle)
            {
                CustomMessageBox message = new CustomMessageBox("", "You want compress the final bundle?", "With LZ4", "With LZMA", "No");
                var result = message.ShowDialog();
                if (result == DialogResult.Yes)
                    compression = AssetBundleCompressionType.LZ4;
                else if (result == DialogResult.No)
                    compression = AssetBundleCompressionType.LZMA;
                else if (result == DialogResult.Cancel)
                    compression = AssetBundleCompressionType.NONE;
            }

            var asset = TMPFont_Importer.Import(m_tmpnew.m_FontNames, m_tmpold.m_FontNames, m_tmpnew.m_FontTextures, m_tmpold.m_FontTextures);
            AssetHelper.Save(m_tmpold.m_Assets, asset, compression);

            AssetHelper.Close(m_tmpold.m_Assets);
            AssetHelper.Close(m_tmpnew.m_Assets);

            MessageBox.Show("Done!");
        }
    }
}
