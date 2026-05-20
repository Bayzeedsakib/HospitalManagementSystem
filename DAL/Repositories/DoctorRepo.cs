using Microsoft.EntityFrameworkCore;
using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DoctorRepo
    {
        HospitalDbContext db;
        public DoctorRepo(HospitalDbContext db)
        {
            this.db = db;
        }

        public List<Doctor> Search(string text)
        {
            return db.Doctors.Where(x => x.Name.Contains(text) || x.Phone.Contains(text)).Include(d => d.Department).ToList();
        }
        public List<Doctor> Get()
        {
            return db.Doctors.Include(d => d.Department).ToList();
        }

        public bool Create(Doctor d)
        {
            db.Doctors.Add(d);
            return db.SaveChanges() > 0;
        }

        public Doctor GetById(int id)
        {
            return db.Doctors.Find(id);
        }

        public bool Edit(Doctor d)
        {
            var exobj = GetById(d.Id);
            db.Entry(exobj).CurrentValues.SetValues(d);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id) 
        {
            var exobj = GetById(id);
            db.Doctors.Remove(exobj);
            return db.SaveChanges() > 0;
        }

        public Doctor GetByUserId(int userId)
        {
            return db.Doctors
                .FirstOrDefault(d => d.UserId == userId);
        }
    }
}
