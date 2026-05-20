using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class PrescriptionDTO
    {
        public int Id { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }

        [StringLength(500)]
        public string? Symptoms { get; set; }

        [StringLength(500)]
        public string? Diagnosis { get; set; }

        [StringLength(500)]
        public string? Advice { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? NextVisitDate { get; set; }

        public DateTime? CreatedAt { get; set; }

    }
}
