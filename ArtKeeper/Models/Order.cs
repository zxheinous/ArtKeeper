using System;
using System.Collections.Generic;

namespace ArtKeeper.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string OrderNumber { get; set; } = null!;

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public string OrderStatus { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public int? ExhibitionId { get; set; }

    public DateTime? TourDatetime { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Exhibition? Exhibition { get; set; }
}
