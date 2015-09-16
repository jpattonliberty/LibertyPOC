using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Liberty.POC.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            using (var data = new LibertyPocEntities())
            {
                data.Sessions.Add(new Session {});
            }

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
