using System;
using System.Collections.Generic;

namespace Port_Morski.Models;

public partial class EmpSchedule
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? ShiftType { get; set; }

    public virtual User? User { get; set; }
}
