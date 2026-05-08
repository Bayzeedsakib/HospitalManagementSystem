using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Appointment
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public DateOnly AppointmentDate { get; set; }

    public TimeOnly AppointmentTime { get; set; }

    public int? SerialNo { get; set; }

    public string? Status { get; set; }

    public string? ProblemDescription { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual Prescription? Prescription { get; set; }
}
