using System;
using System.Collections.Generic;

namespace Port_Morski.Models;

public partial class Ship
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public int? Capacity { get; set; }

    public virtual ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();

    public virtual ICollection<ShipSchedule> ShipSchedules { get; set; } = new List<ShipSchedule>();

    public virtual ICollection<Operations> Operationss { get; set; } = new List<Operations>();
}
