using System.Web.Mvc;
using Liberty.POC.UI.Helpers;
using Liberty.POC.UI.Models;

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

        [HttpPost]
        public JsonResult Save(ClientDetailsModel clientDetailsModel)
        {
            Helper.UpdateSession(clientDetailsModel);

            return Json(new { status = "Success", message = "Success" });
        }

        public string Clear()
        {
            Helper.DeleteAllSessions();
            return "Done";
        }
    }
}