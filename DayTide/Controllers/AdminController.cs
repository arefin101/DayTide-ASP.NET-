using DayTide.Models;
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
        ModeratorRepository moderatorRepository = new ModeratorRepository();
        CustomerRepository customerrRepository = new CustomerRepository();
        DeleveryManRepository delmanRepository = new DeleveryManRepository();
        UserRepository userRepository = new UserRepository();
        //PendingSignupRepository pensignupRepo = new PendingSignupRepository();
        NoticeRepository noticeRepository = new NoticeRepository();
        User usr = new User();
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            return View("AdminHome", adminRepository.GetAll());
        }
        [HttpGet]
        public ActionResult AdminList()
        {
            return View(adminRepository.GetAll());
        }
        [HttpGet]
        public ActionResult CustomerList()
        {
            return View(customerrRepository.GetAll());
        }
        [HttpGet]
        public ActionResult Notifyad(string id)
        {
            Notice notice = new Notice();
            notice.Send_For = id;
            notice.Send_by = Session["UserId"].ToString();
            ViewBag.ids = notice;
            return View("Notify");

        }
        [HttpPost]
        public ActionResult Notifyad(Notice notice)
        {
            noticeRepository.Insert(notice);
            return RedirectToAction("AdminList", "Admin");

        }
        [HttpGet]
        public ActionResult Notifymod(string id)
        {
            Notice notice = new Notice();
            notice.Send_For = id;
            notice.Send_by = Session["UserId"].ToString();
            ViewBag.ids = notice;
            return View("Notify");

        }
        [HttpPost]
        public ActionResult Notifymod(Notice notice)
        {
            noticeRepository.Insert(notice);
            return RedirectToAction("ModeratorList", "Admin");

        }
        [HttpGet]
        public ActionResult Notifydelman(string id)
        {
            Notice notice = new Notice();
            notice.Send_For = id;
            notice.Send_by = Session["UserId"].ToString();
            ViewBag.ids = notice;
            return View("Notify");

        }
        [HttpPost]
        public ActionResult Notifydelman(Notice notice)
        {
            noticeRepository.Insert(notice);
            return RedirectToAction("DeleveryManList", "Admin");

        }
        [HttpGet]
        public ActionResult ModeratorList()
        {
            return View(moderatorRepository.GetAll());
        }
        [HttpGet]
        public ActionResult DeleveryManList()
        {
            return View(delmanRepository.GetAll());
        }
        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            if (userRepository.GetUserById(admin.AdminId) == null)
            {
                User usr = new User();
                usr.UserId = admin.AdminId;
                usr.Password = "1";
                usr.Type = "Admin";
                usr.Status = "valid";
                userRepository.Insert(usr);
                adminRepository.Insert(admin);
                return RedirectToAction("AdminList", "Admin");
            }
            else ViewBag.errmsg = "Invalid USerID";
            return View(admin);

        }
        [HttpGet]
        public ActionResult AddModerator()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddModerator(Moderator moderator)
        {
            if (userRepository.GetUserById(moderator.ModeratorId) == null)
            {
                User usr = new User();
                usr.UserId = moderator.ModeratorId;
                usr.Password = "1";
                usr.Type = "Moderator";
                usr.Status = "valid";
                userRepository.Insert(usr);
                moderatorRepository.Insert(moderator);
                return RedirectToAction("ModeratorList", "Admin");
            }
            else ViewBag.errmsg = "Invalid USerID";
            return View(moderator);
        }
        [HttpGet]
        public ActionResult DelMan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDelMan(DeleveryMan delman)
        {
            if (userRepository.GetUserById(delman.DelManId) == null)
            {
                User usr = new User();
                usr.UserId = delman.DelManId;
                usr.Password = "1";
                usr.Type = "Delivery Man";
                usr.Status = "valid";
                userRepository.Insert(usr);
                delmanRepository.Insert(delman);
                return RedirectToAction("DeleveryManList", "Admin");
            }
            else ViewBag.errmsg = "Invalid USerID";
            return View(delman);

        }

       

    }
}