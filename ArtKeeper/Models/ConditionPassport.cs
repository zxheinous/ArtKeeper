using System;
using System.Collections.Generic;

namespace ArtKeeper.Models;

public partial class ConditionPassport
{
    public int PassportId { get; set; }

    public int ExhibitId { get; set; }

    public DateOnly DiagnosisDate { get; set; }

    public int ExpertId { get; set; }

    public string? BaseDamage { get; set; }

    public string? SurfaceDamage { get; set; }

    public string? БиологическийФактор { get; set; }

    public string Заключение { get; set; } = null!;

    public virtual Exhibit Exhibit { get; set; } = null!;

    public virtual User Expert { get; set; } = null!;
}
