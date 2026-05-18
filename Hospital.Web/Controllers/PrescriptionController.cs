using AutoMapper;
using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Web.Controllers
{
    public class PrescriptionController : Controller
    {
        PrescriptionService service;
        public PrescriptionController(PrescriptionService service)
        {
            this.service = service;
        }

        private void LoadDropdowns()
        {
            var appointments = service.GetAll();

            ViewBag.Appointments = new SelectList(
                appointments,
                "Id",
                "PatientName"
            );
        }

        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("role") != null;
        }

        public IActionResult Index(string search)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            //if (!string.IsNullOrEmpty(search))
            //{
            //    var searched = PrescriptionService.Search(search);

            //    return View(searched);
            //}

            var data = service.GetAll();

            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        [HttpPost]
        public IActionResult Create(PrescriptionDTO p)
        {
            if (ModelState.IsValid)
            {
                var result = service.Create(p);

                if (result.Success)
                {
                    TempData["Msg"] = "Prescription created succesfull";
                    return RedirectToAction("Index");
                }

                ViewBag.Message = result;
            }

            LoadDropdowns();
            return View(p);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = service.Get(id);

            if (data == null)
            {
                return RedirectToAction("Index");
            }

            LoadDropdowns();
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(PrescriptionDTO dto)
        {
            if (ModelState.IsValid)
            {
                bool rs = service.Update(dto);

                if (rs)
                {
                    return RedirectToAction("Index");
                }
            }

            LoadDropdowns();
            return View(dto);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Index", "Auth");
            }

            var data = service.Get(id);
            return View(data);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = service.Get(id);
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
