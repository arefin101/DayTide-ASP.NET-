using DayTide.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayTide.Controllers
{
    public class DeliveryManController : Controller
    {
        // GET: DeliveryMan
        Order_DetailRepository order_DetailRepository = new Order_DetailRepository();
        DeleveryManRepository deleveryManRepository = new DeleveryManRepository();
        CartBackupRepository cartBackupRepository = new CartBackupRepository();
        public ActionResult Index()
        {
            var order = order_DetailRepository.GetAll().Where(x=>x.DelManId==Session["UserId"].ToString()).ToList();
            return View(order);
        }
        public ActionResult Done(int OrderId)
        {
            var order = order_DetailRepository.GetOrderDetailByOrderId(OrderId);
            var cart = cartBackupRepository.GetAll().Where(x=>x.OrderId==OrderId).ToList();
            order.Status = "Done";
            foreach(var item in cart)
            {
                item.Quantiry = -1;
                cartBackupRepository.Update(item);
            }
            var delman = deleveryManRepository.GetUserById(Session["UserId"].ToString());
            delman.In_Service = 0;
            delman.Complete_Task++;
            deleveryManRepository.Update(delman);
            order_DetailRepository.Update(order);
            return RedirectToAction("Index","DeliveryMan");
        }
    }
}