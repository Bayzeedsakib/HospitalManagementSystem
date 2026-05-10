using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DAL.Repositories
{
    public class PatientRepo
    {
        HospitalDbContext db;
        public PatientRepo(HospitalDbContext db)
        {
            this.db = db;
        }

        public List<Patient> Search(string text)
        {
            return db.Patients.Where(x => x.Name.Contains(text) || x.Phone.Contains(text)).ToList();
        }

        public List<Patient> GetAll()
        {
            return db.Patients.ToList();
        }

        public Patient GetById(int id)
        {
            return db.Patients.Find(id);
        }
        public bool Create(Patient p)
        {
            db.Patients.Add(p);
            return db.SaveChanges() > 0;
        }

        public bool Edit(Patient p)
        {
            var exobj = GetById(p.Id);
            db.Entry(exobj).CurrentValues.SetValues(p);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var exobj = GetById(id);
            db.Patients.Remove(exobj);
            return db.SaveChanges() > 0;
        }
    }
}
