using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Medicine
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; } = new List<PrescriptionMedicine>();
}
