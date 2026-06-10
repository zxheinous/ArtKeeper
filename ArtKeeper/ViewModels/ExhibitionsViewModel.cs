using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using ArtKeeper.Core;
using ArtKeeper.Models;

namespace ArtKeeper.ViewModels
{
    public class ExhibitionsViewModel : ObservableObject
    {
        private ObservableCollection<Exhibition> _exhibitionsList = new();
        private ObservableCollection<User> _availableCurators = new();

        private string _newTitle = "";
        private DateTime _newStartDate = DateTime.Now;
        private DateTime _newEndDate = DateTime.Now.AddMonths(1);
        private decimal _newBudget = 0;
        private User? _newCurator;
        private bool _isAddingPanelVisible;

        public ObservableCollection<Exhibition> ExhibitionsList { get => _exhibitionsList; set { _exhibitionsList = value; OnPropertyChanged(); } }
        public ObservableCollection<User> AvailableCurators { get => _availableCurators; set { _availableCurators = value; OnPropertyChanged(); } }

        public string NewTitle { get => _newTitle; set { _newTitle = value; OnPropertyChanged(); } }
        public DateTime NewStartDate { get => _newStartDate; set { _newStartDate = value; OnPropertyChanged(); } }
        public DateTime NewEndDate { get => _newEndDate; set { _newEndDate = value; OnPropertyChanged(); } }
        public decimal NewBudget { get => _newBudget; set { _newBudget = value; OnPropertyChanged(); } }
        public User? NewCurator { get => _newCurator; set { _newCurator = value; OnPropertyChanged(); } }
        public bool IsAddingPanelVisible { get => _isAddingPanelVisible; set { _isAddingPanelVisible = value; OnPropertyChanged(); } }

        public Exhibition? SelectedExhibition { get; set; }

        public RelayCommand LoadDataCommand { get; }
        public RelayCommand ToggleAddingPanelCommand { get; }
        public RelayCommand SaveCommand { get; }

        public ExhibitionsViewModel()
        {
            LoadDataCommand = new RelayCommand(o => LoadData());
            ToggleAddingPanelCommand = new RelayCommand(o => IsAddingPanelVisible = !IsAddingPanelVisible);
            SaveCommand = new RelayCommand(o => SaveExhibition(), o => !string.IsNullOrWhiteSpace(NewTitle) && NewCurator != null);
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new ArtKeeperContext())
            {
                var data = context.Exhibitions.Include(e => e.Curator).ToList();
                ExhibitionsList = new ObservableCollection<Exhibition>(data);
                AvailableCurators = new ObservableCollection<User>(context.Users.Where(u => u.RoleId == 3 || u.RoleId == 1).ToList());
            }
        }

        private void SaveExhibition()
        {
            using (var context = new ArtKeeperContext())
            {
                var exh = new Exhibition
                {
                    Title = NewTitle,
                    StartDate = DateOnly.FromDateTime(NewStartDate),
                    EndDate = DateOnly.FromDateTime(NewEndDate),
                    CuratorId = NewCurator!.UserId,
                    TotalBudget = NewBudget
                };
                context.Exhibitions.Add(exh);
                context.SaveChanges();
            }
            IsAddingPanelVisible = false;
            NewTitle = ""; NewBudget = 0;
            LoadData();
            MessageBox.Show("Выставка успешно создана!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}