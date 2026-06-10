using ArtKeeper.Core;
using ArtKeeper.Models;

namespace ArtKeeper.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public User CurrentUser { get; }

        private ObservableObject? _currentView;
        public ObservableObject? CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        public bool IsAdmin => CurrentUser.RoleId == 1;
        public bool IsCurator => CurrentUser.RoleId == 2 || CurrentUser.RoleId == 3;

        public RelayCommand NavigateExhibitsCommand { get; }
        public RelayCommand LogoutCommand { get; }

        public MainViewModel(User user)
        {
            CurrentUser = user;

            NavigateExhibitsCommand = new RelayCommand(o => CurrentView = new ExhibitsViewModel());
            LogoutCommand = new RelayCommand(o => Logout());

            if (IsCurator) CurrentView = new ExhibitsViewModel();
        }

        private void Logout()
        {
            var loginWindow = new Views.LoginWindow();
            loginWindow.Show();
            System.Windows.Application.Current.MainWindow.Close();
            System.Windows.Application.Current.MainWindow = loginWindow;
        }
    }
}