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
    public class LoginController : Controller
    {
        LoginRepository loginRepository = new LoginRepository();
        AdminRepository adminRepository = new AdminRepository();
        
        [HttpGet]
        public ActionResult Login()
        {
            return View("Index");
        }
       [HttpPost]
       public ActionResult  Login(User user)
        {

            User usr= loginRepository.GetUserById(user.UserId);

            string userid = user.UserId;
            Debug.WriteLine(usr);
            if (usr != null && usr.Password == user.Password)
            {
                Session["UserId"] = user.UserId;
                Session["type"] = usr.Type;

                if (usr.Type == "Admin")
                {
                    Admin admin = adminRepository.GetUserById(user.UserId);
                    Session["name"] = admin.Name;
                      Session["UserId"] = user.UserId;
                       Session["type"] = usr.Type;
                    return RedirectToAction("Index", "Admin");
                }
                else if (usr.Type == "Moderator")
                {
                    Session["UserId"] = usr.UserId;
                    Session["UserType"] = "Moderator";
                    return RedirectToAction("Index" , "Moderator");
                }
                else if (usr.Type == "Customer")
                {
                    Session["userid"] = userid;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session["UserId"] = usr.UserId;
                    return RedirectToAction("Index", "DeliveryMan");
                }
                    

            }
            else
                 return RedirectToAction("Login");
        }
        //ActionName()
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }
    }
}