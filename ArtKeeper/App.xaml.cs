using System.Windows;

namespace ArtKeeper
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var loginWindow = new Views.LoginWindow();
            Current.MainWindow = loginWindow;
            loginWindow.Show();
        }
    }
}