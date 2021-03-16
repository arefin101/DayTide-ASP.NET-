using DayTide.Models;
using DayTide.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayTide.Controllers
{
    public class ChangePasswordController : Controller
    {
        UserRepository userRepo = new UserRepository();
        // GET: ChangePassword
        [HttpGet]
        public ActionResult Index()
        {
            return View(userRepo.GetUserById(Session["UserId"].ToString()));
        }
        [HttpPost]
        public ActionResult Index(User user ,string newpass)
        {
            Debug.WriteLine(user);
            user.Password = newpass;
            userRepo.Update(user);
            Debug.WriteLine(user);
            return RedirectToAction("Login","Login");
        }

    }
}