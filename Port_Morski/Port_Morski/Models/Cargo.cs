using System;
using System.Collections.Generic;

namespace Port_Morski.Models;

public partial class Cargo
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Weight { get; set; }

    public int? ShipId { get; set; }

    public string? Status { get; set; }

    public virtual Ship? Ship { get; set; }
}
