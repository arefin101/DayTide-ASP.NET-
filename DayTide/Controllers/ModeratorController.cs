using DayTide.Models;
using DayTide.Repositories;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayTide.Controllers
{
    public class ModeratorController : BaseController
    {
        protected DayTideEntities3 context1 = new DayTideEntities3();
        UserRepository userRepository = new UserRepository(); 
        ModeratorRepository moderatorRepository = new ModeratorRepository();
        CategoryRepository categoryRepository = new CategoryRepository();
        ProductRepository productRepository = new ProductRepository();
        CartRepository cartRepository = new CartRepository();
        Product context = new Product();
        DeleveryManRepository deleveryManRepository = new DeleveryManRepository();
        NoticeRepository noticeRepository = new NoticeRepository();

        public ActionResult Index()
        {
            Moderator moderator = moderatorRepository.GetUserById(Convert.ToString(Session["UserId"]));

            Session["Name"] = moderator.Name;
            Session["Picture"] = moderator.Picture;

            return View();
        }
        [HttpGet]
        public ActionResult CustomizeDeliveryMan()
        {
            ViewData["status"] = userRepository.GetAll();
            return View(deleveryManRepository.GetAll());
        }
        [HttpGet]
        public ActionResult EditDeliveryMan(string id)
        {
            return View(deleveryManRepository.GetUserById(id));
        }
        [HttpPost]
        public ActionResult EditDeliveryMan(string id, DeleveryMan deleveryMan)
        {
            deleveryMan.DelManId = id;
            deleveryManRepository.Update(deleveryMan);
            return RedirectToAction("CustomizeDeliveryMan", "Moderator");
        }
        [HttpGet]
        public ActionResult CreateDeliveryMan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDeliveryMan(DeleveryMan deleveryMan)
        {
            DeleveryMan DelMan = deleveryManRepository.GetAll().Where(x=>x.DelManId == deleveryMan.DelManId).FirstOrDefault();

            if (DelMan == null && ModelState.IsValid)
            {
                User user = new User();
                user.UserId = deleveryMan.DelManId;
                user.Type = "DeliveryMan";
                user.Password = "1";
                user.Status = "valid";
                userRepository.Insert(user);

                deleveryMan.Complete_Task = 0;
                deleveryMan.Picture = "default.jpg";
                deleveryManRepository.Insert(deleveryMan);
                return RedirectToAction("CustomizeDeliveryMan", "Moderator");
            }
            else
            {
                if(DelMan != null)
                {
                    TempData["UserError"] = "This User Name Is Allready Used";
                }
                return RedirectToAction("CreateDeliveryMan", "Moderator");
            }
            
        }
        [HttpGet]
        public ActionResult DetailDeliveryMan(string id)
        {
            return View(deleveryManRepository.GetUserById(id));
        }
        [HttpGet]
        public ActionResult DeleteDeliveryMan(string id)
        {
            return View(deleveryManRepository.GetUserById(id));
        }
        [HttpPost, ActionName("DeleteDeliveryMan")]
        public ActionResult DeleteDelMan(string id)
        {
            deleveryManRepository.DeleteUser(id);
            userRepository.DeleteUser(id);
            return RedirectToAction("CustomizeDeliveryMan", "Moderator");
        }

        public ActionResult DelManStatus(string id)
        {
            User usr = new User();
            User user = new User();

            user = context1.Users.Where(x =>x.UserId == id).FirstOrDefault();

            if(user.Status == "valid")
            {
                usr = userRepository.GetUserById(id);
                usr.Status = "invalid";
                userRepository.Update(usr);

                return RedirectToAction("CustomizeDeliveryMan", "Moderator");
            }
            else
            {
                usr.UserId = id;
                usr.Password = user.Password;
                usr.Type = user.Type;
                usr.Status = "valid";

                userRepository.Update(usr);

                return RedirectToAction("CustomizeDeliveryMan", "Moderator");
            }

        }

        [HttpGet]
        public ActionResult Notifydelman(string id)
        {
            Notice notice = new Notice();
            notice.Send_For = id;
            notice.Send_by = Session["ModeratorId"].ToString();
            ViewBag.ids = notice;
            return View("Notify");

        }
        [HttpPost]
        public ActionResult Notifydelman(Notice notice)
        {
            noticeRepository.Insert(notice);
            return RedirectToAction("CustomizeDeliveryMan", "Moderator");
        }

        public ActionResult StockCheckProduct(int id)
        {
            Product product = new Product();
            product = productRepository.GetProductById(id);
            Product product1 = context1.Products.Where(x => x.ProductId == id).FirstOrDefault();

            if (product.Quantity > 0)
            {
                product.Quantity = (0 - product1.Quantity);
                productRepository.Update(product);
                return RedirectToAction("CustomizeProduct", "Moderator");
            }
            else
            {
                product.Quantity = (product1.Quantity - (2*product1.Quantity));
                productRepository.Update(product);
                return RedirectToAction("CustomizeProduct", "Moderator");
            }

        }


        [HttpGet]
        public ActionResult Profile(string id)
        {
            return View(moderatorRepository.GetUserById(id));
        }
        [HttpPost]
        public ActionResult Profile(string id, Moderator moderator, HttpPostedFileBase Picture)
        {
            if (Picture == null)
            {
                Session["Name"] = moderator.Name;
                moderator.ModeratorId = id;
                moderatorRepository.Update(moderator);
                return RedirectToAction("Profile", "Moderator");
            }
            else if (Picture != null)
            {
                string path = Server.MapPath("~/Content/Users");
                string filename = Path.GetFileName(Picture.FileName);
                string fullpath = Path.Combine(path, filename);
                Picture.SaveAs(fullpath);

                moderator.Picture = filename;

                Session["Name"] = moderator.Name;
                Session["Picture"] = filename;

                moderator.ModeratorId = id;

                moderatorRepository.Update(moderator);

                return RedirectToAction("Profile", "Moderator");
            }
            else
                return RedirectToAction("Profile", "Moderator");
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category categroy)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Insert(categroy);
                return RedirectToAction("CustomizeCategory", "Moderator");
            }
            return RedirectToAction("AddCategory", "Moderator");
        }
        [HttpGet]
        public ActionResult CustomizeCategory()
        {
            string categories = categoryRepository.GetAll().ToString();
            return View(categoryRepository.GetAll());
        }
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            return View(categoryRepository.GetCategoryById(id));
        }
        [HttpPost]
        public ActionResult EditCategory(Category category, int id)
        {
            category.CategoryId = id;
            categoryRepository.Update(category);
            return RedirectToAction("CustomizeCategory", "Moderator");
        }
        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            return View(categoryRepository.GetCategoryById(id));
        }
        [HttpPost, ActionName("DeleteCategory")]
        public ActionResult ConfirmCategoryDelete(int id)
        {
            if(productRepository.GetAll().Where(x => x.CategoryId == id).FirstOrDefault() == null)
            {
                categoryRepository.Delete(id);
                return RedirectToAction("CustomizeCategory", "Moderator");
            }
            TempData["Error"] = "This Category Is Being Used By Any Product";
            return RedirectToAction("DeleteCategory", "Moderator");
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            ViewData["categories"] = categoryRepository.GetAll();
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase Picture)
        {
            if(ModelState.IsValid && Picture != null)
            {
                string path = Server.MapPath("~/Content/Products");
                string filename = Path.GetFileName(Picture.FileName);
                string fullpath = Path.Combine(path, filename);
                Picture.SaveAs(fullpath);

                product.Picture = filename;

                productRepository.Insert(product);

                return RedirectToAction("CustomizeProduct", "Moderator");
            }
            ViewData["categories"] = categoryRepository.GetAll();
            TempData["File"] = "Picture Required";
            return View(product);
        }
        [HttpGet]
        public ActionResult CustomizeProduct()
        {
            ViewData["categories"] = categoryRepository.GetAll();
            return View(productRepository.GetAll());
        }
        [HttpGet]
        public ActionResult SeeProduct(int id)
        {
            ViewData["Category"] = categoryRepository.GetCategoryById(id);
            ViewData["categories"] = categoryRepository.GetAll();
            return View("CustomizeProduct", productRepository.GetAll().Where(x => x.CategoryId == id).ToList());
        }
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            ViewData["categories"] = categoryRepository.GetAll();
            return View(productRepository.GetCategoryById(id));
        }
        [HttpPost]
        public ActionResult EditProduct(Product product, HttpPostedFileBase Picture, int id)
        {
            if (Picture == null)
            {
                product.ProductId = id;
                productRepository.Update(product);
                return RedirectToAction("CustomizeProduct", "Moderator");
            }
            else if (Picture != null)
            {
                string path = Server.MapPath("~/Content/Products");
                string filename = Path.GetFileName(Picture.FileName);
                string fullpath = Path.Combine(path, filename);
                Picture.SaveAs(fullpath);

                product.Picture = filename;

                product.ProductId = id;

                productRepository.Update(product);

                return RedirectToAction("CustomizeProduct", "Moderator");
            }
            else
                return RedirectToAction("CustomizeProduct", "Moderator");
        }
        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            if (cartRepository.GetCartByProductId(id) == null)
            {
                return View(productRepository.GetProductById(id));
            }
            TempData["Error"] = "This Product Is On Service";
            return RedirectToAction("CustomizeProduct", "Moderator");
        }
        [HttpPost, ActionName("DeleteProduct")]
        public ActionResult ConfirmProductDelete(int id)
        {
            productRepository.DeleteProduct(id);
            return RedirectToAction("CustomizeProduct", "Moderator");
        }



        DayTideEntities3 db = new DayTideEntities3();
        public ActionResult Aru()
        {
            return View(db.DeleveryMen.ToList());
        }
        [HttpPost]
        public JsonResult GetSearchingData(string SearchBy, string SearchValue)
        {
            List<DeleveryMan> StuList = new List<DeleveryMan>();
            if (SearchBy == "ID")
            {
                try
                {
                    string Id = SearchValue;
                    StuList = db.DeleveryMen.Where(x => x.DelManId == Id || SearchValue == "").ToList();
                }
                catch (FormatException)
                {
                    Console.WriteLine("{0} Is Not A ID ", SearchValue);
                }
                return Json(StuList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                StuList = deleveryManRepository.GetAll().Where(x=>x.Name== SearchValue).ToList();
                return Json(StuList, JsonRequestBehavior.AllowGet);
            }
        }


    }
}