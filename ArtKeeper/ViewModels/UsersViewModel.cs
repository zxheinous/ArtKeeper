using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using ArtKeeper.Core;
using ArtKeeper.Models;

namespace ArtKeeper.ViewModels
{
    public class UsersViewModel : ObservableObject
    {
        public ObservableCollection<User> UsersList { get; set; }

        public RelayCommand LoadDataCommand { get; }
        public RelayCommand AddCommand { get; }

        public UsersViewModel()
        {
            LoadDataCommand = new RelayCommand(o => LoadUsers());
            AddCommand = new RelayCommand(o => MessageBox.Show("Открытие окна регистрации нового сотрудника."));
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (var context = new ArtKeeperContext())
            {
                var data = context.Users.Include(u => u.Role).ToList();
                UsersList = new ObservableCollection<User>(data);
                OnPropertyChanged(nameof(UsersList));
            }
        }
    }
}