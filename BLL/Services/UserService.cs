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
    }
}
