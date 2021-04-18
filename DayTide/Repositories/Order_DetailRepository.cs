using DayTide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTide.Repositories
{
    public class Order_DetailRepository : Repository<Order_Detail>
    {
        public List<Order_Detail> GetOrderDetailByUsertId(string id)
        {
            return this.context.Order_Detail.Where(x => x.CustomerId == id).ToList();
        }
        public Order_Detail GetOrderDetailByOrderId(int id)
        {
            return context.Order_Detail.Find(id);
        }
    }
}