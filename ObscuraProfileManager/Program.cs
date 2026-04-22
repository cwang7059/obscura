namespace ObscuraProfileManager;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        try
        {
            Application.Run(new Form1());
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Obscura Profile Manager",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}
