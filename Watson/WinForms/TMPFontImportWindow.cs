using AssetsTools.NET;
using Watson.Lib.Assets;
using Watson.Lib.IO;
using Watson.Lib.Utils;
using Watson.Lib.Utils.Helpers;

namespace Watson;

public partial class TMPFontImportWindow : Form
{
    public TMPFontImportWindow()
    {
        InitializeComponent();
    }

    private void OpenOldFilebutton_Click(object sender, EventArgs e)
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK) OldAssettextBox.Text = openFileDialog1.FileName;
    }

    private void OpenNewFilebutton_Click(object sender, EventArgs e)
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK) NewAssettextBox.Text = openFileDialog1.FileName;
    }

    private void OpenOldDatabutton_Click(object sender, EventArgs e)
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK) OldDataFoldertextBox.Text = openFileDialog1.FileName;
    }

    private void OpenNewDatabutton_Click(object sender, EventArgs e)
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK) NewDataFoldertextBox.Text = openFileDialog1.FileName;
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

        var m_tmpold = new TMPFont(new UnityAssetFile(OldAssettextBox.Text, OldDataFoldertextBox.Text));
        var m_tmpnew = new TMPFont(new UnityAssetFile(NewAssettextBox.Text, NewDataFoldertextBox.Text));

        var compression = AssetBundleCompressionType.None;

        if (m_tmpold.m_AssetFile.IsBundle)
        {
            var message =
                new CustomMessageBox("", "You want compress the final bundle?", "With LZ4", "With LZMA", "No");
            var result = message.ShowDialog();
            if (result == DialogResult.Yes)
                compression = AssetBundleCompressionType.LZ4;
            else if (result == DialogResult.No)
                compression = AssetBundleCompressionType.LZMA;
            else if (result == DialogResult.Cancel)
                compression = AssetBundleCompressionType.None;
        }

        var m = TMPFont_Importer.GetToImportList(m_tmpnew, m_tmpold, newsuffix.Text,
            oldsuffix.Text);

        var msg = "Fuentes a importar: ";
        foreach (var fontsnames in m) msg += $"\n{fontsnames}";
        MessageBox.Show(msg);

        var asset = TMPFont_Importer.Import(m_tmpnew, m_tmpold);

        AssetHelper.Save(asset.m_AssetFile, compression);

        AssetHelper.Close(m_tmpold.m_AssetFile);
        AssetHelper.Close(m_tmpnew.m_AssetFile);

        MessageBox.Show("Done!");
    }
}