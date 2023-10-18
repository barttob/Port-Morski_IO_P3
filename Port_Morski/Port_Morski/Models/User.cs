using System;
using System.Collections.Generic;

namespace Port_Morski.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? UserRole { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<EmpSchedule> EmpSchedules { get; set; } = new List<EmpSchedule>();
}
