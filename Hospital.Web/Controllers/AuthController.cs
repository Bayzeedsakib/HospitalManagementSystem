using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Hospital.Web.Controllers
{
    public class AuthController : Controller
    {
        UserService userservice;
        public AuthController(UserService userservice)
        {
            this.userservice = userservice;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDTO u)
        {
            var user = userservice.Get(u.Email, u.Password);

            if(user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);

                HttpContext.Session.SetString("username", user.Name);

                HttpContext.Session.SetInt32("RoleId", user.RoleId);

                if (user.RoleId == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                     

                if (user.RoleId == 2)
                {
                    return RedirectToAction("DoctorDashboard", "Doctor");
                }
                     

                if (user.RoleId == 3)
                {
                    return RedirectToAction("Index", "Appointment");
                }
                    
                return RedirectToAction("Index", "Home");
            }
            TempData["Class"] = "danger";
            TempData["Msg"] = "Invalid Email or Password";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
