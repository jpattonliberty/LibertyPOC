using System;
using System.Linq;
using System.Web.Mvc;
using Liberty.POC.UI.Models;

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

        public ActionResult Process(long? id)
        {
            var dataModel = id.HasValue
                                ? GetSessionData(id.Value)
                                : GetNullSessionObject();

            var clientDetailsModel = new ClientDetailsModel
            {
                Id = dataModel.SessionID,
                Name = dataModel.ClientName,
                IsCompleted = dataModel.Completed,
                CurrentStep = dataModel.CurrentStep,
                AddressDetails = GetDeserialisedObject<AddressModel>(dataModel.Address),
                PersonalDetails = GetDeserialisedObject<PersonalModel>(dataModel.Personal),
                ContactDetail = GetDeserialisedObject<ContactModel>(dataModel.Contact)
            };

            return View(clientDetailsModel);
        }

        private Session GetNullSessionObject()
        {
            const string defaultCurrentStep = "Personal";

            return new Session
            {
                CurrentStep = defaultCurrentStep,
                Address = string.Empty,
                SessionID = 0,
                Personal = string.Empty,
                Completed = false,
                Contact = string.Empty,
                ClientName = string.Empty
            };
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

        private static Session GetSessionData(long sessionId)
        {
            using (var ctx = new LibertyPocEntities())
            {
                return ctx.Sessions.FirstOrDefault(s => s.SessionID == sessionId);
            }
        }

        private static T GetDeserialisedObject<T>(string serialisedData) where T : new()
        {

            return !string.IsNullOrEmpty(serialisedData)
                ? System.Web.Helpers.Json.Decode<T>(serialisedData)
                : new T();
        }
    }
}
