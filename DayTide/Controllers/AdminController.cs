using DayTide.Models;
using DayTide.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        Order_DetailRepository order_detailRepo = new Order_DetailRepository();
        OrderRequestRepository orderreqRepo = new OrderRequestRepository();
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
        public ActionResult Notifycus(string id)
        {
            Notice notice = new Notice();
            notice.Send_For = id;
            notice.Send_by = Session["UserId"].ToString();
            ViewBag.ids = notice;
            return View("Notify");

        }
        [HttpPost]
        public ActionResult Notifycus(Notice notice)
        {
            noticeRepository.Insert(notice);
            return RedirectToAction("CustomerList", "Admin");

        }
        [HttpGet]
        public ActionResult Detailscus(string id)
        {
            return View(customerrRepository.GetUserById(id));
        }
        [HttpGet]
        public ActionResult OrderDetailcus(string id)
        {
            return View(order_detailRepo.GetOrderDetailByUsertId(id));
        }

        [HttpGet]
        public ActionResult Mynotification()
        {
            return View(noticeRepository.GetNoticeByIdSend_For(Session["UserId"].ToString()));
        }
        [HttpGet]
        public ActionResult EditNotice(int id)
        {
            return View(noticeRepository.GetNoticeById(id));
        }
        [HttpPost]
        public ActionResult EditNotice(Notice notice)
        {
            noticeRepository.Update(notice);
            return RedirectToAction("PostedNotification", "Admin");
        }
        [HttpGet]
        public ActionResult DeleteNotice(int id)
        {
            noticeRepository.Delete(id);
            return RedirectToAction("PostedNotification", "Admin");
        }
        [HttpGet]
        public ActionResult PostedNotification()
        {
            return View(noticeRepository.GetNoticeByIdSend_by(Session["UserId"].ToString()));
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
        public ActionResult OrderRequest()
        {
            return View(orderreqRepo.GetAll());
        }
        [HttpGet]
        public ActionResult Editdelreq(int id)
        {
            OrderRequest ordrreq = new OrderRequest();
            ordrreq = orderreqRepo.GetOrderRequestById(id);

            ViewBag.delman = delmanRepository.GetDeleveryMenByAdd(ordrreq.District);

            return View(orderreqRepo.GetOrderRequestById(id));
        }
        [HttpPost]
        public ActionResult Editdelreq(OrderRequest orderreq, string DelManId)
        {
            Order_Detail order_detail = new Order_Detail();
            order_detail.OrderId = orderreq.OrderId;
            order_detail.Date = orderreq.Date;
            order_detail.Address = orderreq.Address;
            order_detail.District = orderreq.District;
            order_detail.Amount = orderreq.Amount;
            order_detail.Payment_Type = orderreq.Payment_Type;
            order_detail.CustomerId = orderreq.CustomerId;
            order_detail.Status = "Processing";
            order_detail.DelManId = DelManId;
            OrderRequest ordrreq = new OrderRequest();
            order_detailRepo.Insert(order_detail);
            orderreqRepo.Delete(orderreq.OrderId);

            return RedirectToAction("OrderHistory", "Admin");
        }
        [HttpGet]
        public ActionResult Deletedelreq(int id)
        {

            orderreqRepo.Delete(id);
            return RedirectToAction("OrderRequest", "Admin");

        }
        [HttpGet]
        public ActionResult OrderHistory()
        {

            return View(order_detailRepo.GetAll());

        }
        [HttpGet]
        public ActionResult Blockmod(string id)
        {
            User usr = new User();
            usr = userRepository.GetUserById(id);
            usr.Status = "invalid";
            userRepository.Update(usr);
            return RedirectToAction("ModeratorList", "Admin");

        }
        [HttpGet]
        public ActionResult UnBlockmod(string id)
        {
            User usr = new User();
            usr = userRepository.GetUserById(id);
            usr.Status = "valid";
            userRepository.Update(usr);
            return RedirectToAction("ModeratorList", "Admin");

        }
        [HttpGet]
        public ActionResult Blockdel(string id)
        {
            User usr = new User();
            usr = userRepository.GetUserById(id);
            usr.Status = "invalid";
            userRepository.Update(usr);
            return RedirectToAction("ModeratorList", "Admin");

        }
        [HttpGet]
        public ActionResult UnBlockdel(string id)
        {
            User usr = new User();
            usr = userRepository.GetUserById(id);
            usr.Status = "valid";
            userRepository.Update(usr);
            return RedirectToAction("ModeratorList", "Admin");

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
        public ActionResult Deletemod(string id)
        {
            moderatorRepository.DeleteUser(id);
            userRepository.DeleteUser(id);
            return RedirectToAction("ModeratorList", "Admin");
        }
        [HttpGet]
        public ActionResult Deletedelman(string id)
        {
            delmanRepository.DeleteUser(id);
            userRepository.DeleteUser(id);
            return RedirectToAction("ModeratorList", "Admin");
        }
        [HttpGet]
        public ActionResult Adprofile(string id)
        {
            return View(adminRepository.GetUserById(id));
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
        public ActionResult AddDelMan()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DelManReq ()
        {
            return View(userRepository.GetUserSignUpReq());
        }
        [HttpGet]
        public ActionResult DeleteDelSignup(string id)
        {
            delmanRepository.DeleteUser(id);
            userRepository.DeleteUser(id);
            return RedirectToAction("DelManReq","Admin");
        }
        [HttpGet]
        public ActionResult EditDelSignup(string id)
        {
            return View(delmanRepository.GetUserById(id));
        }
        [HttpPost]
        public ActionResult EditDelSignup(User user ,DeleveryMan delman)
        {
            user.Status = "valid";
            delmanRepository.Update(delman);
            userRepository.Update(user);
            return RedirectToAction("DelManReq","Admin");
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
        [HttpGet]
        public ActionResult EditBio(string id)
        {
            return View(adminRepository.GetUserById(id));
        }
        [HttpPost]
        public ActionResult EditBio(Admin admin, HttpPostedFileBase Picture)
        {
            if (Picture == null)
            {
                Session["Name"] = admin.Name;
                adminRepository.Update(admin);
                return RedirectToAction("Adprofile", "Admin", Session["UserId"]);
            }
            else if (Picture != null)
            {
                string path = Server.MapPath("~/Content/Users");
                string filename = Path.GetFileName(Picture.FileName);
                string fullpath = Path.Combine(path, filename);
                Picture.SaveAs(fullpath);

                admin.Picture = filename;

                Session["Name"] = admin.Name;
                Session["Picture"] = filename;

                adminRepository.Update(admin);

                return RedirectToAction("AdProfile", "Admin", Session["UserId"]);
            }
            else
                return RedirectToAction("AdProfile", "Admin", Session["UserId"]);
        }



    }
}