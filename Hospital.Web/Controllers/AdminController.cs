using Microsoft.AspNetCore.Mvc;
using BLL.Services;

namespace Hospital.Web.Controllers
{
    public class AdminController : Controller
    {
        PatientService patientservice;
        DoctorService doctorservice;
        DepartmentService departmentservice;
        public AdminController(PatientService patientservice,DepartmentService departmentservice, DoctorService doctorservice)
        {
            this.patientservice = patientservice;
            this.departmentservice = departmentservice;
            this.doctorservice = doctorservice;
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
            //ViewBag.TotalAppointments = AppointmentService.GetAll().Count;

            return View();
        }
    }
}