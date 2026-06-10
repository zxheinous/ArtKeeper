using System;
using System.Collections.Generic;

namespace ArtKeeper.Models;

public partial class RestorationWorkshop
{
    public int WorkshopId { get; set; }

    public string WorkshopName { get; set; } = null!;

    public string Specialization { get; set; } = null!;

    public int HeadId { get; set; }

    public string? Phone { get; set; }

    public virtual User Head { get; set; } = null!;

    public virtual ICollection<RestorationTask> RestorationTasks { get; set; } = new List<RestorationTask>();
}
