using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class PrescriptionMedicine
{
    public int Id { get; set; }

    public int PrescriptionId { get; set; }

    public int MedicineId { get; set; }

    public string? Dosage { get; set; }

    public string? Duration { get; set; }

    public string? Instruction { get; set; }

    public virtual Medicine Medicine { get; set; } = null!;

    public virtual Prescription Prescription { get; set; } = null!;
}
