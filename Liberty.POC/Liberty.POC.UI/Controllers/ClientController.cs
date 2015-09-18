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
                return RedirectToAction("Login");

            var dataModel = CreateDefaultSession(clientName);

            var session = new ClientDetailsModel
            {
                Id = dataModel.SessionID,
                Name = dataModel.ClientName,
                IsCompleted = dataModel.Completed
            };

            return View(session);
        }

        private static Session CreateDefaultSession(string clientName)
        {
            const string defaultCurrentStep = "Personal";

            var dataModel = new Session
            {
                ClientName = clientName,
                CurrentStep = defaultCurrentStep,
                Completed = false
            };

            SaveSessionData(dataModel);
            return dataModel;
        }

        [HttpPost]
        public JsonResult Save(ClientDetailsModel clientDetailsModel)
        {
            UpdateSessionData(clientDetailsModel);

            return Json(new { status = "Success", message = "Success" });
        }

        private static void UpdateSessionData(ClientDetailsModel clientDetailsModel)
        {
            using (var ctx = new LibertyPocEntities())
            {
                var session = ctx.Sessions.FirstOrDefault(s => s.SessionID == clientDetailsModel.Id);

                if (session != null)
                {
                    session.CurrentStep = clientDetailsModel.CurrentStep;
                    session.Address = System.Web.Helpers.Json.Encode(clientDetailsModel.AddressDetails);
                    session.Contact = System.Web.Helpers.Json.Encode(clientDetailsModel.ContactDetail);
                    session.Personal = System.Web.Helpers.Json.Encode(clientDetailsModel.PersonalDetails);
                    session.Completed = clientDetailsModel.IsCompleted;
                }

                ctx.SaveChanges();
            }
        }

        private static void SaveSessionData(Session dataModel)
        {
            using (var ctx = new LibertyPocEntities())
            {
                ctx.Sessions.Add(dataModel);
                ctx.SaveChanges();
            }
        }
    }
}