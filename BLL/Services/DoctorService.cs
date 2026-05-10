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
    public class DoctorService
    {
        DoctorRepo repo;
        Mapper mapper;
        public DoctorService(DoctorRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<DoctorDTO> Search(string text)
        {
            var data = repo.Search(text);
            var res = mapper.Map <List<DoctorDTO>> (data);
            return res;
        }
        public List<DoctorDTO> GetAll()
        {
            var data = repo.GetAll();

            var res = mapper.Map<List<DoctorDTO>>(data);
            return res;
        }

        public bool Create(DoctorDTO d)
        {
            var data = mapper.Map<Doctor>(d);
            return repo.Create(data);
        }
    }
}
