using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }


        [Required]
        public int PatientId { get; set; }


        [Required]
        public int DoctorId { get; set; }


        public string? PatientName { get; set; }


        public string? DoctorName { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateOnly AppointmentDate { get; set; }


        [Required]
        [DataType(DataType.Time)]
        public TimeOnly AppointmentTime { get; set; }


        public int? SerialNo { get; set; }


        public string? Status { get; set; }


        [StringLength(300)]
        public string? ProblemDescription { get; set; }


        public int? CreatedBy { get; set; }


        public DateTime? CreatedAt { get; set; }
    }
}
