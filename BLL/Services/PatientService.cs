using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
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
        PatientService repo;
        Mapper mapper;

        public PatientService(PatientService repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public bool Create(PatientDTO p)
        {
            var data = mapper.Map<Patient>(p);

            return repo.Create(data);
        }
        
    }
}
