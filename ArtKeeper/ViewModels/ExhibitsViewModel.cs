using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
        public RelayCommand AddCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public ExhibitsViewModel()
        {
            _exhibits = new ObservableCollection<Exhibit>();
            LoadDataCommand = new RelayCommand(o => LoadExhibits());

            AddCommand = new RelayCommand(o => AddExhibit());
            DeleteCommand = new RelayCommand(o => DeleteExhibit(o));

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

        private void AddExhibit()
        {
            MessageBox.Show("Открытие формы добавления нового экспоната в базу данных...", "Добавление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteExhibit(object? parameter)
        {
            MessageBox.Show("Функция удаления записи из БД.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}