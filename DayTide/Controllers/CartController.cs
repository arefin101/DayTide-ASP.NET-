using DayTide.Models;
using DayTide.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayTide.Controllers
{
    public class CartController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        List<Product> productCartList = new List<Product>();
        [HttpGet]
        public ActionResult Index(int id)
        {
            productCartList.Add(productRepository.GetProductById(id));

            return View(productCartList);
        }
    }
}