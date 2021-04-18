using DayTide.Models;
using DayTide.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayTide.Controllers
{
    public class CustomerController : Controller
    {
        CustomerRepository customerRepository = new CustomerRepository();

        ProductRatingRepository productRatingRepository = new ProductRatingRepository();

        UserRepository userRepository = new UserRepository();

        public ActionResult Index()
        {

            return View(customerRepository.GetAll());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                User user1 = new User();

                user1.UserId = customer.CustomerId;
                user1.Password = customer.User.Password;
                user1.Type = "Customer";
                user1.Status = "valid";

                userRepository.Insert(user1);

                Customer customer1 = new Customer();
                customer1.CustomerId = customer.CustomerId;
                customer1.Name = customer.Name;
                customer1.Email = customer.Email;
                customer1.Phone = customer.Phone;
                customer1.Address = customer.Address;
                customer1.Picture = customer.Picture;

                customerRepository.Insert(customer1);

                // customerRepository.Insert(customer);

               
                return RedirectToAction("login", "Login");
            }
            return View();

        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(customerRepository.GetUserById(id));
        }
        [HttpPost]
        public ActionResult Edit(string id, Customer customer)
        {
            customerRepository.Update(customer);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            //customerRepository.DeleteUser(id);

            return View(customerRepository.GetUserById(id));

        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            customerRepository.DeleteUser(id);

            return RedirectToAction("Index");
        }


        public ActionResult MyprofileCustomer()
        {
            if(Session["userid"]==null)
            {
                return RedirectToAction("Index", "Home");
            }

            string id = Session["userid"].ToString();

            return View(customerRepository.GetUserById(id));
            
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "Login");
            }

            string uId = Session["userid"].ToString();

            return View(userRepository.GetUserById(uId));
        }
        [HttpPost]
        public ActionResult ChangePassword(User user)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "Login");
            }

            if (ModelState.IsValid) 
            {

                userRepository.Update(user);

                Session.Clear();

                return RedirectToAction("login", "Login");
            }


            return View();

        }

        [HttpGet]
        public ActionResult ProductRating(int id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "Login");
            }


            TempData["ProductID"] = id;

            return View();
        }

        [HttpPost]
        public ActionResult ProductRating(Product_Rating product_Rating)
        {
            TempData["ProductID"] = 4;
            if (ModelState.IsValid)
            {
                productRatingRepository.Insert(product_Rating);

                return RedirectToAction("Index", "Home");
            }

            
            return View();
        }



    }
}