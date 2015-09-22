using System.Web.Mvc;
using Liberty.POC.UI.Helpers;

namespace Liberty.POC.UI.Controllers
{
    public class ClientController : Controller
    {
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

            var clientDetailsModel = Helper.GetClientDetails(clientName);

            if (clientDetailsModel.Id == 0)
            {
                clientDetailsModel.Name = clientName;
                Helper.InsertSession(clientDetailsModel);
            }

            return View(clientDetailsModel);
        }
        
        public string Clear()
        {
            Helper.DeleteAllSessions();
            return "Done";
        }
    }
}