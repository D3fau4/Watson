namespace Watson;

public partial class CustomMessageBox : Form
{
    public int Result = 0;

    public CustomMessageBox(string Title, string Comment, string Yes = "Yes", string No = "No",
        string Cancel = "Cancel")
    {
        InitializeComponent();
        label1.Text = Comment;
        button1.Text = Yes;
        button2.Text = No;
        button3.Text = Cancel;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Yes;
        Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.No;
        Close();
    }

    private void button3_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}