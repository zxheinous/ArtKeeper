using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ArtKeeper.Core;
using ArtKeeper.Models;

namespace ArtKeeper.ViewModels
{
    public class UsersViewModel : ObservableObject
    {
        public ObservableCollection<User> UsersList { get; set; }

        public UsersViewModel()
        {
            using (var context = new ArtKeeperContext())
            {
                var data = context.Users.Include(u => u.Role).ToList();
                UsersList = new ObservableCollection<User>(data);
            }
        }
    }
}