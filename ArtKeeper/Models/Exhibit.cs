using System;
using System.Collections.Generic;

namespace ArtKeeper.Models;

public partial class Exhibit
{
    public int ExhibitId { get; set; }

    public string InventoryNumber { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string? CreationDate { get; set; }

    public string? MaterialTechnique { get; set; }

    public string? Dimensions { get; set; }

    public string CurrentStatus { get; set; } = null!;

    public int HallId { get; set; }

    public string? SafetyCategory { get; set; }

    public virtual ICollection<ConditionPassport> ConditionPassports { get; set; } = new List<ConditionPassport>();

    public virtual ICollection<ExhibitExhibition> ExhibitExhibitions { get; set; } = new List<ExhibitExhibition>();

    public virtual MuseumHall Hall { get; set; } = null!;

    public virtual ICollection<RestorationTask> RestorationTasks { get; set; } = new List<RestorationTask>();
}
