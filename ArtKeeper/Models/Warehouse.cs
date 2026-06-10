using System;
using System.Collections.Generic;

namespace ArtKeeper.Models;

public partial class Warehouse
{
    public int WarehouseId { get; set; }

    public string WarehouseName { get; set; } = null!;

    public string? Location { get; set; }

    public int ResponsibleId { get; set; }

    public virtual ICollection<MaterialsInventory> MaterialsInventories { get; set; } = new List<MaterialsInventory>();

    public virtual User Responsible { get; set; } = null!;
}
