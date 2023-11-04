using System;
using System.Collections.Generic;

namespace Port_Morski.Models;

public partial class Dock
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<ShipSchedule> ShipSchedules { get; set; } = new List<ShipSchedule>();

    public virtual ICollection<Operations> Operationss { get; set; } = new List<Operations>();
}
