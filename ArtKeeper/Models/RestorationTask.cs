using System;
using System.Collections.Generic;

namespace ArtKeeper.Models;

public partial class RestorationTask
{
    public int TaskId { get; set; }

    public int ExhibitId { get; set; }

    public int WorkshopId { get; set; }

    public int RestorerId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string TaskDescription { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? CommissionActNumber { get; set; }

    public virtual Exhibit Exhibit { get; set; } = null!;

    public virtual ICollection<RestorationDiary> RestorationDiaries { get; set; } = new List<RestorationDiary>();

    public virtual User Restorer { get; set; } = null!;

    public virtual RestorationWorkshop Workshop { get; set; } = null!;
}
