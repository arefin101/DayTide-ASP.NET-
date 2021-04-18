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
        protected DaytideEntities3 context = new DaytideEntities3();

        //string cid = "";

        [HttpPost]
        public ActionResult Index(Cart cart)
        {
            string cid = cart.CustomerId;


            var dbCart = cartRepository.GetCartById(cid);

            //string  dbCustomerID = dbCart.Select(x => x.CustomerId).ToString();

            List<int> dbProductsList = new List<int>();

            foreach(var pro in dbCart)
            {
                dbProductsList.Add(pro.ProductId);
            }

            bool anss=  dbProductsList.Contains(cart.ProductId);

            //List<Cart> editCart = new List<Cart>();

            if(anss == true)
            {
                var editCart =  cartRepository.GetCartByProdIDCusID(cart.ProductId, cid);

               //var toEdit = cart.Quantiry;

                editCart.Quantiry += cart.Quantiry;

                cartRepository.Update(editCart);

            }
            else
            {
                cartRepository.Insert(cart);
            }


            

            

            






            var products = productRepository.GetProductById(cart.ProductId);
            products.Quantity -= cart.Quantiry;

            productRepository.Update(products);
            double totalPrice = 0.0;

            List<double> listPrice = new List<double>();


            var multi = cartRepository.GetCartById(cid);
            foreach(var item in multi)
            {
                totalPrice += Convert.ToDouble(((item.Price_unit_ * item.Quantiry)+((item.Price_unit_ * item.Quantiry)*0.05)));

                listPrice.Add(Convert.ToDouble(item.Price_unit_));
            }

            TempData["totalPrice"] = totalPrice;

            listPrice.Sort();

            TempData["chartPrice"] = listPrice.ToList();
            TempData["tempo"] = listPrice.ToList();

            TempData["customerIdData"] = cartRepository.GetCartById(cid);
            //  TempData["customerIdData"] = cid;

            //return RedirectToAction("ShowCartData","Cart");

            var list = from c in context.Carts
                       join p in context.Products
                       on c.ProductId equals p.ProductId
                       where c.CustomerId == cid
                       select new CartViewModelDemo() { Id = c.Id, CustomerId = c.CustomerId, Quantiry = c.Quantiry, Price_unit_ = c.Price_unit_, ProductId = c.ProductId, ProductName = p.ProductName, Picture = p.Picture };
            TempData["testingitem"] = list.ToList();





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



            var multi = cartRepository.GetCartById(customeID);

            List<double> listPrice = new List<double>();
            double totalPrice = 0.0;
            foreach (var item in multi)
            {
                totalPrice += Convert.ToDouble(((item.Price_unit_ * item.Quantiry) + ((item.Price_unit_ * item.Quantiry) * 0.05)));

                listPrice.Add(Convert.ToDouble(item.Price_unit_));

            }

            listPrice.Sort();

            TempData["totalPrice"] = totalPrice;

            TempData["customerIdData"] = cartRepository.GetCartById(customeID);

            TempData["chartPrice"] = listPrice.ToList();

            //TempData["chartPrice"] = TempData["tempo"];


           // string customeID = "banik101";

            var list = from c in context.Carts
                       join p in context.Products
                       on c.ProductId equals p.ProductId
                       where c.CustomerId == customeID
                       select new CartViewModelDemo() {Id =c.Id , CustomerId = c.CustomerId, Quantiry = c.Quantiry, Price_unit_ = c.Price_unit_, ProductId = c.ProductId, ProductName = p.ProductName, Picture = p.Picture };
            TempData["testingitem"] = list.ToList();







            return View();
        }

        public ActionResult DemoPage()
        {


            //string customeID = Session["userid"].ToString();
            string customeID = "banik101";
            var list = from c in context.Carts
                       join p in context.Products
                       on c.ProductId equals p.ProductId
                       where c.CustomerId == customeID
                       select new CartViewModelDemo() {CustomerId= c.CustomerId, Quantiry= c.Quantiry, Price_unit_= c.Price_unit_,ProductId= c.ProductId,ProductName= p.ProductName, Picture= p.Picture };
            TempData["testingitem"] = list.ToList();
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(cartRepository.GetProductById(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            //context.Products.Remove(context.Products.Find(id));
            //context.SaveChanges();

            cartRepository.Delete(id);

            return RedirectToAction("CustomerCart");
        }

    }
}