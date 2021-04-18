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

        public Cart GetCartByProductId(int id)
        {
            return this.context.Carts.Where(x => x.ProductId == id).FirstOrDefault();
        }


        public void DeleteCustomerCart(string id)
        {
            var itemss = context.Carts.Where(q => q.CustomerId == id).ToList();

            context.Carts.RemoveRange(itemss);
            context.SaveChanges();
            //var itemss = GetCartById(id);
            //foreach (var customer in itemss)
            //{
            //    context.Carts.RemoveRange(customer);
            //}

            //GetCartById(cid)

            //var table1 = context.Carts.Where(x => itemss.Contains(Convert.ToString(x.CustomerId)).ToList();
            //if (table1 != null && table1.Count > 0)
            //{
            //    _context.table1.RemoveRange(table1);
            //    _context.SaveChanges();
            //}

        }
        public Cart GetCartByProdIDCusID(int proId, string cusID)
        {
            return this.context.Carts.Where(x => x.ProductId == proId).Where(u => u.CustomerId == cusID).FirstOrDefault();
        }

    }
}