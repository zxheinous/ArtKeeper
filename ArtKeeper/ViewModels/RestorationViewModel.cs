using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ArtKeeper.Core;
using ArtKeeper.Models;

namespace ArtKeeper.ViewModels
{
    public class RestorationViewModel : ObservableObject
    {
        public ObservableCollection<RestorationTask> Tasks { get; set; }

        public RestorationViewModel()
        {
            using (var context = new ArtKeeperContext())
            {
                var data = context.RestorationTasks
                    .Include(t => t.Exhibit)
                    .Include(t => t.Restorer)
                    .Include(t => t.Workshop)
                    .ToList();
                Tasks = new ObservableCollection<RestorationTask>(data);
            }
        }
    }
}