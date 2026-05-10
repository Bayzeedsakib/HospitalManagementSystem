using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DAL.Repositories
{
    public class DepartmentRepo
    {
        HospitalDbContext db;
        public DepartmentRepo(HospitalDbContext db)
        {
            this.db = db;
        }
        public List<Department> Search(string text)
        {
            return db.Departments.Where(x => x.Name.Contains(text)).ToList();
        }

        public List<Department> GetAll()
        {
            return db.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return db.Departments.Find(id);
        }

        public bool Create(Department d)
        {
            db.Departments.Add(d);
            return db.SaveChanges() > 0;
        }

        public bool Edit(Department d)
        {
            var exobj = GetById(d.Id);
            db.Entry(exobj).CurrentValues.SetValues(d);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var exobj = GetById(id);
            db.Departments.Remove(exobj);
            return db.SaveChanges() > 0;
        }
    }
}
