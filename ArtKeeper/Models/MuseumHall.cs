using System;
using System.Collections.Generic;

namespace ArtKeeper.Models;

public partial class MuseumHall
{
    public int HallId { get; set; }

    public string HallName { get; set; } = null!;

    public int Floor { get; set; }

    public int MaxCapacity { get; set; }

    public int CurrentCount { get; set; }

    public int ResponsibleId { get; set; }

    public decimal TargetTemperature { get; set; }

    public decimal TargetHumidity { get; set; }

    public virtual ICollection<ClimateLog> ClimateLogs { get; set; } = new List<ClimateLog>();

    public virtual ICollection<Exhibit> Exhibits { get; set; } = new List<Exhibit>();

    public virtual User Responsible { get; set; } = null!;
}
