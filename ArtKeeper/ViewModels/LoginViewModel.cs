using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ArtKeeper.Core;
using ArtKeeper.Models;

namespace ArtKeeper.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private string _username = "";
        private string _errorMessage = "";

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public RelayCommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(Username);
        }

        private void ExecuteLogin(object? parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox?.Password;

            using (var context = new ArtKeeperContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == Username && u.PasswordHash == password);

                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        ErrorMessage = "Учетная запись заблокирована!";
                        return;
                    }

                    var mainWindow = new Views.MainWindow();
                    var mainVM = new MainViewModel(user);
                    mainWindow.DataContext = mainVM;
                    mainWindow.Show();

                    Application.Current.MainWindow.Close();
                    Application.Current.MainWindow = mainWindow;
                }
                else
                {
                    ErrorMessage = "Неверный логин или пароль!";
                }
            }
        }
    }
}