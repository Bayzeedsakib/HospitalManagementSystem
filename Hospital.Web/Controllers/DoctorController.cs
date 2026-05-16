using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Web.Controllers
{
    public class DoctorController : Controller
    {
        DoctorService service;
        DepartmentService Service;
        public DoctorController(DoctorService service, DepartmentService Service)
        {
            this.service = service;
            this.Service = Service;
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
        public IActionResult GetById(int id)
        {
            ViewBag.Departments = new SelectList(Service.GetAll(), "Id", "Name");
            var data = service.GetById(id);
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(Service.GetAll(), "Id","Name");
            return View(new DoctorDTO()); 
        }

        [HttpPost]
        public IActionResult Create(DoctorDTO d)
        {
            if (ModelState.IsValid)
            {
                var res = service.Create(d);
                if(res == true)
                {
                    TempData["Msg"] = "New doctor added successfully";
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
