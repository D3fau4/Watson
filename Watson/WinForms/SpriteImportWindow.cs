﻿using AssetsTools.NET;
using Watson.Lib.Assets;
using Watson.Lib.IO;
using Watson.Lib.Utils;
using Watson.Lib.Utils.Helpers;

namespace Watson;

public partial class SpriteImportWindow : Form
{
    public SpriteImportWindow()
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

    private void Submitbutton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(NewAssettextBox.Text) || string.IsNullOrEmpty(OldAssettextBox.Text))
        {
            MessageBox.Show("Porfavor rellene todos los campos");
            return;
        }

        if (!File.Exists(NewAssettextBox.Text) || !File.Exists(OldAssettextBox.Text))
        {
            MessageBox.Show("Porfavor establece un asset.");
            return;
        }

        var m_old = new Sprites(new UnityAssetFile(OldAssettextBox.Text));
        var m_new = new Sprites(new UnityAssetFile(NewAssettextBox.Text));

        var compression = AssetBundleCompressionType.None;

        if (m_old.m_AssetFile.IsBundle)
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

        var m = Sprites_Importer.Import(m_new, m_old);

        AssetHelper.Save(m.m_AssetFile, compression);

        AssetHelper.Close(m_old.m_AssetFile);
        AssetHelper.Close(m_new.m_AssetFile);

        MessageBox.Show("Done!");
    }
}