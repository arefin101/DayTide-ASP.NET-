using DayTide.Models;
using DayTide.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayTide.Controllers
{
    public class HomeController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        CategoryRepository categoryRepository = new CategoryRepository();

        protected DayTideEntities context = new DayTideEntities();
        public ActionResult FirstPage()
        {
            return View();
        }

        public ActionResult Index()
        {
            TempData["categotyProd"] =  categoryRepository.GetAll();
            return View(productRepository.GetAll());
        }
        [HttpGet]
        public ActionResult Create()
        {
            TempData["categotyProd"] = categoryRepository.GetAll();
            ViewBag.abc = categoryRepository.GetAll();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            TempData["categotyProd"] = categoryRepository.GetAll();
            productRepository.Insert(product);
            return View();
        }

        public ActionResult OnlyView(int id)
        {
            TempData["categotyProd"] = categoryRepository.GetAll();
            return View(productRepository.GetCategoryById(id));
        }

        [HttpGet]
        public ActionResult FullView(int id)
        {
            TempData["categotyProd"] = categoryRepository.GetAll();

            if (Session["userid"]==null)
            {
                return RedirectToAction("Index");
            }
            
            return View(productRepository.GetCategoryById(id));
        }

        [HttpPost]
        public ActionResult CategotySort(FormCollection formCollection)
        {
            int categoryId = Convert.ToInt32(formCollection["categoryId"]);

            

            return View(productRepository.GetProductByCateg(categoryId));
        }


        public ActionResult PriceSort(FormCollection formCollection)
        {

            return View(productRepository.GetProductByPrice(2));
        }
        [HttpGet]
        public ActionResult Mysearch(string id)
        {
            var key = Request["key"];

            

            return View(productRepository.GetProductSearch(key));
        }
        public ActionResult SearchResult(int id)
        {
            
            return View(productRepository.GetProductById(id));
        }

        public ActionResult OrderDetails()
        {
            string cusId = Session["userid"].ToString();

            var Orderlist = from c in context.CartBackups
                            join o in context.OrderRequests
                            on c.OrderId equals o.OrderId
                            where c.CustomerId == o.CustomerId && c.CustomerId == cusId
                            select new OrderDetailsViewModel()
                            { ID = c.ID, Address = o.Address, Amount = o.Amount, CustomerId = c.CustomerId, Date = o.Date, District = o.District, OrderId = o.OrderId, Payment_Type = o.Payment_Type, Price = c.Price, ProductId = c.ProductId, Quantiry = c.Quantiry };


            var orderIDs = Orderlist.GroupBy(x => x.OrderId).Select(g => g.FirstOrDefault()).ToList();

            TempData["OrderDetailsDemo1"] = Orderlist.ToList();
            TempData["ABCD"] = orderIDs;
            //TempData["OrderDetailsDemo1"] = Orderlist.Where(y => y.OrderId == 1022).ToList();


            return View();
        }


        public ActionResult OrderDetailsFull(int id)
        {
            string cusId = Session["userid"].ToString();


            var Orderlist = from c in context.CartBackups
                            from o in context.OrderRequests
                            from p in context.Products

                            where c.CustomerId == o.CustomerId && c.CustomerId == cusId && o.OrderId == id && c.OrderId == o.OrderId && c.ProductId == p.ProductId
                            select new OrderDetailsViewModel()
                            { ID = c.ID, Address = o.Address, Amount = o.Amount, CustomerId = c.CustomerId, Date = o.Date, District = o.District, OrderId = o.OrderId, Payment_Type = o.Payment_Type, Price = c.Price, ProductId = c.ProductId, Quantiry = c.Quantiry, ProductName = p.ProductName };

            TempData["TryOrder"] = Orderlist.ToList();
            //var orderIDs = Orderlist.GroupBy(x => x.OrderId).Select(g => g.FirstOrDefault()).ToList();

            return View();
        }



    }
}

