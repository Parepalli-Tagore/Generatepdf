using System;
using System.Collections.Generic;

namespace Generate_pdf.Models;

public partial class Doctor
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Specialised { get; set; } = null!;

    public string Qualification { get; set; } = null!;

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public string? Experience { get; set; }

    public virtual ICollection<Disease> Diseases { get; set; } = new List<Disease>();
}
