using DayTide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTide.Repositories
{
    public class Delevary_Man_RatingRepository:Repository<Delevary_Man_RatingRepository>
    {
        public List<Delevary_Man_Rating> GetDeleveryMenRatingById(string id)
        {
            return this.context.Delevary_Man_Rating.Where(x => x.DelManID==id).ToList();
        }
    }
}