using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class DoctorDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
        public string? Gender { get; set; }
        public string? Specialization { get; set; }
        public string? Qualification { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public TimeOnly? AvailableFrom { get; set; }
        public TimeOnly? AvailableTo { get; set; }
        public int? MaxPatientsPerDay { get; set; }
    }
}
