using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PrescriptionRepo
    {
        HospitalDbContext db;
        AdHocMapper mapper;
        public PrescriptionRepo(HospitalDbContext db)
        {
            this.db = db;
        }

        public List<Prescription> GetAll()
        {
            return db.Prescriptions
                .Include(p => p.Appointment)
                    .ThenInclude(a => a.Patient)
                .Include(p => p.Appointment)
                    .ThenInclude(a => a.Doctor)
                .ToList();
        }

        public Prescription Get(int id)
        {
            return db.Prescriptions

                .Include(p => p.Appointment)
                    .ThenInclude(a => a.Patient)

                .Include(p => p.Appointment)
                    .ThenInclude(a => a.Doctor)

                .FirstOrDefault(p => p.Id == id);
        }

        public bool Create(Prescription prescription)
        {
            db.Prescriptions.Add(prescription);

            return db.SaveChanges() > 0;
        }

        public bool Update(Prescription prescription)
        {
            var ex = db.Prescriptions.Find(prescription.Id);

            if (ex == null)
            {
                return false;
            }

            ex.AppointmentId = prescription.AppointmentId;

            ex.Symptoms = prescription.Symptoms;

            ex.Diagnosis = prescription.Diagnosis;

            ex.Advice = prescription.Advice;

            ex.NextVisitDate = prescription.NextVisitDate;

            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var exobj = Get(id);
            db.Prescriptions.Remove(exobj);
            return db.SaveChanges() > 0;
        }

        //Prevent duplicate prescription
        //one appointment one prescription
        public bool ExistsByAppointment(int appointmentId)
        {
            return db.Prescriptions
                .Any(p => p.AppointmentId == appointmentId);
        }
    }
}
