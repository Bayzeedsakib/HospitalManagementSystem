using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Prescription
{
    public int Id { get; set; }

    public int AppointmentId { get; set; }

    public string? Symptoms { get; set; }

    public string? Diagnosis { get; set; }

    public string? Advice { get; set; }

    public DateOnly? NextVisitDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; } = new List<PrescriptionMedicine>();
}
