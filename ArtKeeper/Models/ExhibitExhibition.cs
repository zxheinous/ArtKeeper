using System;
using System.Collections.Generic;

namespace ArtKeeper.Models;

public partial class ExhibitExhibition
{
    public int RecordId { get; set; }

    public int ExhibitionId { get; set; }

    public int ExhibitId { get; set; }

    public string? PlacementLocation { get; set; }

    public virtual Exhibit Exhibit { get; set; } = null!;

    public virtual Exhibition Exhibition { get; set; } = null!;
}
