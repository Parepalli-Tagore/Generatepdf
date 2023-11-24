using System;
using System.Collections.Generic;

namespace Generate_pdf.Models;

public partial class Disease
{
    public long Id { get; set; }

    public string DiseasesName { get; set; } = null!;

    public long? DoctorId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Doctor? Doctor { get; set; }
}
