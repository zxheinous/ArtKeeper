using System;
using System.Collections.Generic;

namespace ArtKeeper.Models;

public partial class SupplyMaterial
{
    public int SupplyId { get; set; }

    public int SupplierId { get; set; }

    public DateOnly SupplyDate { get; set; }

    public string MaterialName { get; set; } = null!;

    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalCost { get; set; }

    public int ReceivedBy { get; set; }

    public virtual User ReceivedByNavigation { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
