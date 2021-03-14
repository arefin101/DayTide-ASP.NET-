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



    }
}

