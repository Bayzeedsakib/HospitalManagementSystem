using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PatientRepo
    {
        HospitalDbContext db;
        public PatientRepo(HospitalDbContext db)
        {
            this.db = db;
        }

        public bool Create(Patient p)
        {
            db.Patients.Add(p);
            return db.SaveChanges() > 0;
        }
    }
}
