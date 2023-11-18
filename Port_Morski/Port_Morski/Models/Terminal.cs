using System;
using System.Collections.Generic;

namespace Port_Morski.Models;

public partial class Terminal
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public int? MaxCapacity { get; set; }

    public bool Available { get; set; }

    public DateTime? AvailableFromDate { get; set; }

    public int? DockId { get; set; }

    public virtual Dock? Dock { get; set; }
    public virtual ICollection<Magazine> Magazines { get; set; } = new List<Magazine>();
}