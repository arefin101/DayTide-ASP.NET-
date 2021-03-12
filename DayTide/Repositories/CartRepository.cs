using DayTide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTide.Repositories
{
    public class CartRepository : Repository<Cart>
    {
        public List<Cart> GetCartById(string id)
        {
            return this.context.Carts.Where(x => x.CustomerId == id).ToList();
        }
        
        //public List<Cart> GetCartProduct(string id)
        //{
        //    return this.context.Carts.Find();

        //    //select price, quantity from cart  where cart.productId = product.productId
        //}

    }
}