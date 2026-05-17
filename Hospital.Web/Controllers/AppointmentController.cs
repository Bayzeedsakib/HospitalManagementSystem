using BLL.DTOs;
using BLL.Services;
using DAL.EF.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Web.Controllers
{
    public class AppointmentController : Controller
    {
        AppointmentService service;
        PatientService patientservice;
        DoctorService doctorservice;
        public AppointmentController(AppointmentService service, PatientService patientservice, DoctorService doctorservice)
        {
            this.service = service;
            this.patientservice = patientservice;
            this.doctorservice = doctorservice;
        }

        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("RoleId") != null;
        }


        private void LoadDropdowns()
        {
            var patients = patientservice.GetAll();
            var doctors = doctorservice.Get();

            ViewBag.Patients = new SelectList(patients, "Id", "Name");
            ViewBag.Doctors = new SelectList(doctors, "Id", "Name");
            ViewBag.StatusList = new List<string>()
            {
                "Pending",
                "Confirmed",
                "Completed",
                "Cancel"
            };
        }


        public IActionResult Index(string search)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            if (!string.IsNullOrEmpty(search))
            {
                var searched = service.Search(search);

                return View(searched);
            }

            var data = service.GetAll();

            return View(data);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = service.GetAll();
            return View(data);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var data = service.GetById(id);
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }
            LoadDropdowns();
            return View();
        }

        [HttpPost]
        public IActionResult Create(AppointmentDTO a)
        {
            //if (!IsLoggedIn())
            //{
            //    return RedirectToAction("Login", "Auth");
            //}

            if (ModelState.IsValid)
            {
                a.CreatedBy = HttpContext.Session.GetInt32("userid");

                var res = service.Create(a);
                if(res.Success)
                {
                    TempData["Msg"] = "Appoinment created succesfull";
                    return RedirectToAction("Index");
                }
                TempData["Msg"] = res.Message;
            }
            LoadDropdowns();
            return View(a);
           
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Index", "Auth");
            }

            var data = service.GetById(id);
            return View(data);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = service.GetById(id);
            LoadDropdowns();
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(AppointmentDTO a)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                var res = service.Update(a);
                if (res)
                {
                    TempData["Msg"] = "Appointment Updated Successfully";
                    return RedirectToAction("Index");
                }
            }

            LoadDropdowns();
            return View(a);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = service.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(int id, string Decision)
        {
            if (Decision.Equals("Yes"))
            {
                var res = service.Delete(id);
                if (res)
                {
                    TempData["Msg"] = "Appoinment deleted";
                    return RedirectToAction("Index");
                }
            }
            return View("Index");
        }

    }
}
