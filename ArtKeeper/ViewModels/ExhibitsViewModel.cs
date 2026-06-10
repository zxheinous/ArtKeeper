using System;
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
        private ObservableCollection<Exhibit> _exhibits = new();
        private ObservableCollection<MuseumHall> _availableHalls = new();

        private string _newInvNumber = "";
        private string _newTitle = "";
        private string _newAuthor = "Неизвестный автор";
        private string _newMaterial = "";
        private MuseumHall? _newHall;
        private bool _isAddingPanelVisible;

        public ObservableCollection<Exhibit> Exhibits { get => _exhibits; set { _exhibits = value; OnPropertyChanged(); } }
        public ObservableCollection<MuseumHall> AvailableHalls { get => _availableHalls; set { _availableHalls = value; OnPropertyChanged(); } }

        public string NewInvNumber { get => _newInvNumber; set { _newInvNumber = value; OnPropertyChanged(); } }
        public string NewTitle { get => _newTitle; set { _newTitle = value; OnPropertyChanged(); } }
        public string NewAuthor { get => _newAuthor; set { _newAuthor = value; OnPropertyChanged(); } }
        public string NewMaterial { get => _newMaterial; set { _newMaterial = value; OnPropertyChanged(); } }
        public MuseumHall? NewHall { get => _newHall; set { _newHall = value; OnPropertyChanged(); } }
        public bool IsAddingPanelVisible { get => _isAddingPanelVisible; set { _isAddingPanelVisible = value; OnPropertyChanged(); } }

        public Exhibit? SelectedExhibit { get; set; }

        public RelayCommand LoadDataCommand { get; }
        public RelayCommand ToggleAddingPanelCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public ExhibitsViewModel()
        {
            LoadDataCommand = new RelayCommand(o => LoadExhibits());
            ToggleAddingPanelCommand = new RelayCommand(o => IsAddingPanelVisible = !IsAddingPanelVisible);
            SaveCommand = new RelayCommand(o => SaveExhibit(), o => CanSave());
            DeleteCommand = new RelayCommand(o => DeleteExhibit(), o => SelectedExhibit != null);

            LoadExhibits();
        }

        private void LoadExhibits()
        {
            using (var context = new ArtKeeperContext())
            {
                var data = context.Exhibits.Include(e => e.Hall).ToList();
                Exhibits = new ObservableCollection<Exhibit>(data);
                AvailableHalls = new ObservableCollection<MuseumHall>(context.MuseumHalls.ToList());
            }
        }

        private bool CanSave() => !string.IsNullOrWhiteSpace(NewInvNumber) && !string.IsNullOrWhiteSpace(NewTitle) && NewHall != null;

        private void SaveExhibit()
        {
            try
            {
                using (var context = new ArtKeeperContext())
                {
                    var exhibit = new Exhibit
                    {
                        InventoryNumber = NewInvNumber,
                        Title = NewTitle,
                        Author = NewAuthor,
                        MaterialTechnique = NewMaterial,
                        CurrentStatus = "В фонде",
                        HallId = NewHall!.HallId
                    };
                    context.Exhibits.Add(exhibit);
                    context.SaveChanges();
                }
                IsAddingPanelVisible = false;
                NewInvNumber = ""; NewTitle = ""; NewMaterial = "";
                LoadExhibits();
                MessageBox.Show("Экспонат успешно добавлен в фонды!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения! Возможно, инвентарный номер уже существует.\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteExhibit()
        {
            if (SelectedExhibit == null) return;
            if (MessageBox.Show("Удалить экспонат?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var context = new ArtKeeperContext())
                {
                    var item = context.Exhibits.Find(SelectedExhibit.ExhibitId);
                    if (item != null) { context.Exhibits.Remove(item); context.SaveChanges(); }
                }
                LoadExhibits();
            }
        }
    }
}