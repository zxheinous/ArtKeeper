using System;
using System.Collections.Generic;

namespace ArtKeeper.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int RoleId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastLogin { get; set; }

    public virtual ICollection<ConditionPassport> ConditionPassports { get; set; } = new List<ConditionPassport>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Exhibition> Exhibitions { get; set; } = new List<Exhibition>();

    public virtual ICollection<MuseumHall> MuseumHalls { get; set; } = new List<MuseumHall>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<RestorationTask> RestorationTasks { get; set; } = new List<RestorationTask>();

    public virtual ICollection<RestorationWorkshop> RestorationWorkshops { get; set; } = new List<RestorationWorkshop>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<SupplyMaterial> SupplyMaterials { get; set; } = new List<SupplyMaterial>();

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
