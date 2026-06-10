using System;
using System.Collections.Generic;

namespace ArtKeeper.Models;

public partial class Exhibition
{
    public int ExhibitionId { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int CuratorId { get; set; }

    public string? Description { get; set; }

    public decimal? TotalBudget { get; set; }

    public virtual User Curator { get; set; } = null!;

    public virtual ICollection<ExhibitExhibition> ExhibitExhibitions { get; set; } = new List<ExhibitExhibition>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
