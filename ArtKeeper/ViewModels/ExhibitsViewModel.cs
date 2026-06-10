using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ArtKeeper.Core;
using ArtKeeper.Models;

namespace ArtKeeper.ViewModels
{
    public class ExhibitionsViewModel : ObservableObject
    {
        public ObservableCollection<Exhibition> ExhibitionsList { get; set; }

        public ExhibitionsViewModel()
        {
            using (var context = new ArtKeeperContext())
            {
                var data = context.Exhibitions.Include(e => e.Curator).ToList();
                ExhibitionsList = new ObservableCollection<Exhibition>(data);
            }
        }
    }
}