using System.Collections.Generic;
using System.Web.Http;
using Liberty.POC.UI.Helpers;
using Liberty.POC.UI.Models;

namespace Liberty.POC.UI.Controllers
{
    public class MobileApiController : ApiController
    {
        [HttpGet]
        public List<ClientDetailsModel> Index()
        {
            var allSessions = Helper.GetAllClientDetails();
            return allSessions;
        }

        [HttpGet]
        public ClientDetailsModel Index(long? id)
        {
            var clientDetailsModel = new ClientDetailsModel();

            if (id.HasValue)
            {
                clientDetailsModel = Helper.GetClientDetails(id.Value);
            }

            return clientDetailsModel;
        }

        [System.Web.Mvc.HttpPost]
        public void Save(ClientDetailsModel clientDetailsModel)
        {
            if (clientDetailsModel == null)
                return;

            if (clientDetailsModel.Id > 0)
                Helper.UpdateSession(clientDetailsModel);
            else
                Helper.InsertSession(clientDetailsModel);
        }

        [HttpGet]
        public bool Authenticate(string clientName, string password)
        {
            return true;
        }
    }
}