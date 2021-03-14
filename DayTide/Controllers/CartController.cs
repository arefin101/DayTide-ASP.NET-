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

            List<double> listPrice = new List<double>();


            var multi = cartRepository.GetCartById(cid);
            foreach(var item in multi)
            {
                totalPrice += Convert.ToDouble((item.Price_unit_ * item.Quantiry));

                listPrice.Add(Convert.ToDouble(item.Price_unit_));
            }

            TempData["totalPrice"] = totalPrice;

            listPrice.Sort();

            TempData["chartPrice"] = listPrice.ToList();
            TempData["tempo"] = listPrice.ToList();

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

        public ActionResult CustomerCart()
        {

            if (Session["userid"] == null)
            {
                return RedirectToAction("Index","Home");
            }

            string customeID = Session["userid"].ToString();

            TempData["customerIdData"] = cartRepository.GetCartById(customeID);

            TempData["chartPrice"] = TempData["tempo"];

            return View();
        }
    }
}