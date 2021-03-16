﻿using DayTide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTide.Repositories
{
    public class UserRepository : Repository<User>
    {
        public List<User> GetUserSignUpReq()
        {
            return this.context.Users.Where(x => x.Status == "Processing").ToList();
        }
    }
}