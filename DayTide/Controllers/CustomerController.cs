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

            return RedirectToAction("Index");

        }

    }
}