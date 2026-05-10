using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PatientService
    {
        PatientRepo repo;
        Mapper mapper;

        public PatientService(PatientRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<PatientDTO> GetAll()
        {
            var data = repo.GetAll();

            var res = mapper.Map<List<PatientDTO>>(data);
            return res;
           
        }

        public PatientDTO GetById(int id)
        {
            var data = repo.GetById(id);

            var res = mapper.Map<PatientDTO>(data);

            return res;
        }

        public bool Create(PatientDTO p)
        {
            var data = mapper.Map<Patient>(p);

            return repo.Create(data);
        }

        public bool Edit(PatientDTO p)
        {
            var data = mapper.Map<Patient>(p);

            return repo.Edit(data);
        }
        
        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
