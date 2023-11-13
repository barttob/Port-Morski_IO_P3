using System;
using System.Collections.Generic;

namespace Port_Morski.Models;

public partial class Operacje
{
    public int Id { get; set; }

    public string? Operation { get; set; }

    public int? ShipId { get; set; }

    public int? DockId { get; set; }

    public bool Approved { get; set; }

    public DateTime? Date { get; set; }

    public virtual Dock? Dock { get; set; }

    public virtual Ship? Ship { get; set; }
}
