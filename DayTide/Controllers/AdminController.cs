using DayTide.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayTide.Controllers
{
    public class AdminController : Controller
    {
        AdminRepository adminRepository = new AdminRepository();
        // GET: Admin
        public ActionResult Index()
        {
            return View(adminRepository.GetAll());
        }
    }
}