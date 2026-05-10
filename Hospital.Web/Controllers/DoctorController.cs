using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers
{
    public class DoctorController : Controller
    {
        DoctorService service;
        public DoctorController(DoctorService service)
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
        public IActionResult Create()
        {
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

            return View(d);
        }
    }
}
