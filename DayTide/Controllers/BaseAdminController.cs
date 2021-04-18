using DayTide.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayTide.Controllers
{
    public class BaseAdminController : Controller
    {

       // AdminRepository adminRepository = new AdminRepository();
        // GET: BaseAdmin
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (Session["type"].ToString()!= "Admin")
            {
                Response.Redirect("/Login/Login");
            }
        }
    }
}