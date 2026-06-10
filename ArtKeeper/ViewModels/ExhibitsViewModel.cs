using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ArtKeeper.Core;
using ArtKeeper.Models;

namespace ArtKeeper.ViewModels
{
    public class ExhibitsViewModel : ObservableObject
    {
        private ObservableCollection<Exhibit> _exhibits;
        public ObservableCollection<Exhibit> Exhibits
        {
            get => _exhibits;
            set { _exhibits = value; OnPropertyChanged(); }
        }

        public RelayCommand LoadDataCommand { get; }

        public ExhibitsViewModel()
        {
            _exhibits = new ObservableCollection<Exhibit>();
            LoadDataCommand = new RelayCommand(o => LoadExhibits());
            LoadExhibits();
        }

        private void LoadExhibits()
        {
            using (var context = new ArtKeeperContext())
            {
                var data = context.Exhibits.Include(e => e.Hall).ToList();
                Exhibits = new ObservableCollection<Exhibit>(data);
            }
        }
    }
}