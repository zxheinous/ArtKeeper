using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using ArtKeeper.Core;
using ArtKeeper.Models;

namespace ArtKeeper.ViewModels
{
    public class RestorationViewModel : ObservableObject
    {
        private ObservableCollection<RestorationTask> _tasks = new();
        private ObservableCollection<Exhibit> _availableExhibits = new();
        private ObservableCollection<RestorationWorkshop> _availableWorkshops = new();
        private ObservableCollection<User> _availableRestorers = new();

        private RestorationTask? _selectedTask;
        private Exhibit? _newExhibit;
        private RestorationWorkshop? _newWorkshop;
        private User? _newRestorer;
        private string _newDescription = "";
        private string _newStatus = "В очереди";
        private bool _isAddingPanelVisible;

        public ObservableCollection<RestorationTask> Tasks
        {
            get => _tasks;
            set { _tasks = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Exhibit> AvailableExhibits
        {
            get => _availableExhibits;
            set { _availableExhibits = value; OnPropertyChanged(); }
        }

        public ObservableCollection<RestorationWorkshop> AvailableWorkshops
        {
            get => _availableWorkshops;
            set { _availableWorkshops = value; OnPropertyChanged(); }
        }

        public ObservableCollection<User> AvailableRestorers
        {
            get => _availableRestorers;
            set { _availableRestorers = value; OnPropertyChanged(); }
        }

        public RestorationTask? SelectedTask
        {
            get => _selectedTask;
            set { _selectedTask = value; OnPropertyChanged(); }
        }

        public Exhibit? NewExhibit
        {
            get => _newExhibit;
            set { _newExhibit = value; OnPropertyChanged(); }
        }

        public RestorationWorkshop? NewWorkshop
        {
            get => _newWorkshop;
            set { _newWorkshop = value; OnPropertyChanged(); }
        }

        public User? NewRestorer
        {
            get => _newRestorer;
            set { _newRestorer = value; OnPropertyChanged(); }
        }

        public string NewDescription
        {
            get => _newDescription;
            set { _newDescription = value; OnPropertyChanged(); }
        }

        public string NewStatus
        {
            get => _newStatus;
            set { _newStatus = value; OnPropertyChanged(); }
        }

        public bool IsAddingPanelVisible
        {
            get => _isAddingPanelVisible;
            set { _isAddingPanelVisible = value; OnPropertyChanged(); }
        }

        public RelayCommand LoadDataCommand { get; }
        public RelayCommand ToggleAddingPanelCommand { get; }
        public RelayCommand SaveNewTaskCommand { get; }
        public RelayCommand DeleteTaskCommand { get; }
        public RelayCommand CompleteTaskCommand { get; }

        public RestorationViewModel()
        {
            LoadDataCommand = new RelayCommand(o => LoadData());
            ToggleAddingPanelCommand = new RelayCommand(o => IsAddingPanelVisible = !IsAddingPanelVisible);
            SaveNewTaskCommand = new RelayCommand(o => SaveNewTask(), o => CanSaveNewTask());
            DeleteTaskCommand = new RelayCommand(o => DeleteTask(), o => SelectedTask != null);
            CompleteTaskCommand = new RelayCommand(o => CompleteTask(), o => SelectedTask != null && SelectedTask.EndDate == null);

            LoadData();
        }

        private void LoadData()
        {
            using (var context = new ArtKeeperContext())
            {
                var tasksData = context.RestorationTasks
                    .Include(t => t.Exhibit)
                    .Include(t => t.Restorer)
                    .Include(t => t.Workshop)
                    .ToList();
                Tasks = new ObservableCollection<RestorationTask>(tasksData);

                AvailableExhibits = new ObservableCollection<Exhibit>(context.Exhibits.ToList());
                AvailableWorkshops = new ObservableCollection<RestorationWorkshop>(context.RestorationWorkshops.ToList());

                AvailableRestorers = new ObservableCollection<User>(context.Users.Where(u => u.RoleId == 4 || u.RoleId == 1).ToList());
            }
        }

        private bool CanSaveNewTask()
        {
            return NewExhibit != null && NewWorkshop != null && NewRestorer != null && !string.IsNullOrWhiteSpace(NewDescription);
        }

        private void SaveNewTask()
        {
            if (NewExhibit == null || NewWorkshop == null || NewRestorer == null) return;

            using (var context = new ArtKeeperContext())
            {
                var newTask = new RestorationTask
                {
                    ExhibitId = NewExhibit.ExhibitId,
                    WorkshopId = NewWorkshop.WorkshopId,
                    RestorerId = NewRestorer.UserId,
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    TaskDescription = NewDescription,
                    Status = NewStatus
                };

                context.RestorationTasks.Add(newTask);
                context.SaveChanges();
            }

            NewDescription = "";
            IsAddingPanelVisible = false;
            LoadData();
            MessageBox.Show("Реставрационное задание успешно создано и внесено в базу данных!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteTask()
        {
            if (SelectedTask == null) return;

            var result = MessageBox.Show($"Вы уверены, что хотите безвозвратно удалить задание для экспоната \"{SelectedTask.Exhibit?.Title}\"?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                using (var context = new ArtKeeperContext())
                {
                    var taskToDelete = context.RestorationTasks.Find(SelectedTask.TaskId);
                    if (taskToDelete != null)
                    {
                        context.RestorationTasks.Remove(taskToDelete);
                        context.SaveChanges();
                    }
                }
                LoadData();
            }
        }

        private void CompleteTask()
        {
            if (SelectedTask == null) return;

            using (var context = new ArtKeeperContext())
            {
                var task = context.RestorationTasks.Find(SelectedTask.TaskId);
                if (task != null)
                {
                    task.Status = "Завершено";
                    task.EndDate = DateOnly.FromDateTime(DateTime.Now);
                    task.CommissionActNumber = "АК-" + new Random().Next(1000, 9999);
                    context.SaveChanges();
                }
            }
            LoadData();
            MessageBox.Show("Реставрационные манипуляции успешно завершены! Присвоен номер итогового акта комиссии: " + SelectedTask?.CommissionActNumber, "Дело закрыто", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}