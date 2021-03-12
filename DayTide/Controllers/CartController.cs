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
          
            

            string cid = cart.CustomerId;

            cartRepository.Insert(cart);

            var products = productRepository.GetProductById(cart.ProductId);
            products.Quantity -= cart.Quantiry;

            productRepository.Update(products);
            double totalPrice = 0.0;

            var multi = cartRepository.GetCartById(cid);
            foreach(var item in multi)
            {
                totalPrice += Convert.ToDouble((item.Price_unit_ * item.Quantiry));
            }

            TempData["totalPrice"] = totalPrice;


            TempData["customerIdData"] = cartRepository.GetCartById(cid);
            //  TempData["customerIdData"] = cid;

            //return RedirectToAction("ShowCartData","Cart");

            return View();

        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {

            return View(cartRepository.GetProductById(id));
        }
        [HttpPost]
        public ActionResult Edit(int id, Cart cart)
        {
            cartRepository.Update(cart);

            return RedirectToAction("Index");
            //return View(cartRepository.GetProductById(id));
        }
    }
}