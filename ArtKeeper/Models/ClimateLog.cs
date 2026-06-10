using System;
using System.Collections.Generic;

namespace ArtKeeper.Models;

public partial class ClimateLog
{
    public int LogId { get; set; }

    public int HallId { get; set; }

    public DateTime CheckDatetime { get; set; }

    public decimal CurrentTemperature { get; set; }

    public decimal CurrentHumidity { get; set; }

    public int? IlluminationLux { get; set; }

    public bool DeviationFlag { get; set; }

    public virtual MuseumHall Hall { get; set; } = null!;
}
