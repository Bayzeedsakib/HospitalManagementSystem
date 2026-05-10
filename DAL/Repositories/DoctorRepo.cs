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
            return db.Doctors.Where(x => x.Name.Contains(text) || x.Phone.Contains(text)).ToList();
        }
        public List<Doctor> GetAll()
        {
            return db.Doctors.ToList();
        }

        public bool Create(Doctor d)
        {
            db.Doctors.Add(d);
            return db.SaveChanges() > 0;
        }
    }
}
