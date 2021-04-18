using DayTide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTide.Repositories
{
    public class DeleveryManRepository : Repository<DeleveryMan>
    {
        public List<DeleveryMan> GetDeleveryMenByAdd(string add)
        {
            return this.context.DeleveryMen.Where(x => x.Address.Contains(add) ).ToList();
        }
    }
}