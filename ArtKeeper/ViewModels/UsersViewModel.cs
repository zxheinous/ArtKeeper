using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using ArtKeeper.Core;
using ArtKeeper.Models;

namespace ArtKeeper.ViewModels
{
    public class UsersViewModel : ObservableObject
    {
        private ObservableCollection<User> _usersList = new();
        private ObservableCollection<Role> _availableRoles = new();

        private string _newFullName = "";
        private string _newUsername = "";
        private string _newEmail = "";
        private Role? _newRole;
        private bool _isAddingPanelVisible;

        public ObservableCollection<User> UsersList { get => _usersList; set { _usersList = value; OnPropertyChanged(); } }
        public ObservableCollection<Role> AvailableRoles { get => _availableRoles; set { _availableRoles = value; OnPropertyChanged(); } }

        public string NewFullName { get => _newFullName; set { _newFullName = value; OnPropertyChanged(); } }
        public string NewUsername { get => _newUsername; set { _newUsername = value; OnPropertyChanged(); } }
        public string NewEmail { get => _newEmail; set { _newEmail = value; OnPropertyChanged(); } }
        public Role? NewRole { get => _newRole; set { _newRole = value; OnPropertyChanged(); } }
        public bool IsAddingPanelVisible { get => _isAddingPanelVisible; set { _isAddingPanelVisible = value; OnPropertyChanged(); } }

        public RelayCommand LoadDataCommand { get; }
        public RelayCommand ToggleAddingPanelCommand { get; }
        public RelayCommand SaveCommand { get; }

        public UsersViewModel()
        {
            LoadDataCommand = new RelayCommand(o => LoadUsers());
            ToggleAddingPanelCommand = new RelayCommand(o => IsAddingPanelVisible = !IsAddingPanelVisible);
            SaveCommand = new RelayCommand(SaveUser, CanSaveUser);
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (var context = new ArtKeeperContext())
            {
                var data = context.Users.Include(u => u.Role).ToList();
                UsersList = new ObservableCollection<User>(data);
                AvailableRoles = new ObservableCollection<Role>(context.Roles.ToList());
            }
        }

        private bool CanSaveUser(object? parameter) => !string.IsNullOrWhiteSpace(NewFullName) && !string.IsNullOrWhiteSpace(NewUsername) && NewRole != null;

        private void SaveUser(object? parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox?.Password;

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пароль не может быть пустым!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var context = new ArtKeeperContext())
                {
                    var user = new User
                    {
                        FullName = NewFullName,
                        Username = NewUsername,
                        PasswordHash = password,
                        Email = NewEmail,
                        RoleId = NewRole!.RoleId,
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    };
                    context.Users.Add(user);
                    context.SaveChanges();
                }

                IsAddingPanelVisible = false;
                NewFullName = ""; NewUsername = ""; NewEmail = "";
                if (passwordBox != null) passwordBox.Password = "";

                LoadUsers();
                MessageBox.Show("Сотрудник успешно зарегистрирован!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! Возможно, такой логин или Email уже существует.\n" + ex.Message, "Ошибка БД", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}