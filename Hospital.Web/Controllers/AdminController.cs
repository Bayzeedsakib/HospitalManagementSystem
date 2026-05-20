using Microsoft.AspNetCore.Mvc;
using BLL.Services;

namespace Hospital.Web.Controllers
{
    public class AdminController : Controller
    {
        PatientService patientservice;
        DoctorService doctorservice;
        DepartmentService departmentservice;
        AppointmentService appointmentservice;
        public AdminController(PatientService patientservice,DepartmentService departmentservice, DoctorService doctorservice, AppointmentService appointmentservice)
        {
            this.patientservice = patientservice;
            this.departmentservice = departmentservice;
            this.doctorservice = doctorservice;
            this.appointmentservice = appointmentservice;
        }

        //private bool IsAdmin()
        //{
        //    return HttpContext.Session.GetString("role") == "Admin";
        //}

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleId = HttpContext.Session.GetInt32("RoleId");
            //if (!IsAdmin())
            //{
            //    return RedirectToAction("AccessDenied", "Auth");
            //}

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (roleId != 1)
            {
                return RedirectToAction("AccessDenied", "Auth");
            }
            ViewBag.TotalPatients = patientservice.GetAll().Count;
            ViewBag.TotalDoctors = doctorservice.Get().Count;
            ViewBag.TotalDepartments = departmentservice.GetAll().Count;
            ViewBag.TotalAppointments = appointmentservice.GetAll().Count;

            return View();
        }
    }
}