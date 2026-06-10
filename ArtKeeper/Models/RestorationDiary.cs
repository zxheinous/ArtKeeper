using System;
using System.Collections.Generic;

namespace ArtKeeper.Models;

public partial class RestorationDiary
{
    public int DiaryId { get; set; }

    public int TaskId { get; set; }

    public DateTime EntryDate { get; set; }

    public string OperationType { get; set; } = null!;

    public string ActionsTaken { get; set; } = null!;

    public string? PhotoPath { get; set; }

    public virtual RestorationTask Task { get; set; } = null!;
}
