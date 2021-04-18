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
    public class AdminController : BaseAdminController
    {

        Delevary_Man_RatingRepository delevary_Man_RatingRepository = new Delevary_Man_RatingRepository();
        AdminRepository adminRepository = new AdminRepository();
        ModeratorRepository moderatorRepository = new ModeratorRepository();
        CustomerRepository customerrRepository = new CustomerRepository();
        DeleveryManRepository delmanRepository = new DeleveryManRepository();
        UserRepository userRepository = new UserRepository();
        ApplicationRepository applicationRepository = new ApplicationRepository();
        NoticeRepository noticeRepository = new NoticeRepository();
        Order_DetailRepository order_detailRepo = new Order_DetailRepository();
        OrderRequestRepository orderreqRepo = new OrderRequestRepository();
        ProductRepository productRepository = new ProductRepository();
        User usr = new User();
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            
            ViewBag.adminCount = adminRepository.GetAll().Count;
            ViewBag.ModeratorCount = moderatorRepository.GetAll().Count;
            ViewBag.DelmanCount = delmanRepository.GetAll().Count;
            ViewBag.cusCount = customerrRepository.GetAll().Count;
            ViewBag.BlkUserCount = userRepository.GetBlockedUser().Count;
            ViewBag.Pensign = userRepository.GetUserSignUpReq().Count;
            return View("AdminHome");
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
            notice.Status = "Unread";
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
            notice.Status = "Unread";
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
            notice.Status = "Unread";
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
            notice.Status = "Unread";
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
        public ActionResult viewApplication()
        {
            return View(applicationRepository.GetAll());
        }
        [HttpGet]
        public ActionResult applicationReject(int id )
        {
            Application app=applicationRepository.GetApplicationById(id);
            app.Status = "Rejected";
            app.Accepted_RejectedBy = Session["UserId"].ToString();
            applicationRepository.Update(app);
            Notice notice = new Notice();
            notice.Massage = "Your Application Is Rejected For Some Internal Issue Please Contuct Admin panel For More Detail";
            notice.Send_For = app.SentBy;
            notice.Send_by = Session["UserId"].ToString();
            notice.Status = "Unread";
            noticeRepository.Insert(notice);
            return RedirectToAction("ViewApplication", "Admin");

        }
        [HttpGet]
        public ActionResult applicationAccept(int id)
        {
            Application app = applicationRepository.GetApplicationById(id);
            app.Status = "Accepted";
            app.Accepted_RejectedBy= Session["UserId"].ToString();
            applicationRepository.Update(app);
            Notice notice = new Notice();
            notice.Massage = "Your Application Is Accepted By Our Admin Panel";
            notice.Send_For = app.SentBy;
            notice.Send_by = Session["UserId"].ToString();
            notice.Status = "Unread";
            noticeRepository.Insert(notice);
            return RedirectToAction("ViewApplication", "Admin");

        }
        [HttpGet]
        public ActionResult applicationDetail(int id)
        {
            return View(applicationRepository.GetApplicationById(id));
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
        public ActionResult viewFullMassege(int id)
        {
            Notice notice = noticeRepository.GetNoticeById(id);
            notice.Status = "read";
            noticeRepository.Update(notice);
            return View(notice);
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
        public ActionResult DetailModerator(string id)
        {
            return View(moderatorRepository.GetUserById(id));

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
        public ActionResult updatesalmod(string id)
        {
            return View(moderatorRepository.GetUserById(id));
        }
        [HttpPost]
        public ActionResult updatesalmod(Moderator moderator)
        {
            moderatorRepository.Update(moderator);
            Notice notice = new Notice();
            notice.Massage = "Your Salary Has been Changed by Admin/Moderator Panel";
            notice.Send_For = moderator.ModeratorId;
            notice.Send_by = Session["UserId"].ToString();
            notice.Status = "Unread";
            noticeRepository.Insert(notice);
            return RedirectToAction("ModeratorList", "Admin");
        }
        [HttpGet]
        public ActionResult DetailDelman(string id)
        {
           List< Delevary_Man_Rating >delmanratinhg= delevary_Man_RatingRepository.GetDeleveryMenRatingById(id);
            ViewBag.comments = delmanratinhg;
            int count = delevary_Man_RatingRepository.GetDeleveryMenRatingById(id).Count;
            int  ratingCount = 0;
            if (count != 0)
            {
                foreach (var v in delmanratinhg)
                {
                    ratingCount = ratingCount + Convert.ToInt32(v.Rating);
                }
                float finalrating = (ratingCount / count);
                ViewBag.Rating = Math.Ceiling(finalrating);
            }
            else
                ViewBag.Rating = 0;
            return View(delmanRepository.GetUserById(id));
           
        }
        [HttpGet]
        public ActionResult Blockdel(string id)
        {
            User usr = new User();
            usr = userRepository.GetUserById(id);
            usr.Status = "invalid";
            userRepository.Update(usr);
            return RedirectToAction("DeleveryManList", "Admin");

        }
        [HttpGet]
        public ActionResult UnBlockdel(string id)
        {
            User usr = new User();
            usr = userRepository.GetUserById(id);
            usr.Status = "valid";
            userRepository.Update(usr);
            return RedirectToAction("DeleveryManList", "Admin");

        }
        [HttpGet]
        public ActionResult updatesalDeletedelman(string id)
        {
            return View(delmanRepository.GetUserById(id));
        }
        [HttpPost]
        public ActionResult updatesalDeletedelman(DeleveryMan delman)
        {
            delmanRepository.Update(delman);
            Notice notice = new Notice();
            notice.Massage = "Your Salary Has been Changed by Admin/Moderator Panel";
            notice.Send_For = delman.DelManId;
            notice.Send_by = Session["UserId"].ToString();
            notice.Status = "Unread";
            noticeRepository.Insert(notice);
            return RedirectToAction("DeleveryManList", "Admin");
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
        public ActionResult Adprofile()
        {
            return View(adminRepository.GetUserById(Session["UserId"].ToString()));
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
            else ViewBag.errmsg = "Invalid UserID";
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
            else ViewBag.errmsg = "Invalid UserID";
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
                return RedirectToAction("Adprofile", "Admin");
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

                return RedirectToAction("AdProfile", "Admin");
            }
            else
                return RedirectToAction("AdProfile", "Admin");
        }



    }
}