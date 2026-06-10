using System;
using System.Collections.Generic;

namespace ArtKeeper.Models;

public partial class MaterialsInventory
{
    public int MaterialId { get; set; }

    public string MaterialName { get; set; } = null!;

    public string Category { get; set; } = null!;

    public int WarehouseId { get; set; }

    public decimal CurrentQuantity { get; set; }

    public string UnitMeasure { get; set; } = null!;

    public DateOnly? ExpirationDate { get; set; }

    public virtual Warehouse Warehouse { get; set; } = null!;
}
