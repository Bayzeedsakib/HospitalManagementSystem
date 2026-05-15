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
    .ForMember(dest => dest.DepartmentName,
        opt => opt.MapFrom(src => src.Department.Name));

            cfg.CreateMap<DoctorDTO, Doctor>()
    .ForMember(dest => dest.Department, opt => opt.Ignore());
        });

        public static Mapper GetMapper()
        {
            return new Mapper(config);
        }
    }  
  
}
