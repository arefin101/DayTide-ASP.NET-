using DayTide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTide.Repositories
{
    public class CartBackupRepository: Repository<CartBackup>
    {
        public CartBackup GetCartBackupById(int id)
        {
            return context.Set<CartBackup>().Find(id);
        }
    }
}