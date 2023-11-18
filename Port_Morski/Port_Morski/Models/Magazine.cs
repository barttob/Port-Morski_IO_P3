using System;
using System.Collections.Generic;

namespace Port_Morski.Models;

public partial class Magazine
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Area { get; set; }

    public int? AvailableCapacity { get; set; }

    public string? Specification { get; set; }

    public int? DockId { get; set; }

    public virtual Dock? Dock { get; set; }
}