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

        [HttpGet]
        public IActionResult Create()
        {
            return View(new PatientDTO());
        }

        [HttpGet]
        public IActionResult Index()
        {
            var data = service.GetAll();
            return View(data);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = service.GetAll();
            return View(data);
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
      
    }
}
