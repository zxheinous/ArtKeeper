using System;
using System.Collections.Generic;

namespace ArtKeeper.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? ContactPerson { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Inn { get; set; }

    public virtual ICollection<SupplyMaterial> SupplyMaterials { get; set; } = new List<SupplyMaterial>();
}
