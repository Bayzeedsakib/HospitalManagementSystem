using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Doctor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Gender { get; set; }

    public string? Specialization { get; set; }

    public string? Qualification { get; set; }

    public int DepartmentId { get; set; }

    public TimeOnly? AvailableFrom { get; set; }

    public TimeOnly? AvailableTo { get; set; }

    public int? MaxPatientsPerDay { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Department Department { get; set; } = null!;

    public virtual User? User { get; set; }
}
