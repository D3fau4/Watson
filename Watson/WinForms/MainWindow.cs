namespace Watson;

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
        var tMPFontImportWindow = new TMPFontImportWindow();
        tMPFontImportWindow.ShowDialog();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        var tSpriteImportWindow = new SpriteImportWindow();
        tSpriteImportWindow.ShowDialog();
    }
}