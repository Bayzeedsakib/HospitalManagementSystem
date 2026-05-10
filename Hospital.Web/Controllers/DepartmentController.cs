using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers
{
    public class DepartmentController : Controller
    {
        DoctorService service;
        public DepartmentController(DoctorService service)
        {
            this.service = service;
        }

        [HttpGet]
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
            return View(new DepartmentDTO());
        }

        [HttpPost]
        public IActionResult Create(DepartmentDTO d)
        {
            if (ModelState.IsValid)
            {
                var res = service.Create(d);
                if(res == true)
                {
                    TempData["Msg"] = "New department created";
                    return RedirectToAction("Index");
                } 
            }
            return View(d);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = service.GetById(id);
            return View(data);
        }

        [HttpPost]

        public IActionResult Edit(DepartmentDTO d)
        {
            if (ModelState.IsValid)
            {
                var res = service.Edit(d);

                if(res == true)
                {
                    TempData["Msg"] = "Department name edited";
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

        public IActionResult Delete(int id, string Decision)
        {
            if (Decision.Equals("Yes"))
            {
                var res = service.Delete(id);
                if(res == true)
                {
                    TempData["Msg"] = "Department deleted";
                    return RedirectToAction("Index");
                }
            }

            return View("Index");
        }
    }
}
