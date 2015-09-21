using System.Linq;
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

            var userSession = this.GetUserSession(clientName);
            if(userSession == default(Session))
                userSession = CreateDefaultSession(clientName);   

            var clientDetailsModel = new ClientDetailsModel
            {
                Id = userSession.SessionID,
                Name = userSession.ClientName,
                IsCompleted = userSession.Completed,
                CurrentStep = userSession.CurrentStep,
                AddressDetails = GetDeserialisedObject<AddressModel>(userSession.Address),
                PersonalDetails = GetDeserialisedObject<PersonalModel>(userSession.Personal),
                ContactDetail = GetDeserialisedObject<ContactModel>(userSession.Contact)
            };

            return View(clientDetailsModel);
        }

        private static T GetDeserialisedObject<T>(string serialisedData) where T : new()
        {

            return !string.IsNullOrEmpty(serialisedData)
                ? System.Web.Helpers.Json.Decode<T>(serialisedData)
                : new T();
        }

        private Session GetUserSession(string clientName)
        {
            using(var libertyPocEntities = new LibertyPocEntities())
            {
                return libertyPocEntities
                    .Sessions
                    .Where(n => n.ClientName == clientName 
                        && !n.Completed)
                    .OrderByDescending(n => n.SessionID)
                    .FirstOrDefault();
            }
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