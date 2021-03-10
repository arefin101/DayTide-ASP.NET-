using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTide.Repositories
{
    interface IRepository <TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity GetUserById(string id);
        TEntity GetProductById(int id);
        TEntity GetCategoryById(int id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void DeleteUser(string id);
        void Delete(int id);
        void DeleteProduct(int id);
    }
}
