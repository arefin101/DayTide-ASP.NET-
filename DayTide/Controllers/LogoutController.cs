using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayTide.Controllers
{
    public class LogoutController : Controller
    {
        // GET: LogOut
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }

        public ActionResult LogoutUser()
        {
            Session.Clear();

            return RedirectToAction("Index", "Home");
        }

    }
}