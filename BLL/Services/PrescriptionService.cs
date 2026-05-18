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
    public class PrescriptionService
    {
        PrescriptionRepo repo;
        Mapper mapper;
        public PrescriptionService(PrescriptionRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<PrescriptionDTO> GetAll()
        {
            var data = repo.GetAll();
            var res = mapper.Map<List<PrescriptionDTO>>(data);
            return res;
        }

        public PrescriptionDTO Get(int id)
        {
            var data = repo.Get(id);
            var res = mapper.Map<PrescriptionDTO>(data);
            return res;
        }

        public ResponseDTO Create(PrescriptionDTO p)
        {
            bool exists = repo.ExistsByAppointment(p.AppointmentId);

            if (exists == true)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = "Prescription already exists for this appointment"
                };
            }

            p.CreatedAt = DateTime.Now;

            var data = mapper.Map<Prescription>(p);

            var res = repo.Create(data);

            if (res == true)
            {
                return new ResponseDTO
                {
                    Success = true,
                    Message = "Prescription Created Successfully"
                };
            }
            else
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = "Failed to Create Prescription."
                };
            }

        }

        public bool Update(PrescriptionDTO p)
        {
            var data = mapper.Map<Prescription>(p);
            return repo.Update(data);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
