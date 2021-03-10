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
    public class ModeratorController : Controller
    {
        ModeratorRepository moderatorRepository = new ModeratorRepository();
        CategoryRepository categoryRepository = new CategoryRepository();
        ProductRepository productRepository = new ProductRepository();

        public ActionResult Index()
        {
            return View(moderatorRepository.GetAll());
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category categroy)
        {
            categoryRepository.Insert(categroy);
            return RedirectToAction("CustomizeCategory", "Moderator");
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
            categoryRepository.Delete(id);
            return RedirectToAction("CustomizeCategory", "Moderator");
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
            string path = Server.MapPath("~/Content/Products");
            string filename = Path.GetFileName(Picture.FileName);
            string fullpath = Path.Combine(path, filename);
            Picture.SaveAs(fullpath);

            product.Picture = filename;

            productRepository.Insert(product);

            return RedirectToAction("CustomizeProduct", "Moderator");
        }
        [HttpGet]
        public ActionResult CustomizeProduct()
        {
            ViewData["categories"] = categoryRepository.GetAll();
            return View(productRepository.GetAll());
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
                product.Picture = productRepository.GetProductById(id).Picture;
                product.ProductId = id;
                productRepository.Update(product);
                return RedirectToAction("CustomizeProduct", "Moderator");
            }
            else if(Picture != null)
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
            ViewData["CategoryName"] = categoryRepository.GetCategoryById(1).CategoryName.ToString();
            return View("aru");
            //return View(productRepository.GetProductById(id));
        }
        [HttpPost, ActionName("DeleteProduct")]
        public ActionResult ConfirmProductDelete(int id)
        {
            categoryRepository.Delete(id);
            return RedirectToAction("CustomizeCategory", "Moderator");
        }
    }
}