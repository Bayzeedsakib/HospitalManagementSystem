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
   public  class DepartmentService
   
    {
        DepartmentRepo repo;
        Mapper mapper;
        public DepartmentService(DepartmentRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<DepartmentDTO> Search(string text)
        {
            var data = repo.Search(text);

            var res = mapper.Map<List<DepartmentDTO>>(data);
            return res;
        }


        public List<DepartmentDTO> GetAll()
        {
            var data = repo.GetAll();

            var res = mapper.Map<List<DepartmentDTO>>(data);
            return res;
        }

        public DepartmentDTO GetById(int id)
        {
            var data = repo.GetById(id);

            var res = mapper.Map<DepartmentDTO>(data);
            return res;
        }
        public bool Create(DepartmentDTO d)
        {
            var data = mapper.Map<Department>(d);

            return repo.Create(data);
        }

        public bool Edit(DepartmentDTO d)
        {
            var data = mapper.Map<Department>(d);

            return repo.Edit(data);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }

    }
}
