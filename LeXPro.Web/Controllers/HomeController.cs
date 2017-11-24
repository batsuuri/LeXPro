using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeXPro.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        [RBAC]
        public ActionResult About()
        {
            return View();
        }
        
        [RBAC]
        public ActionResult Reports()
        {
            //if(!this.User.HasPermission("Home-Reports"))
            return View();
        }
    }
}