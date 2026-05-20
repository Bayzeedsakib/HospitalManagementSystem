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

        public List<User> GetAll()
        {
            var data = db.Users.ToList();
            return data;
        }

        public User GetByEmail(string email)
        {
            return db.Users
                .FirstOrDefault(u => u.Email == email);
        }

        public User Create(User user)
        {
            db.Users.Add(user);

            db.SaveChanges();

            return user;
        }
    }
}
