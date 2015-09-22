using System.Web.Mvc;
using Liberty.POC.UI.Helpers;
using Liberty.POC.UI.Models;

namespace Liberty.POC.UI.Controllers
{
    public class CallCenterController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Process(long? id)
        {
            var clientDetailsModel = new ClientDetailsModel();
            if (id.HasValue)
            {
                clientDetailsModel = Helper.GetClientDetails(id.Value);
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
