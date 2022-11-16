namespace Watson;

public partial class ProgressBarWindow : Form
{
    public ProgressBarWindow(string comment = "Working...", int max = 10, int min = 0)
    {
        InitializeComponent();
        label1.Text = comment;
        progressBar1.Maximum = max;
        progressBar1.Minimum = min;
    }

    public void SetMessage(string message)
    {
        label1.Text = message;
    }

    public void setSetProgressbar(int i)
    {
        progressBar1.Value = i;
    }
}