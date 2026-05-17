using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AppointmentRepo
    {
        HospitalDbContext db;
        public AppointmentRepo(HospitalDbContext db)
        {
            this.db = db;
        }

        public List<Appointment> GetAll()
        {
            return db.Appointments.Include(a => a.Patient)
                .Include(a => a.Doctor).Include(a => a.CreatedByNavigation).ToList();
        }

        public Appointment GetById(int id)
        {
            return db.Appointments.Include(a => a.Patient)
                .Include(a => a.Doctor).Include(a => a.CreatedByNavigation).FirstOrDefault(a => a.Id == id);
        }

        public bool Create(Appointment a)
        {
            db.Appointments.Add(a);
            return db.SaveChanges() > 0;
        }

        public bool Update(Appointment a)
        {
            var exobj = GetById(a.Id);

            if (exobj == null)
            {
                return false;
            }

            //for prevent createAt and createdBy
            exobj.PatientId = a.PatientId;
            exobj.DoctorId = a.DoctorId;
            exobj.AppointmentDate = a.AppointmentDate;
            exobj.AppointmentTime = a.AppointmentTime;
            exobj.SerialNo = a.SerialNo;
            exobj.Status = a.Status;
            exobj.ProblemDescription = a.ProblemDescription;

            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var exobj = GetById(id);
            db.Appointments.Remove(exobj);
            return db.SaveChanges() > 0;
        }

        //Doctor + Date appointments
        public List<Appointment> GetByDoctorAndDate(int doctorId, DateOnly date)
        {
            return db.Appointments.Where(a => a.DoctorId == doctorId && a.AppointmentDate == date).ToList();
        }

        //SerialNo. Count
        public int GetTodayCountByDoctor(int doctorId,DateOnly date)
        {
            return db.Appointments.Count(a => a.DoctorId == doctorId && a.AppointmentDate == date);
        }

        //Check Time Conflict
        public bool IsTimeSlotToken(int doctorId, DateOnly date, TimeOnly time)
        {
            return db.Appointments.Any(a => a.DoctorId == doctorId &&
            a.AppointmentDate == date && a.AppointmentTime == time);
        }

        public List<Appointment> Search(string text)
        {
            return db.Appointments.Include(a => a.Patient).Include(a => a.Doctor)
                .Where(a => a.Patient.Name.Contains(text) || a.Doctor.Name.Contains(text)).ToList();
        }
    }
}
