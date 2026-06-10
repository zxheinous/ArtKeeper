using System;
using System.Collections.Generic;

namespace ArtKeeper.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public string ReportType { get; set; } = null!;

    public int GeneratedBy { get; set; }

    public DateTime GeneratedAt { get; set; }

    public string? ParametersJson { get; set; }

    public string FilePath { get; set; } = null!;

    public virtual User GeneratedByNavigation { get; set; } = null!;
}
