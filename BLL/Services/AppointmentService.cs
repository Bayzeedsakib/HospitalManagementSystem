using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AppointmentService
    {
        AppointmentRepo repo;
        Mapper mapper;
        public AppointmentService(AppointmentRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<AppointmentDTO> GetAll()
        {
            var data = repo.GetAll();
            var res = mapper.Map<List<AppointmentDTO>>(data);
            return res;
        }

        public AppointmentDTO GetById(int id)
        {
            var data = repo.GetById(id);
            var res = mapper.Map<AppointmentDTO>(data);
            return res;
        }

        public bool Create(AppointmentDTO a)
        {
            bool taken = repo.IsTimeSlotToken(
                a.DoctorId,
                a.AppointmentDate,
                a.AppointmentTime
                );

            var data = mapper.Map<Appointment>(a);
            return repo.Create(data);
        }

        public bool Update(AppointmentDTO a)
        {
            var data = mapper.Map<Appointment>(a);
            return repo.Update(data);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }


    }
}
