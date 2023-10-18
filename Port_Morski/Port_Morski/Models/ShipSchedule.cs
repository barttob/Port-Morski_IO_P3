using System;
using System.Collections.Generic;

namespace Port_Morski.Models;

public partial class ShipSchedule
{
    public int Id { get; set; }

    public int? ShipId { get; set; }

    public int? DockId { get; set; }

    public DateTime? ArriveDate { get; set; }

    public DateTime? FlowOutDate { get; set; }

    public virtual Dock? Dock { get; set; }

    public virtual Ship? Ship { get; set; }
}
