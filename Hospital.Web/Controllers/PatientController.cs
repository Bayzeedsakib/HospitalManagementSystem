using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Hospital.Web.Controllers
{
    public class PatientController : Controller
    {
        PatientService service;
        public PatientController(PatientService service)
        {
            this.service = service;
        }

        public IActionResult Index(string search)
        {
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
            return View(new PatientDTO());
        }

        [HttpPost]
        public IActionResult Create(PatientDTO p)
        {
            if (ModelState.IsValid)
            {
                var res = service.Create(p);

                if(res == true)
                {
                    TempData["Msg"] = "Patient Added Successfully";
                    return RedirectToAction("Index");
                }
            }

            return View(p);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = service.GetById(id);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(PatientDTO p)
        {
            if (ModelState.IsValid)
            {
                var res = service.Edit(p);
                if(res == true)
                {
                    TempData["Msg"] = "Patient data edited successfully";
                    return RedirectToAction("Index");
                }
            }

            return View();
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
                    TempData["Msg"] = "Deleted successfully";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
      
    }
}
