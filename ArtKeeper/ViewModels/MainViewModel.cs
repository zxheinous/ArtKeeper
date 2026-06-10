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
        public bool IsRestorer => CurrentUser.RoleId == 1 || CurrentUser.RoleId == 4;

        public RelayCommand NavigateExhibitsCommand { get; }
        public RelayCommand NavigateRestorationCommand { get; }
        public RelayCommand NavigateExhibitionsCommand { get; }
        public RelayCommand NavigateUsersCommand { get; }
        public RelayCommand LogoutCommand { get; }

        public MainViewModel(User user)
        {
            CurrentUser = user;

            NavigateExhibitsCommand = new RelayCommand(o => CurrentView = new ExhibitsViewModel());
            NavigateRestorationCommand = new RelayCommand(o => CurrentView = new RestorationViewModel());
            NavigateExhibitionsCommand = new RelayCommand(o => CurrentView = new ExhibitionsViewModel());
            NavigateUsersCommand = new RelayCommand(o => CurrentView = new UsersViewModel());

            LogoutCommand = new RelayCommand(o => Logout());

            if (IsAdmin) CurrentView = new UsersViewModel();
            else if (IsCurator) CurrentView = new ExhibitsViewModel();
            else if (IsRestorer) CurrentView = new RestorationViewModel();
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