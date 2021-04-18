using DayTide.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DayTide.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DaytideEntities2 context = new DaytideEntities2();
        public void DeleteProduct(int id)
        {
            context.Set<TEntity>().Remove(GetProductById(id));
            context.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            context.Set<TEntity>().Remove(GetUserById(id));
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            context.Set<TEntity>().Remove(GetCategoryById(id));
            context.SaveChanges();
        }

        public List<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public TEntity GetUserById(string id)
        {
            return context.Set<TEntity>().Find(id);
        }
        public TEntity GetProductById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }
        public TEntity GetCategoryById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public void Insert(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}