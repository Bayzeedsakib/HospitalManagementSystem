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
    public class UserService
    {
        UserRepo repo;
        Mapper mapper;
        public UserService(UserRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public UserDTO Get(string Email, string Password)
        {
            var exuser = repo.Get(Email, Password);
            

            var res = mapper.Map<UserDTO>(exuser);

            return res;
        }

        public List<UserDTO> GetAll()
        {
            var exuser = repo.GetAll();


            var res = mapper.Map<List<UserDTO>>(exuser);

            return res;
        }


        public UserDTO GetByEmail(string email)
        {
            var data = repo.GetByEmail(email);

            var mapper = MapperConfig.GetMapper();

            return mapper.Map<UserDTO>(data);
        }

        public UserDTO Create(UserDTO dto)
        {
            var mapper = MapperConfig.GetMapper();

            var data = mapper.Map<User>(dto);

            var created = repo.Create(data);

            return mapper.Map<UserDTO>(created);
        }
    }
}
