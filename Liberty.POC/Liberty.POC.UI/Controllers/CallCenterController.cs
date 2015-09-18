﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Liberty.POC.UI.Controllers
{
    public class CallCenterController : Controller
    {
        public ActionResult Index()
        {
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

        public ActionResult Process()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        public string Clear()
        {
            using (var libertyPocEntities = new LibertyPocEntities())
            {
                libertyPocEntities.Sessions.RemoveRange(libertyPocEntities.Sessions);
                libertyPocEntities.SaveChanges();
            }
            return "Done";
        }
    }
}
