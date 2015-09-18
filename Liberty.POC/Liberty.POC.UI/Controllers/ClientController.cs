using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Liberty.POC.UI.Models;

namespace Liberty.POC.UI.Controllers
{
    public class ClientController : Controller
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

        [HttpGet]
        public ActionResult Login()
        {
            //System.Web.Helpers.Json.Encode();
            //System.Web.Helpers.Json.Decode();

            return View();
        }

        [HttpPost]
        public ActionResult Login(string clientName, string password)
        {
            return RedirectToAction("Process", new { clientName });
        }

        [HttpGet]
        public ActionResult Process(string clientName)
        {
            if (string.IsNullOrWhiteSpace(clientName))
                throw new ArgumentNullException("clientName");

            var session = new ClientDetailsModel
            {
                Name = clientName,
                IsCompleted = false,
            };

            return View(session);
        }

        [HttpPost]
        public JsonResult Save(ClientDetailsModel clientDetailsModel)
        {
            var dataModel = new Session
            {
                Address = System.Web.Helpers.Json.Encode(clientDetailsModel.AddressDetails),
                Contact = System.Web.Helpers.Json.Encode(clientDetailsModel.ContactDetail),
                Personal = System.Web.Helpers.Json.Encode(clientDetailsModel.PersonalDetails),
                ClientName = clientDetailsModel.Name,
                Completed = clientDetailsModel.IsCompleted
            };

            using (var context = new LibertyPocEntities())
            {
                context.Sessions.Add(dataModel);
            }
            
            return Json(new { status = "Success", message = "Success" });
        }
    }
}