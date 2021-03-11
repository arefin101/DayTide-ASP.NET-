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
        CartRepository cartRepository = new CartRepository();
        List<Product> productCartList = new List<Product>();

        //string cid = "";

        [HttpPost]
        public ActionResult Index(Cart cart)
        {
          
            cartRepository.Insert(cart);

            string cid = cart.CustomerId;
         

            var products = productRepository.GetProductById(cart.ProductId);
            products.Quantity -= cart.Quantiry;

            productRepository.Update(products);




            return View(cartRepository.GetCartById(cid));
        }
        //[HttpGet]
        //public ActionResult ShowCartData(string cid)
        //{

        //    return View(cartRepository.GetCartById(cid));
        //}
        //[HttpPost]
        //public ActionResult ShowCartData()
        //{

        //    return View();
        //}
    }
}