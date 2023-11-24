using System;
using System.Collections.Generic;

namespace Generate_pdf.Models;

public partial class Appointment
{
    public long Id { get; set; }

    public string PatientName { get; set; } = null!;

    public string MedicalIssue { get; set; } = null!;

    public string DocterToVisit { get; set; } = null!;

    public string DoctorAvaliableTime { get; set; } = null!;

    public DateTime AppointmentTime { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public virtual Disease MedicalIssueNavigation { get; set; } = null!;
}
