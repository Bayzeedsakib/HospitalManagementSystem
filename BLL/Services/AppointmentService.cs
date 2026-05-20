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

        public List<AppointmentDTO> GetByDoctorId(int id)
        {
            var data = repo.GetAll().Where(x => x.DoctorId == id);
            var res = mapper.Map<List<AppointmentDTO>>(data);
            return res;
        }

        public List<AppointmentDTO> GetById(int id)
        {
            var data = repo.GetById(id);
            var res = mapper.Map<List<AppointmentDTO>>(data);
            return res;
        }

        public ResponseDTO Create(AppointmentDTO a)
        {
            bool taken = repo.IsTimeSlotToken(
                a.DoctorId,
                a.AppointmentDate,
                a.AppointmentTime
                );

            if (taken)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = "This doctor already has an appointment at this time."
                };
            }

            //auto serial number
            int count = repo.GetTodayCountByDoctor(a.DoctorId, a.AppointmentDate);
            a.SerialNo = count + 1;

            if (string.IsNullOrEmpty(a.Status))
            {
                a.Status = "Pending";
            }

            a.CreatedAt = DateTime.Now;

            var data = mapper.Map<Appointment>(a);
            bool res = repo.Create(data);

            if(res == true)
            {
                return new ResponseDTO
                {
                    Success = true,
                    Message = "Appointment Created Successfully"
                };
            }
            else
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = "Failed to Create Appointmen."
                };
            }
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

        public List<AppointmentDTO> Search(string text)
        {
            var data = repo.Search(text);
            var res = mapper.Map<List<AppointmentDTO>>(data);
            return res;
        }

    }
}
