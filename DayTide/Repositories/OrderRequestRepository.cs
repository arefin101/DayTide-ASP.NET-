using DayTide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTide.Repositories
{
    public class OrderRequestRepository: Repository<OrderRequest>
    {
        public OrderRequest GetOrderRequestById(int id)
        {
            return this.context.OrderRequests.Where(x => x.OrderId == id).FirstOrDefault();
        }
    }
}