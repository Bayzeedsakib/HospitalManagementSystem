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
        UserService userService;
        public DoctorService(DoctorRepo repo, UserService userService)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
            this.userService = userService;
        }

        public List<DoctorDTO> Search(string text)
        {
            var data = repo.Search(text);
            var res = mapper.Map <List<DoctorDTO>> (data);
            return res;
        }
        public List<DoctorDTO> Get()
        {
            var data = repo.Get();

            var res = mapper.Map<List<DoctorDTO>>(data);
            return res;
        }

        public string Create(DoctorDTO dto)
        {
            var user = userService.GetByEmail(dto.Email);



            // EMAIL DOES NOT EXIST
            if (user == null)
            {
                UserDTO newUser = new UserDTO()
                {
                    Name = dto.Name,
                    Email = dto.Email,

                    Password = "123",

                    RoleId = 2
                };

                var createdUser = userService.Create(newUser);


                dto.UserId = createdUser.Id;
            }



            // EMAIL EXISTS
            else
            {
                // EMAIL EXISTS BUT NOT DOCTOR ROLE
                if (user.RoleId != 2)
                {
                    return "Email already exists with another role.";
                }

                dto.UserId = user.Id;
            }


            var data = mapper.Map<Doctor>(dto);

            bool rs = repo.Create(data);

            if (rs)
            {
                return "Doctor created successfully.";
            }

            return "Failed to create doctor.";
        }

        public DoctorDTO GetById(int id)
        {
            var data = repo.GetById(id);
            var res = mapper.Map<DoctorDTO>(data);
            return res;
        }

        public bool Edit(DoctorDTO d)
        {
            var data = mapper.Map<Doctor>(d);
            return repo.Edit(data);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }

        public DoctorDTO GetByUserId(int userId)
        {
            var data = repo.GetByUserId(userId);

            return mapper.Map<DoctorDTO>(data);
        }
    }
}
