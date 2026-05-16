using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepo
    {
        HospitalDbContext db;
        public UserRepo(HospitalDbContext db)
        {
            this.db = db;
        }

        public User Get(string Email, string Password)
        {
            var data = db.Users.Where(x => x.Email.Equals(Email) && x.Password.Equals(Password)).FirstOrDefault();
            return data;
        }
    }
}
