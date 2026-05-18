using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MapperConfig
    {
        static MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Patient, PatientDTO>().ReverseMap();
            cfg.CreateMap<Department, DepartmentDTO>().ReverseMap();
            cfg.CreateMap<Doctor, DoctorDTO>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));


            cfg.CreateMap<DoctorDTO, Doctor>()
            .ForMember(dest => dest.Department, opt => opt.Ignore());

            cfg.CreateMap<User, UserDTO>().ReverseMap();

            cfg.CreateMap<Appointment, AppointmentDTO>()
            .ForMember(dest => dest.PatientName,
            opt => opt.MapFrom(src => src.Patient.Name))

            .ForMember(dest => dest.DoctorName,
             opt => opt.MapFrom(src => src.Doctor.Name));

            cfg.CreateMap<AppointmentDTO, Appointment>()
                .ForMember(dest => dest.Patient, opt => opt.Ignore())
                .ForMember(dest => dest.Doctor, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByNavigation, opt => opt.Ignore());

            cfg.CreateMap<Prescription, PrescriptionDTO>()

                .ForMember(dest => dest.PatientName,

                    opt => opt.MapFrom(
                        src => src.Appointment.Patient.Name
                    )
                )

                .ForMember(dest => dest.DoctorName,

                    opt => opt.MapFrom(
                        src => src.Appointment.Doctor.Name
                    )
                );

                //.ForMember(dest => dest.AppointmentDate,

                //    opt => opt.MapFrom(
                //        src => src.Appointment.AppointmentDate
                //    )
                //);



            cfg.CreateMap<PrescriptionDTO, Prescription>()

                .ForMember(dest => dest.Appointment,
                    opt => opt.Ignore());

        });
        public static Mapper GetMapper()
        {
            return new Mapper(config);
        }
    }  
  
}
