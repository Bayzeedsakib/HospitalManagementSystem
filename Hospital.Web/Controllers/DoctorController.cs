using BLL.DTOs;
using BLL.Services;
using DAL.EF.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Web.Controllers
{
    public class DoctorController : Controller
    {
        DoctorService service;
        DepartmentService Service;
        AppointmentService appointmentservice;
        UserService userService;
        public DoctorController(DoctorService service, DepartmentService Service, AppointmentService appointmentservice, UserService us)
        {
            this.service = service;
            this.Service = Service;
            this.appointmentservice = appointmentservice;
            this.userService = us;
        }
        public IActionResult Index(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var searched = service.Search(search);
                return View(searched);
            }

            var data = service.Get();
            return View(data);
        }

        [HttpGet]
        public IActionResult DoctorDashboard()
        {
            var userId = HttpContext.Session.GetInt32("UserId");


            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            
            var doctor = service.GetByUserId(userId.Value);

            if (doctor == null)
            {
                return RedirectToAction(
                    "AccessDenied",
                    "Auth"
                );
            }

            
            var appointments = appointmentservice.GetByDoctorId(doctor.Id);


            
            ViewBag.Doctor = doctor;

            return View(appointments);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            ViewBag.Departments = new SelectList(Service.GetAll(), "Id", "Name");
            var data = service.GetById(id);
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");
            if (roleId != 1)
            {
                return RedirectToAction("AccessDenied", "Auth");
            }
            ViewBag.Departments = new SelectList(Service.GetAll(), "Id","Name");
            return View(new DoctorDTO()); 
        }

        [HttpPost]
        public IActionResult Create(DoctorDTO d)
        {
            if (ModelState.IsValid)
            {
                var msg = service.Create(d);

                TempData["Msg"] = msg;

                if (msg.Contains("successfully"))
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Departments = new SelectList(Service.GetAll(),"Id","Name");

            return View(d);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Departments = new SelectList(Service.GetAll(), "Id", "Name");
            var data = service.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(DoctorDTO d)
        {
            if (ModelState.IsValid)
            {
                var res = service.Edit(d);
                if (res == true)
                {
                    TempData["Msg"] = "Doctor's details edited";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Departments = new SelectList(Service.GetAll(), "Id", "Name");
            return View(d);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = service.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(int id, string Decision)
        {
            if (Decision.Equals("Yes"))
            {
                var res = service.Delete(id);
                if(res == true)
                {
                    TempData["Msg"] = "Deleted one doctor details";
                    return RedirectToAction("Index");
                }
            }
            return View("Index");
        }

    }
}
