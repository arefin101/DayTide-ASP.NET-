using DayTide.Models;
using DayTide.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayTide.Controllers
{
    public class LoginController : Controller
    {
        LoginRepository loginRepository = new LoginRepository();
        
        [HttpGet]
        public ActionResult Login()
        {
            return View("Index");
        }
       [HttpPost]
       public ActionResult  Login(User user)
        {

            User usr= loginRepository.GetUserById(user.UserId);

            if (usr != null && usr.Password == user.Password)
            {
                if (usr.Type == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (usr.Type == "Moderator")
                {
                    return RedirectToAction("Index", "Moderator");
                }
                // else if (usr.Type == "Deleveryman")
                // {

                // }
                else
                    return RedirectToAction("Login");

            }
            else
                 return RedirectToAction("Login");
        }
        //ActionName()
    }
}