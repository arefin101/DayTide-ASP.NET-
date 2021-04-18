using DayTide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTide.Repositories
{
    public class ProductRepository: Repository<Product>
    {
        //public List<Cart> GetCartById(string id)
        //{
        //    return this.context.Carts.Where(x => x.CustomerId == id).ToList();
        //}

        public List<Product> GetProductByCateg(int id)
        {
            return this.context.Products.Where(y => y.CategoryId == id).ToList();
        }

        public List<Product> GetProductByPrice(int top)
        {
            return this.context.Products.OrderByDescending(i => i.Selling_Price).Take(top).ToList();
        }

        public List<Product> GetProductSearch(string key)
        {


            return this.context.Products.Where(l => l.ProductName.Contains(key)).ToList();
        }


    }
}